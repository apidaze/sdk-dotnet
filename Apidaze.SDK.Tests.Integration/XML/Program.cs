using Apidaze.SDK.ScriptBuilder;
using Apidaze.SDK.ScriptBuilder.POCO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XML
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
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

        /// <summary>
        /// Class WebService.
        /// </summary>
        internal static class WebService
        {
            /// <summary>
            /// The port the HttpListener should listen on
            /// </summary>
            private static readonly string LOCALHOST = "http://localhost:8080";

            /// <summary>
            /// The intro prefix
            /// </summary>
            static readonly string INTRO_PATH = "/";

            /// <summary>
            /// This is the heart of the web server
            /// </summary>
            private static readonly HttpListener Listener = new HttpListener();

            /// <summary>The context HTTP dictionary</summary>
            private static Dictionary<string, Func<byte[]>> httpContextDictionary;


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
                catch
                {
                    // ignored
                }
            }

            /// <summary>
            /// The main loop to handle requests into the HttpListener
            /// </summary>
            private static async Task MainLoop()
            {
                GetReadyHttpContext();
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

            private static void GetReadyHttpContext()
            {
                var prefixes = new List<string>()
                {
                    LOCALHOST + INTRO_PATH
                };
                prefixes.ForEach(p =>
                {
                    Listener.Prefixes.Add(p);
                });

                httpContextDictionary = new Dictionary<string, Func<byte[]>>
                {
                    {
                        INTRO_PATH, GetResponse
                    },
                };
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
                    Console.WriteLine(context.Request.RawUrl + "\n" + DateTime.Now);
                    var handled = false;

                    switch (context.Request.HttpMethod)
                    {
                        case "GET":
                            if (httpContextDictionary.ContainsKey("/"))
                            {
                                var result = httpContextDictionary["/"].DynamicInvoke();
                                handled = WriteResponse(response, "text/xml", (byte[])result);
                            }

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
            /// Gets the response.
            /// </summary>
            private static byte[] GetResponse()
            {
                var script = ApidazeScript.Build();
                var intro = script.AddNode(new Dial
                {
                    Timeout = 24,
                    Strategy = StrategyEnum.SEQUENCE,
                    Number =
                        new List<Number> { new Number("14123456789", 12), new Number("14123456789", 12) }
                }).ToXml();
                return Encoding.UTF8.GetBytes(intro);
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
}