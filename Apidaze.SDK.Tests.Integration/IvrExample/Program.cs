using Apidaze.SDK.ScriptBuilder;
using Apidaze.SDK.ScriptBuilder.POCO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IvrExample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                WebService.StartWebServer();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }

    internal static class WebService
    {
        /// <summary>
        /// The port the HttpListener should listen on
        /// </summary>
        private static readonly string SERVER_URL = "http://01393488.ngrok.io";
        private static readonly string LOCALHOST = "http://localhost:8080";

        static readonly string INTRO_PATH = "/";
        static readonly string STEP_1_PATH = "/step1.xml/";
        static readonly string STEP_2_PATH = "/step2.xml/";
        static readonly string STEP_3_PATH = "/step3.xml/";
        static readonly string PLAYBACK_PATH = "/apidazeintro.wav/";

        /// <summary>
        /// This is the heart of the web server
        /// </summary>
        private static readonly HttpListener Listener = new HttpListener()
        {
            Prefixes = {
                LOCALHOST + INTRO_PATH,
                LOCALHOST + STEP_1_PATH,
                LOCALHOST + STEP_2_PATH,
                LOCALHOST + STEP_3_PATH,
                LOCALHOST + PLAYBACK_PATH,
            }
        };

        /// <summary>
        /// A flag to specify when we need to stop
        /// </summary>
        private static bool _keepGoing = true;

        /// <summary>
        /// Keep the task in a static variable to keep it alive
        /// </summary>
        private static Task _mainLoop;

        /// <summary>
        /// Call this to start the web server
        /// </summary>
        public static void StartWebServer()
        {
            if (_mainLoop != null && !_mainLoop.IsCompleted) return; //Already started
            _mainLoop = MainLoop();
        }

        /// <summary>
        /// Call this to stop the web server. It will not kill any requests currently being processed.
        /// </summary>
        public static void StopWebServer()
        {
            _keepGoing = false;
            lock (Listener)
            {
                Listener.Stop();
            }
            try
            {
                _mainLoop.Wait();
            }
            catch { }
        }

        /// <summary>
        /// The main loop to handle requests into the HttpListener
        /// </summary>
        /// <returns></returns>
        private static async Task MainLoop()
        {

            Listener.Prefixes.Add(LOCALHOST + INTRO_PATH);
            Listener.Prefixes.Add(LOCALHOST + STEP_1_PATH);
            Listener.Prefixes.Add(LOCALHOST + STEP_2_PATH);
            Listener.Prefixes.Add(LOCALHOST + STEP_3_PATH);
            Listener.Prefixes.Add(LOCALHOST + PLAYBACK_PATH);

            Listener.Start();
            while (_keepGoing)
            {
                try
                {
                    var context = await Listener.GetContextAsync();
                    lock (Listener)
                    {
                        if (_keepGoing) ProcessRequest(context);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    if (e is HttpListenerException) return;
                }
            }
        }

        /// <summary>
        /// Handle an incoming request
        /// </summary>
        /// <param name="context">The context of the incoming request</param>
        private static void ProcessRequest(HttpListenerContext context)
        {
            using var response = context.Response;
            try
            {
                var handled = false;
                switch (context.Request.Url.AbsolutePath)
                {
                    case "/":
                        handled = GetIntro(context, response);
                        break;
                    case "/step1.xml":
                        handled = GetStep1(context, response);
                        break;
                    case "/step2.xml":
                        handled = GetStep2(context, response);
                        break;
                    case "/step3.xml":
                        handled = GetStep3(context, response);
                        break;
                    case "/apidazeintro.wav":
                        handled = GetIntroWav(context, response);
                        break;
                }
                if (!handled)
                {
                    response.StatusCode = 404;
                }
            }
            catch (Exception e)
            {
                //Return the exception details the client - you may or may not want to do this
                ReturnExceptionResponse(response, e);
            }
        }

        private static bool GetIntro(HttpListenerContext context, HttpListenerResponse response)
        {
            var handled = false;
            switch (context.Request.HttpMethod)
            {
                case "GET":

                    var script = ApidazeScript.Build();

                    var intro = script.AddNode(Ringback.FromFile(SERVER_URL + PLAYBACK_PATH.Remove(PLAYBACK_PATH.Length - 1))).AddNode(Wait.SetDuration(2))
                  .AddNode(new Answer()).AddNode(new Record { Name = "example_recording" }).AddNode(Wait.SetDuration(2))
                  .AddNode(Playback.FromFile(SERVER_URL + PLAYBACK_PATH.Remove(PLAYBACK_PATH.Length - 1)))
                  .AddNode(Speak.WithText("This example script will show you some things you can do with our API"))
                  .AddNode(Wait.SetDuration(2)).AddNode(
                      new Speak
                      {
                          InputTimeoutMillis = TimeSpan.FromSeconds(10).TotalMilliseconds,
                          Text = "Press 1 for an example of text to speech, press 2 to enter an echo line to check voice latency or press 3 to enter a conference.",
                          Binds = new List<object>
                          {
                                                    new Bind {Action = SERVER_URL + STEP_1_PATH.Remove(STEP_1_PATH.Length - 1), Value = "1"},
                                                    new Bind {Action = SERVER_URL + STEP_2_PATH.Remove(STEP_2_PATH.Length - 1), Value = "2"},
                                                    new Bind {Action = SERVER_URL + STEP_3_PATH.Remove(STEP_3_PATH.Length - 1), Value = "3"}
                          }
                      })
                  .ToXml();
                    var buffer = Encoding.UTF8.GetBytes(intro);
                    handled = WriteResponse(response, "text/xml", buffer);
                    break;
            }

            return handled;
        }



        private static bool GetStep1(HttpListenerContext context, HttpListenerResponse response)
        {
            var handled = false;
            switch (context.Request.HttpMethod)
            {
                case "GET":
                    var step1 = ApidazeScript.Build()
                        .AddNode(Speak.WithText(
                            "Our text to speech leverages Google's cloud APIs to offer the best possible solution"))
                        .AddNode(Wait.SetDuration(1))
                        .AddNode(new Speak()
                        {
                            LangEnum = LangEnum.ENGLISH_AUSTRALIA,
                            Voice = VoiceEnum.MALE_A,
                            Text = "A wide variety of voices and languages are available.Here are just a few"
                        }).AddNode(Wait.SetDuration(1))
                        .AddNode(new Speak() { LangEnum = LangEnum.FRENCH_FRANCE, Text = "Je peux parler français" }).AddNode(
                            Wait.SetDuration(1)).AddNode(new Speak() { LangEnum = LangEnum.GERMAN, Text = "Auch deutsch" })
                        .AddNode(Wait.SetDuration(1)).AddNode(new Speak()
                        {
                            LangEnum = LangEnum.JAPANESE,
                            Text = "そして日本人ですら"
                        }).AddNode(Wait.SetDuration(1)).AddNode(new Speak()
                        {
                            Text =
                                "You can see our documentation for a full list of supported languages and voices for them.  We'll take you back to the menu for now."
                        }).AddNode(Wait.SetDuration(2))
                        .ToXml();

                    var buffer = Encoding.UTF8.GetBytes(step1);
                    handled = WriteResponse(response, "text/xml", buffer);
                    break;
            }

            return handled;
        }

        private static bool GetStep2(HttpListenerContext context, HttpListenerResponse response)
        {
            var handled = false;
            switch (context.Request.HttpMethod)
            {
                case "GET":
                    var step2 = ApidazeScript.Build()
                        .AddNode(Speak.WithText("You will now be joined to an echo line."))
                        .AddNode(Echo.SetDuration(500))
                        .ToXml();

                    var buffer = Encoding.UTF8.GetBytes(step2);
                    handled = WriteResponse(response, "text/xml", buffer);
                    break;
            }

            return handled;
        }
        private static bool GetStep3(HttpListenerContext context, HttpListenerResponse response)
        {
            var handled = false;
            switch (context.Request.HttpMethod)
            {
                case "GET":
                    var step3 = ApidazeScript.Build()
                        .AddNode(Speak.WithText("You will be entered into a conference call now.  You can initiate more calls to join participants or hangup to leave"))
                        .AddNode(new Conference() { Name = "SDKTestConference" })
                        .ToXml();

                    var buffer = Encoding.UTF8.GetBytes(step3);
                    handled = WriteResponse(response, "text/xml", buffer);
                    break;
            }

            return handled;
        }

        private static void ReturnExceptionResponse(HttpListenerResponse response, Exception e)
        {
            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e)); 
            WriteResponse(response, "text/plain", buffer, HttpStatusCode.InternalServerError);
        }
     
        private static bool GetIntroWav(HttpListenerContext context, HttpListenerResponse response)
        {
            var handled = false;
            switch (context.Request.HttpMethod)
            {
                case "HEAD":
                    handled = true;
                    response.ContentLength64 = 0;
                    break;
                case "GET":
                    var fileContent = ExampleUtil.GetFileContents("apidazeintro.wav");
                    handled = WriteResponse(response, "audio/wav", fileContent);
                    break;
            }
            return handled;
        }

        private static bool WriteResponse(HttpListenerResponse response, string contentType, byte[] buffer, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            response.ContentType = contentType;
            response.StatusCode = (int)statusCode;
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            return true;
        }
    }

    public static class ExampleUtil
    {
        public static byte[] GetFileContents(string sampleFile)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resource = $"IvrExample.{sampleFile}";
            using var stream = asm.GetManifestResourceStream(resource);
            if (stream == null) return null;
            using var reader = new StreamReader(stream);
            return reader.BaseStream.ToByteArray();
        }

        private static byte[] ToByteArray(this Stream stream)
        {
            using MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}