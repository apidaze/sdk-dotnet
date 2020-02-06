using APIdaze.SDK.ScriptsBuilders;
using APIdaze.SDK.ScriptsBuilders.POCO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using static APIdaze.SDK.Tests.Unit.TestUtil;


namespace APIdaze.SDK.Tests.Unit.ScriptsBuilders
{

    [TestClass]
    public class ApidazeScriptTest
    {
        private ApidazeScript _apidazeScript;

        [TestInitialize]
        public void Startup()
        {
            _apidazeScript = new ApidazeScript();
        }

        [TestMethod]
        public void ToXml_Answer_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("answer.xml");
            _apidazeScript.AddNode(new Answer()).AddNode(Playback.FromFile("http://www.mydomain.com/welcome.wav")).AddNode(new Hangup());
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_Playback_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("playback.xml");
            _apidazeScript.AddNode(new Answer()).AddNode(Playback.FromFile("http://www.mydomain.com/welcome.wav")).AddNode(new Hangup());
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_Ringback_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("ringback.xml");
            _apidazeScript.AddNode(new Answer())
                          .AddNode(Ringback.FromFile("http://www.mydomain.com/welcome.wav"))
                          .AddNode(new Dial { Sipaccount = "bob" })
                          .AddNode(new Hangup());
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_Echo_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("echo.xml");
            _apidazeScript.AddNode(new Answer())
                .AddNode(new Echo { Delay = 500 });
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_Hangup_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("hangup.xml");
            _apidazeScript.AddNode(new Hangup());
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_Intercept_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("intercept.xml");
            _apidazeScript.AddNode(new Answer())
                .AddNode(new Intercept { Uuid = new Guid("f28a3e29-dac4-462c-bf94-b1d518ddbe2d") })
                .AddNode(new Hangup());
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_Speak_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("speak.xml").RemoveWhiteSpaces();
            _apidazeScript.AddNode(new Answer())
                .AddNode(new Speak
                {
                    LangEnum = LangEnum.FRENCH_FRANCE,
                    Voice = VoiceEnum.FEMALE_A,
                    Text = "Bonjour et bienvenue chez APIDAIZE. Vous pouvez patienter, mais n'oubliez pas de raccrocher."
                })
                .AddNode(Wait.SetDuration(5));
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true).RemoveWhiteSpaces();

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_BindWithSpeak_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("bind-with-speak.xml").RemoveWhiteSpaces();
            _apidazeScript.AddNode(new Answer())
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
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true).RemoveWhiteSpaces();

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_BindWithPlayback_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("bind-with-playback.xml");
            _apidazeScript.AddNode(new Answer())
                .AddNode(new Playback()
                {
                    InputTimeoutMillis = TimeSpan.FromSeconds(5).TotalMilliseconds,
                    File = "http://www.mydomain.com/welcome.wav",
                    Binds = new List<Bind> {
                        new Bind("http://www.mydomain.com/get_digits.php?bind=1", "1"),
                        new Bind("http://www.mydomain.com/get_digits.php?bind=2", "2"),
                        new Bind("http://www.mydomain.com/get_digits.php?bind=other", "~[3-9]") }

                });
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_Wait_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("wait.xml");
            _apidazeScript.AddNode(new Answer())
                .AddNode(new Speak
                {
                    LangEnum = LangEnum.FRENCH_FRANCE,
                    Text = "Bonjour et bienvenue chez APIDAIZE. Vous pouvez patienter, mais n'oubliez pas de raccrocher."
                }).AddNode(Wait.SetDuration(5));
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_Conference_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("conference.xml");
            _apidazeScript.AddNode(new Speak
            {
                Text = "You will now be placed into the conference"
            }).AddNode(new Conference { Name = "my_meeting_room" });
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_Record_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("record.xml");
            _apidazeScript.AddNode(new Answer()).AddNode(Wait.SetDuration(2)).AddNode(
                new Speak { LangEnum = LangEnum.ENGLISH_US, Text = "Please leave a message." }).AddNode(
                new Record { Name = "example1" }).AddNode(Wait.SetDuration(60)).AddNode(new Hangup());
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXml_RecordWithAllAttributes_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("record-all-attributes.xml");
            _apidazeScript.AddNode(new Answer()).AddNode(Wait.SetDuration(2)).AddNode(
                new Speak { LangEnum = LangEnum.ENGLISH_US, Text = "Please leave a message." }).AddNode(
                new Record { Name = "example1", OnAnswered = true, ALeg = false, BLeg = false }).AddNode(Wait.SetDuration(60)).AddNode(new Hangup());
            var noFormatting = true;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXmlWithNoFormatting_DialNumber_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("dial-number.xml").RemoveWhiteSpaces();
            var dial = new Dial() { Number = "1234567890" };
            _apidazeScript.AddNode(dial).AddNode(new Hangup());
            var noFormatting = false;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXmlWithNoFormatting_DialSipAccount_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("dial-sipaccount.xml").RemoveWhiteSpaces();
            var dial = new Dial() { Sipaccount = "targetsipaccount" };
            _apidazeScript.AddNode(dial).AddNode(new Hangup());
            var noFormatting = false;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXmlWithNoFormatting_DialSipUri_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("dial-sipuri.xml").RemoveWhiteSpaces();
            var dial = new Dial() { SipUri = "phone_number@anysipdomain.com" };
            _apidazeScript.AddNode(dial).AddNode(new Hangup());
            var noFormatting = false;

            // Act
            var result = _apidazeScript.ToXml(noFormatting, true);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [TestMethod]
        public void ToXmlWithNoFormatting_DialWithAllAttributesAndDestinationTypes_ReturnsEqualToFile()
        {
            // Arrange
            var expectedOutput = GetFileContents("dial-all-in-one.xml").RemoveWhiteSpaces();
            var dial = new Dial()
            {
                SipUri = "phone_number@anysipdomain.com",
                Timeout = 60,
                MaxCallDuration = 300,
                Strategy = StrategyEnum.SEQUENCE,
                Action = "http://action.url.com",
                AnswerUrl = "http://answer-url.com",
                CallerHangupUrl = "http://caller-hangup-url.com",
                Number = "1234567890",
                Sipaccount = "targetsipaccount"
            };
            _apidazeScript.AddNode(dial).AddNode(new Hangup());
            var noFormatting = true;


            // Act
            var result = _apidazeScript.ToXml(noFormatting, true).RemoveWhiteSpaces();

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }
    }
}
