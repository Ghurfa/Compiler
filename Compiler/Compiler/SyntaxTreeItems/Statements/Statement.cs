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
                case TokenType.IfKeyword: return new IfBlock(tokens);
                case TokenType.WhileKeyword: return new WhileBlock(tokens);
                case TokenType.ForKeyword: return new ForBlock(tokens);
                case TokenType.ReturnKeyword: return new ReturnStatement(tokens);
                case TokenType.ForeachKeyword:
                case TokenType.BreakKeyword:
                case TokenType.ContinueKeyword:
                case TokenType.ThrowKeyword:
                    throw new NotImplementedException();
                case TokenType.OpenCurly: return new CodeBlock(tokens);
                case TokenType.Semicolon: return new EmptyStatement(tokens);
                default:
                    return new ExpressionStatement(tokens);
            }
        }
    }
}
