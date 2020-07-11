using Compiler.SyntaxTreeItems.Expressions;
using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public abstract class Statement
    {
        public static Statement ReadStatement(TokenCollection tokens)
        {
            switch(tokens.PeekToken().Type)
            {
                case TokenType.IfKeyword:
                case TokenType.ForKeyword:
                case TokenType.ForeachKeyword:
                case TokenType.WhileKeyword:
                case TokenType.ReturnKeyword:
                case TokenType.BreakKeyword:
                case TokenType.ContinueKeyword:
                    throw new NotImplementedException();
                case TokenType.OpenCurly: return new CodeBlock(tokens);
                case TokenType.Semicolon: return new EmptyStatement(tokens);
                default:
                    PrimaryExpression startingExpression = PrimaryExpression.ReadPrimaryExpression(tokens);
                    throw new NotImplementedException();
            }
        }
    }
}
