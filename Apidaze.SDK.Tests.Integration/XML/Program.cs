﻿using APIdaze.SDK.ScriptsBuilders;
using APIdaze.SDK.ScriptsBuilders.POCO;
using HttpMock;
using System;
using System.Net;

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
            var stubHttp = HttpMockRepository.At("http://localhost:9191");
            stubHttp.Start();
            Console.WriteLine("Server is running at http://localhost:9191");

            var script = new ApidazeScript();

            string expected = script.AddNode(new Dial {Timeout = 12, Number = "123456788"}).AddNode(new Dial {Number = "123456789" } ).ToXml();

            stubHttp.Stub(x => x.Get("/"))
                .AsContentType("text/xml")
                .Return(expected)
                .OK();

            string result = new WebClient().DownloadString("http://localhost:9191/");

            Console.WriteLine("RESPONSE :\n{0}", result);
        }
    }
}