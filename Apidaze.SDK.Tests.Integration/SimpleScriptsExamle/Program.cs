using APIdaze.SDK.ScriptsBuilders;
using APIdaze.SDK.ScriptsBuilders.POCO;
using Newtonsoft.Json;
using System;

namespace SimpleScriptsExamle
{
    class Program
    {
        static void Main(string[] args)
        {
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

            Console.WriteLine("Script builded {0}", step1);

        }
    }
}
