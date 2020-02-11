using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Apidaze.SDK.ScriptBuilder;
using Apidaze.SDK.ScriptBuilder.POCO;
using Newtonsoft.Json;

namespace IvrExample
{
    /// <summary>
    /// Class WebService.
    /// </summary>
    internal static class WebService
    {
        /// <summary>
        /// The port the HttpListener should listen on.
        /// </summary>
        private static readonly string SERVER_URL = "http://780f6967.ngrok.io";

        /// <summary>
        /// The localhost.
        /// </summary>
        private static readonly string LOCALHOST = "http://localhost:8080";

        /// <summary>The context HTTP dictionary</summary>
        private static Dictionary<ContextHttp, Func<byte[]>> contextHttpDictionary;

        /// <summary>
        /// The intro path.
        /// </summary>
        static readonly string INTRO_PATH = "/";

        /// <summary>
        /// The step 1 path.
        /// </summary>
        static readonly string STEP_1_PATH = "/step1.xml";

        /// <summary>
        /// The step 2 path.
        /// </summary>
        static readonly string STEP_2_PATH = "/step2.xml";

        /// <summary>
        /// The step 3 path.
        /// </summary>
        static readonly string STEP_3_PATH = "/step3.xml";

        /// <summary>
        /// The playback path.
        /// </summary>
        static readonly string PLAYBACK_PATH = "/apidazeintro.wav";

        /// <summary>
        /// This is the heart of the web server.
        /// </summary>
        private static readonly HttpListener Listener = new HttpListener();

        /// <summary>
        /// A flag to specify when we need to stop.
        /// </summary>
        private static bool _keepGoing = true;

        /// <summary>
        /// Keep the task in a static variable to keep it alive.
        /// </summary>
        private static Task _mainLoop;

        /// <summary>
        /// Call this to start the web server.
        /// </summary>
        public static void StartWebServer()
        {
            if (_mainLoop != null && !_mainLoop.IsCompleted)
            {
                return;
            }

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
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// The main loop to handle requests into the HttpListener.
        /// </summary>
        private static async Task MainLoop()
        {
            var prefixes = new List<string>()
            {
                LOCALHOST,
                LOCALHOST + STEP_1_PATH,
                LOCALHOST + STEP_2_PATH,
                LOCALHOST + STEP_3_PATH,
                LOCALHOST + PLAYBACK_PATH,
            };
            prefixes.ForEach(p =>
            {
                var pd = p.AddForwardSlash();
                Listener.Prefixes.Add(pd);
            });

            Listener.Start();
            contextHttpDictionary = new Dictionary<ContextHttp, Func<byte[]>> {
                {
                    new ContextHttp(INTRO_PATH), GetIntro
                },
                {
                    new ContextHttp(STEP_1_PATH), GetStep1
                },
                {
                    new ContextHttp(STEP_2_PATH), GetStep2
                },
                {
                    new ContextHttp(STEP_3_PATH), GetStep3
                },
                {
                    new ContextHttp(PLAYBACK_PATH)
                    {
                        ContentType = "audio/wav",
                    }, () => ExampleUtil.GetFileContents("apidazeintro.wav")
                },
            };
            while (_keepGoing)
            {
                try
                {
                    var context = await Listener.GetContextAsync();
                    lock (Listener)
                    {
                        if (_keepGoing)
                        {
                            ProcessRequest(context);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    if (e is HttpListenerException)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Handle an incoming request
        /// </summary>
        /// <param name="context">The context of the incoming request.</param>
        private static void ProcessRequest(HttpListenerContext context)
        {
            using var response = context.Response;
            try
            {
                Console.WriteLine(context.Request.RawUrl + "\n" + DateTime.Now);
                var handled = false;
                var contextHttp = new ContextHttp(context.Request.Url.AbsolutePath);
                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        if (contextHttpDictionary.ContainsKey(contextHttp))
                        {
                            var result = contextHttpDictionary[contextHttp].DynamicInvoke();
                            handled = WriteResponse(response, contextHttpDictionary.Keys.FirstOrDefault(p => p.AbsolutePath == contextHttp.AbsolutePath)?.ContentType, (byte[])result);
                        }

                        break;
                    case "HEAD":
                        handled = true;
                        response.ContentLength64 = 0;
                        break;
                }

                if (!handled)
                {
                    response.StatusCode = 404;
                }
            }
            catch (Exception e)
            {
                ReturnExceptionResponse(response, e);
            }
        }

        /// <summary>
        /// Gets the intro.
        /// </summary>
        private static byte[] GetIntro()
        {
            var script = ApidazeScript.Build();

            var intro = script.AddNode(Ringback.FromFile(SERVER_URL + PLAYBACK_PATH)).AddNode(Wait.SetDuration(2))
                .AddNode(new Answer()).AddNode(new Record { Name = "example_recording" }).AddNode(Wait.SetDuration(2))
                .AddNode(Playback.FromFile(SERVER_URL + PLAYBACK_PATH))
                .AddNode(Speak.WithText("This example script will show you some things you can do with our API"))
                .AddNode(Wait.SetDuration(2)).AddNode(
                    new Speak
                    {
                        DigitTimeoutMillis = TimeSpan.FromSeconds(15).TotalMilliseconds,
                        InputTimeoutMillis = TimeSpan.FromSeconds(15).TotalMilliseconds,
                        Text = "Press 1 for an example of text to speech, press 2 to enter an echo line to check voice latency or press 3 to enter a conference.",
                        Binds = new List<object>
                        {
                            new Bind {Action = SERVER_URL + STEP_1_PATH, Value = "1"},
                            new Bind {Action = SERVER_URL + STEP_2_PATH, Value = "2"},
                            new Bind {Action = SERVER_URL + STEP_3_PATH, Value = "3"},
                        },
                    })
                .ToXml();
            return Encoding.UTF8.GetBytes(intro);
        }

        /// <summary>
        /// Gets the step1.
        /// </summary>
        private static byte[] GetStep1()
        {
            var step1 = ApidazeScript.Build()
                .AddNode(Speak.WithText(
                    "Our text to speech leverages Google's cloud APIs to offer the best possible solution"))
                .AddNode(Wait.SetDuration(1))
                .AddNode(new Speak()
                {
                    LangEnum = LangEnum.ENGLISH_AUSTRALIA,
                    Voice = VoiceEnum.MALE_A,
                    Text = "A wide variety of voices and languages are available.Here are just a few",
                }).AddNode(Wait.SetDuration(1))
                .AddNode(new Speak() { LangEnum = LangEnum.FRENCH_FRANCE, Text = "Je peux parler français" }).AddNode(
                    Wait.SetDuration(1)).AddNode(new Speak() { LangEnum = LangEnum.GERMAN, Text = "Auch deutsch" })
                .AddNode(Wait.SetDuration(1)).AddNode(new Speak()
                {
                    LangEnum = LangEnum.JAPANESE,
                    Text = "そして日本人ですら",
                }).AddNode(Wait.SetDuration(1)).AddNode(new Speak()
                {
                    Text =
                        "You can see our documentation for a full list of supported languages and voices for them.  We'll take you back to the menu for now.",
                }).AddNode(Wait.SetDuration(2))
                .ToXml();
            return Encoding.UTF8.GetBytes(step1);
        }

        /// <summary>
        /// Gets the step2.
        /// </summary>
        private static byte[] GetStep2()
        {
            var step2 = ApidazeScript.Build()
                .AddNode(Speak.WithText("You will now be joined to an echo line."))
                .AddNode(Echo.SetDuration(500))
                .ToXml();
            return Encoding.UTF8.GetBytes(step2);
        }

        /// <summary>
        /// Gets the step3.
        /// </summary>
        private static byte[] GetStep3()
        {
            var step3 = ApidazeScript.Build()
                .AddNode(Speak.WithText("You will be entered into a conference call now.  You can initiate more calls to join participants or hangup to leave"))
                .AddNode(new Conference() { Name = "SDKTestConference" })
                .ToXml();
            return Encoding.UTF8.GetBytes(step3);
        }

        /// <summary>
        /// Returns the exception response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="exception">The exception.</param>
        private static void ReturnExceptionResponse(HttpListenerResponse response, Exception exception)
        {
            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(exception));
            WriteResponse(response, "text/plain", buffer, HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Writes the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns><c>true</c> if handled, <c>false</c> otherwise.</returns>
        private static bool WriteResponse(HttpListenerResponse response, string contentType, byte[] buffer, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            response.ContentType = contentType;
            response.StatusCode = (int)statusCode;
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            return true;
        }
    }
}