using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;

namespace ClarionAddins.CodeSnippets
{
    class ClarionAddinsStringTagProvider : IStringTagProvider
    {
        public string Convert(string tag)
        {
            switch (tag.ToUpperInvariant())
            {
                case "PROMPT":
                    return GetPromptResult();
                case "PROMPT_VALUE":
                    return PropertyService.Get("ClarionAddins.CodeSnippets.Prompt", "");
                case "CLARIONADDINS":
                    return "http://clarionaddins.com";
            }
            return String.Empty;
        }

        private string GetPromptResult()
        {
            string promptVal = MessageService.ShowInputBox("Code Snippet", "Enter snippet value:", "");
            PropertyService.Set("ClarionAddins.CodeSnippets.Prompt", promptVal);
            return promptVal;
        }

        readonly static string[] tags = new string[] {
            "Prompt", "Prompt_Value", "ClarionAddins"
        };

        public string[] Tags
        {
            get
            {
                return tags;
            }
        }
    }
}
