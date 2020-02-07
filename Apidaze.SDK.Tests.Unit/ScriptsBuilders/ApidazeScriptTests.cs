using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Apidaze.SDK.ScriptBuilder;
using Apidaze.SDK.ScriptBuilder.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apidaze.SDK.Tests.Unit.ScriptBuilder
{
    [TestClass]
    public class ApidazeScriptTests
    {
        [TestMethod]
        public void ShouldTest()
        {
            // Arrange
            var scripts = new ApidazeScript();
            var answer = new Answer();
            var playback = Playback.FromFile("http://www.mydomain.com/welcome.wav");
            playback.InputTimeoutMillis = TimeSpan.FromSeconds(5).TotalMilliseconds;

            var bind = new Bind() { Action = "http://www.mydomain.com/get_digits.php?bind=1", Value = "1" };

            playback.Binds = new List<Bind>() { bind, bind };

            scripts.AddNode(answer).AddNode(playback).AddNode(new Hangup());
            var xml = scripts.ToXml();
        }
    }
}
