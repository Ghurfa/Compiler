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
    public class InvalidTokenException : SyntaxTreeBuildingException
    {
        public readonly IToken Token;
        public InvalidTokenException(IToken token)
            : base($"Unexpected token of type {token.GetType().Name}") { Token = token; }
        public InvalidTokenException(IToken token, string message)
            : base(message) { Token = token; }
    }
    public class InvalidEndOfStatementException : InvalidTokenException
    {
        public InvalidEndOfStatementException(IToken token)
            : base(token, $"Invalid end of statement or field declarator. Expected ';' or line break") { }
    }
    public class MissingWhitespaceException : InvalidTokenException
    {
        public MissingWhitespaceException(IToken token)
            : base(token, $"Expected whitespace") { }
    }
    public class MissingLineBreakException : InvalidTokenException
    {
        public MissingLineBreakException(IToken token)
            : base(token, $"Expected a line break between non-perenthesized condition and embedded statement") { }
    }
    public class InvalidStatementException : SyntaxTreeBuildingException
    {
        public readonly Expression Expression;
        public InvalidStatementException(Expression expr)
            : base($"Expression ({expr.ToString()}) is not a valid statement") { Expression = expr; }
    }
}
