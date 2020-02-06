using Apidaze.SDK.ScriptsBuilders;
using Apidaze.SDK.ScriptsBuilders.POCO;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XML
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

        internal static class WebService
        {
            /// <summary>
            /// The port the HttpListener should listen on
            /// </summary>
            private static readonly string LOCALHOST = "http://localhost:8080";

            /// <summary>The intro prefix</summary>
            static readonly string INTRO = "/";

            /// <summary>
            /// This is the heart of the web server
            /// </summary>
            private static readonly HttpListener Listener = new HttpListener();

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

                Listener.Prefixes.Add(LOCALHOST + INTRO);

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
                using (var response = context.Response)
                {
                    try
                    {
                        var handled = false;
                        switch (context.Request.Url.AbsolutePath)
                        {

                            case "/":
                                handled = GetResponse(context, response, handled);
                                break;
                        }
                        if (!handled)
                        {
                            response.StatusCode = 404;
                        }

                        Console.WriteLine(response.ContentType + response.StatusDescription);
                    }
                    catch (Exception e)
                    {
                        //Return the exception details the client - you may or may not want to do this
                        response.StatusCode = 500;
                        response.ContentType = "text/plain";
                        var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e));
                        response.ContentLength64 = buffer.Length;
                        response.OutputStream.Write(buffer, 0, buffer.Length);

                        //TODO: Log the exception
                    }
                }
            }

            private static bool GetResponse(HttpListenerContext context, HttpListenerResponse response, bool handled)
            {
                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        response.ContentType = "text/xml";

                        var script = ApidazeScript.Build();

                        var intro = script.AddNode(new Dial { Timeout = 12, Number = "48504916910" }).AddNode(new Dial { Number = "48504916910" }).ToXml();

                        var buffer = Encoding.UTF8.GetBytes(intro);
                        response.ContentLength64 = buffer.Length;
                        response.OutputStream.Write(buffer, 0, buffer.Length);
                        handled = true;
                        break;

                }

                return handled;
            }
        }
    }
}