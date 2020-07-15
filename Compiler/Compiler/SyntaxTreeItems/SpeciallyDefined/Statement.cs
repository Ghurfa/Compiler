using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public abstract class Statement
    {
        public abstract IToken LeftToken { get; }
        public abstract IToken RightToken { get; }
        public static Statement ReadStatement(TokenCollection tokens)
        {
            switch (tokens.PeekToken())
            {
                case IfKeywordToken _: return new IfStatement(tokens);
                case WhileKeywordToken _: return new WhileStatement(tokens);
                case ForKeywordToken _: return new ForStatement(tokens);
                case ReturnKeywordToken _: return new ReturnStatement(tokens);
                case ForeachKeywordToken _: return new ForeachStatement(tokens);
                case SwitchKeywordToken _: return new SwitchStatement(tokens);
                case BreakKeywordToken _: return new BreakStatement(tokens);
                case ContinueKeywordToken _: return new ContinueStatement(tokens);
                case ThrowKeywordToken _: return new ThrowStatement(tokens);
                case OpenCurlyToken _: return new CodeBlock(tokens);
                case SemicolonToken _: return new EmptyStatement(tokens);
                default:
                    return new ExpressionStatement(tokens);
            }
        }
    }
}