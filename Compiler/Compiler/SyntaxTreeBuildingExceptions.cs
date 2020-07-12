using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public abstract class SyntaxTreeBuildingException : InvalidOperationException
    {
        protected SyntaxTreeBuildingException(string message) : base(message) { }
    }
    public class UnexpectedEndOfFrameException : SyntaxTreeBuildingException
    {
        public UnexpectedEndOfFrameException()
            : base("Reached the end of the frame unexpectedly") { }
    }
    public abstract class SyntaxException : SyntaxTreeBuildingException
    {
        public readonly Token Token;
        public SyntaxException(Token token, string message)
            : base(message) { Token = token; }
    }
    public class UnexpectedToken : SyntaxException
    {
        public UnexpectedToken(Token token)
            : base(token, $"Unexpected token of type {token.Type.ToString()}") { }
    }
    public class InvalidEndOfStatement : SyntaxException
    {
        public InvalidEndOfStatement(Token token)
            : base(token, $"Invalid end of statement. Expected ';' or line break") { }
    }
    public class InvalidStatement : SyntaxException
    {
        public InvalidStatement(Token token)
            : base(token, $"Expression is not a valid statement") { }
    }
}
