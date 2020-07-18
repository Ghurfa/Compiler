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
        public InvalidTokenException(TokenCollection tokens)

            : base($"Unexpected token of type {tokens.PeekToken().GetType().Name}")
        {
            Token = tokens.PeekToken();
        }
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
    public class MissingCommaException : InvalidTokenException
    {
        public MissingCommaException(IToken token)
            : base(token, $"Missing a comma between items in argument list, parameter list, tuple type list, or tuple expression") { }
        public MissingCommaException(TokenCollection tokens)
            : base(tokens.PeekToken(), $"Missing a comma between items in argument list, parameter list, tuple type list, or tuple expression") { }
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
            : base($"Expression {expr.ToString()} is not a valid statement") { Expression = expr; }
    }

    public class InvalidAssignmentLeftException : SyntaxTreeBuildingException
    {
        public readonly Expression Expression;
        public InvalidAssignmentLeftException(Expression expr)
            : base($"{expr.ToString()} is not valid as the left-hand side of an assignment") { Expression = expr; }
    }

    public class InvalidIncrDecrOperand : SyntaxTreeBuildingException
    {
        public readonly Expression Expression;
        public InvalidIncrDecrOperand(Expression expr)
            : base($"{expr.ToString()} is not valid as the operand of an increment or decrement expression") { Expression = expr; }
    }
}
