﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Apidaze.SDK.ScriptBuilder;
using Apidaze.SDK.ScriptBuilder.POCO;
using static Apidaze.SDK.Tests.Unit.TestUtil;


namespace Apidaze.SDK.Tests.Unit.ScriptBuilder
{

    /// <summary>
    /// Defines test class ApidazeScriptTest.
    /// </summary>
    [TestClass]
    public class ApidazeScriptTest
    {
        private ApidazeScript _ApidazeScript;

        /// <summary>
        /// Startups this instance.
        /// </summary>
        [TestInitialize]
        public void Startup()
        {
            _ApidazeScript = new ApidazeScript();
        }

        /// <summary>
        /// Defines the test method ToXml_Answer_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Answer_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("answer.xml");
            _ApidazeScript.AddNode(new Answer()).AddNode(Playback.FromFile("http://www.mydomain.com/welcome.wav")).AddNode(new Hangup());
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_Playback_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Playback_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("playback.xml");
            _ApidazeScript.AddNode(new Answer()).AddNode(Playback.FromFile("http://www.mydomain.com/welcome.wav")).AddNode(new Hangup());
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_Ringback_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Ringback_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("ringback.xml");
            _ApidazeScript.AddNode(new Answer())
                          .AddNode(Ringback.FromFile("http://www.mydomain.com/welcome.wav"))
                          .AddNode(new Dial { Sipaccount = new List<SipAccount>() { new SipAccount("bob") } })
                          .AddNode(new Hangup());
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_Echo_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Echo_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("echo.xml");
            _ApidazeScript.AddNode(new Answer())
                .AddNode(new Echo { Delay = 500 });
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_Hangup_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Hangup_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("hangup.xml");
            _ApidazeScript.AddNode(new Hangup());
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_Intercept_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Intercept_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("intercept.xml");
            _ApidazeScript.AddNode(new Answer())
                .AddNode(new Intercept { Uuid = new Guid("f28a3e29-dac4-462c-bf94-b1d518ddbe2d") })
                .AddNode(new Hangup());
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_Speak_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Speak_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("speak.xml").RemoveWhiteSpaces();
            _ApidazeScript.AddNode(new Answer())
                .AddNode(new Speak
                {
                    LangEnum = LangEnum.FRENCH_FRANCE,
                    Voice = VoiceEnum.FEMALE_A,
                    Text = "Bonjour et bienvenue chez APIDAIZE. Vous pouvez patienter, mais n'oubliez pas de raccrocher."
                })
                .AddNode(Wait.SetDuration(5));
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true).RemoveWhiteSpaces();

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_BindWithSpeak_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_BindWithSpeak_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("bind-with-speak.xml").RemoveWhiteSpaces();
            _ApidazeScript.AddNode(new Answer())
                .AddNode(new Speak
                {
                    InputTimeoutMillis = TimeSpan.FromSeconds(5).TotalMilliseconds,
                    Text = "Press one to or two, or any digit, and we'll handle your call, or not.",
                    Binds = new List<object>
                        {
                            new Bind("http://www.mydomain.com/get_digits.php?bind=1", "1"),
                            new Bind("http://www.mydomain.com/get_digits.php?bind=2", "2"),
                            new Bind("http://www.mydomain.com/get_digits.php?bind=other", "~[3-9]")}

                });
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true).RemoveWhiteSpaces();

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_BindWithPlayback_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_BindWithPlayback_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("bind-with-playback.xml");
            _ApidazeScript.AddNode(new Answer())
                .AddNode(new Playback()
                {
                    InputTimeoutMillis = TimeSpan.FromSeconds(5).TotalMilliseconds,
                    File = "http://www.mydomain.com/welcome.wav",
                    Binds = new List<Bind> {
                        new Bind("http://www.mydomain.com/get_digits.php?bind=1", "1"),
                        new Bind("http://www.mydomain.com/get_digits.php?bind=2", "2"),
                        new Bind("http://www.mydomain.com/get_digits.php?bind=other", "~[3-9]") }

                });
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_Wait_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Wait_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("wait.xml");
            _ApidazeScript.AddNode(new Answer())
                .AddNode(new Speak
                {
                    LangEnum = LangEnum.FRENCH_FRANCE,
                    Text = "Bonjour et bienvenue chez APIDAIZE. Vous pouvez patienter, mais n'oubliez pas de raccrocher."
                }).AddNode(Wait.SetDuration(5));
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_Conference_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Conference_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("conference.xml");
            _ApidazeScript.AddNode(new Speak
            {
                Text = "You will now be placed into the conference"
            }).AddNode(new Conference { Name = "my_meeting_room" });
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_Record_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_Record_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("record.xml");
            _ApidazeScript.AddNode(new Answer()).AddNode(Wait.SetDuration(2)).AddNode(
                new Speak { LangEnum = LangEnum.ENGLISH_US, Text = "Please leave a message." }).AddNode(
                new Record { Name = "example1" }).AddNode(Wait.SetDuration(60)).AddNode(new Hangup());
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXml_RecordWithAllAttributes_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXml_RecordWithAllAttributes_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("record-all-attributes.xml");
            _ApidazeScript.AddNode(new Answer()).AddNode(Wait.SetDuration(2)).AddNode(
                new Speak { LangEnum = LangEnum.ENGLISH_US, Text = "Please leave a message." }).AddNode(
                new Record { Name = "example1", OnAnswered = true, ALeg = false, BLeg = false }).AddNode(Wait.SetDuration(60)).AddNode(new Hangup());
            const bool noFormatting = true;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXmlWithNoFormatting_DialNumber_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXmlWithNoFormatting_DialNumber_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("dial-number.xml").RemoveWhiteSpaces();
            var dial = new Dial() { Number = new List<Number>() { new Number("1234567890") } };
            _ApidazeScript.AddNode(dial).AddNode(new Hangup());
            const bool noFormatting = false;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXmlWithNoFormatting_DialSipAccount_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXmlWithNoFormatting_DialSipAccount_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("dial-sipaccount.xml").RemoveWhiteSpaces();
            var dial = new Dial() { Sipaccount = new List<SipAccount>() { new SipAccount("targetsipaccount") } };
            _ApidazeScript.AddNode(dial).AddNode(new Hangup());
            const bool noFormatting = false;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXmlWithNoFormatting_DialSipUri_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXmlWithNoFormatting_DialSipUri_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("dial-sipuri.xml").RemoveWhiteSpaces();
            var dial = new Dial() { SipUri = new List<SipUri>() { new SipUri("phone_number@anysipdomain.com") } };
            _ApidazeScript.AddNode(dial).AddNode(new Hangup());
            const bool noFormatting = false;

            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        /// <summary>
        /// Defines the test method ToXmlWithNoFormatting_DialWithAllAttributesAndDestinationTypes_ReturnsEqualToFile.
        /// </summary>
        [TestMethod]
        public void ToXmlWithNoFormatting_DialWithAllAttributesAndDestinationTypes_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("dial-all-in-one.xml").RemoveWhiteSpaces();
            var dial = new Dial()
            {
                SipUri = new List<SipUri>() { new SipUri("phone_number@anysipdomain.com") },
                Timeout = 60,
                MaxCallDuration = 300,
                Strategy = StrategyEnum.SEQUENCE,
                Action = "http://action.url.com",
                AnswerUrl = "http://answer-url.com",
                CallerHangupUrl = "http://caller-hangup-url.com",
                Number = new List<Number>() { new Number("1234567890") },
                Sipaccount = new List<SipAccount>() { new SipAccount("targetsipaccount") },
            };
            _ApidazeScript.AddNode(dial).AddNode(new Hangup());
            const bool noFormatting = true;


            // Act
            var result = _ApidazeScript.ToXml(noFormatting, true).RemoveWhiteSpaces();

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }
    }
}
