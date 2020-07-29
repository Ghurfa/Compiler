using Parser.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public abstract class Statement
    {
        public static Statement ReadStatement(TokenCollection tokens)
        {
            switch (tokens.PeekToken())
            {
                case IfKeywordToken _: return new IfBlock(tokens);
                case WhileKeywordToken _: return new WhileBlock(tokens);
                case ForKeywordToken _: return new ForBlock(tokens);
                case ExitKeywordToken _: return new ExitStatement(tokens);
                case ReturnKeywordToken _: return new ReturnStatement(tokens);
                case ForeachKeywordToken _:
                case SwitchKeywordToken _:
                case BreakKeywordToken _:
                case ContinueKeywordToken _:
                case ThrowKeywordToken _:
                    throw new NotImplementedException();
                case OpenCurlyToken _: return new CodeBlock(tokens);
                case SemicolonToken _: return new EmptyStatement(tokens);
                default: return new ExpressionStatement(tokens);
            }
        }
    }
}
