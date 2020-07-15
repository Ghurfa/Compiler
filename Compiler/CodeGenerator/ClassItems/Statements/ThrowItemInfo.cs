using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.ClassItems.Statements
{
    class ThrowItemInfo : InitialStatementItemInfo
    {
        string ExceptionName;
        string Arguments;
        public ThrowItemInfo(string[] arguments)
        {
            ExceptionName = arguments[0];
            Arguments = "";
            bool firstArgument = true;
            foreach(string argument in arguments.ExceptFirst())
            {
                if (firstArgument) firstArgument = false;
                else Arguments += ", ";
                Arguments += argument;
            }
        }
        public override string[] GetCreationStatements() => new string[] { $"throw new {ExceptionName}Exception();" };
    }
}
