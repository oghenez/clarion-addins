using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;

namespace ClarionAddins.CodeSnippets
{
    class RegisterStringTagProvider : AbstractCommand
    {
        public override void Run()
        {
            StringParser.RegisterStringTagProvider(new ClarionAddinsStringTagProvider());
        }
    }
}
