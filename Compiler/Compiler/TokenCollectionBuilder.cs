using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    class TokenCollectionBuilder
    {
        LinkedList<IToken> tokens;
        int index;
        public TokenCollectionBuilder()
        {
            tokens = new LinkedList<IToken>();
            index = 0;
        }

        public void Add(string text, TokenType type)
        {
            tokens.AddLast(CreateToken(text, type));
            index++;
        }

        public IToken CreateToken(string text, TokenType type)
        {
            switch(type)
            {
                case TokenType.Identifier: return new IdentifierToken(text, index);
                case TokenType.NamespaceKeyword: return new NamespaceKeywordToken(text, index);
                case TokenType.ClassKeyword: return new ClassKeywordToken(text, index);
                case TokenType.ConstructorKeyword: return new ConstructorKeywordToken(text, index);
                case TokenType.Modifier: return new ModifierToken(text, index);
                case TokenType.PrimitiveType: return new PrimitiveTypeToken(text, index);
                case TokenType.ValueKeyword: return new ValueKeywordToken(text, index);
                case TokenType.IfKeyword: return new IfKeywordToken(text, index);
                case TokenType.ElseKeyword: return new ElseKeywordToken(text, index);
                case TokenType.WhileKeyword: return new WhileKeywordToken(text, index);
                case TokenType.ForKeyword: return new ForKeywordToken(text, index);
                case TokenType.ForeachKeyword: return new ForeachKeywordToken(text, index);
                case TokenType.SwitchKeyword: return new SwitchKeywordToken(text, index);
                case TokenType.ReturnKeyword: return new ReturnKeywordToken(text, index);
                case TokenType.ExitKeyword: return new ExitKeywordToken(text, index);
                case TokenType.BreakKeyword: return new BreakKeywordToken(text, index);
                case TokenType.ContinueKeyword: return new ContinueKeywordToken(text, index);
                case TokenType.ThrowKeyword: return new ThrowKeywordToken(text, index);
                case TokenType.AsKeyword: return new AsKeywordToken(text, index);
                case TokenType.NewKeyword: return new NewKeywordToken(text, index);
                case TokenType.InKeyword: return new InKeywordToken(text, index);
                case TokenType.SingleLineComment: return new SingleLineCommentToken(text, index);
                case TokenType.MultiLineComment: return new MultiLineCommentToken(text, index);
                case TokenType.Whitespace: return new WhitespaceToken(text, index);
                case TokenType.WhitespaceWithLineBreak: return new WhitespaceWithLineBreakToken(text, index);
                case TokenType.Dot: return new DotToken(text, index);
                case TokenType.Comma: return new CommaToken(text, index);
                case TokenType.OpenPeren: return new OpenPerenToken(text, index);
                case TokenType.ClosePeren: return new ClosePerenToken(text, index);
                case TokenType.OpenBracket: return new OpenBracketToken(text, index);
                case TokenType.CloseBracket: return new CloseBracketToken(text, index);
                case TokenType.OpenCurly: return new OpenCurlyToken(text, index);
                case TokenType.CloseCurly: return new CloseCurlyToken(text, index);
                case TokenType.QuestionMark: return new QuestionMarkToken(text, index);
                case TokenType.Colon: return new ColonToken(text, index);
                case TokenType.Semicolon: return new SemicolonToken(text, index);
                case TokenType.SingleQuote: return new SingleQuoteToken(text, index);
                case TokenType.DoubleQuote: return new DoubleQuoteToken(text, index);
                case TokenType.Backslash: return new BackslashToken(text, index);
                case TokenType.NullCondDot: return new NullCondDotToken(text, index);
                case TokenType.NullCondOpenBracket: return new NullCondOpenBracketToken(text, index);
                case TokenType.Not: return new NotToken(text, index);
                case TokenType.BitwiseNot: return new BitwiseNotToken(text, index);
                case TokenType.Increment: return new IncrementToken(text, index);
                case TokenType.Decrement: return new DecrementToken(text, index);
                case TokenType.Plus: return new PlusToken(text, index);
                case TokenType.Minus: return new MinusToken(text, index);
                case TokenType.Asterisk: return new AsteriskToken(text, index);
                case TokenType.Divide: return new DivideToken(text, index);
                case TokenType.Modulo: return new ModuloToken(text, index);
                case TokenType.BitwiseAnd: return new BitwiseAndToken(text, index);
                case TokenType.BitwiseOr: return new BitwiseOrToken(text, index);
                case TokenType.BitwiseXor: return new BitwiseXorToken(text, index);
                case TokenType.LeftShift: return new LeftShiftToken(text, index);
                case TokenType.RightShift: return new RightShiftToken(text, index);
                case TokenType.NullCoalescing: return new NullCoalescingToken(text, index);
                case TokenType.Assign: return new AssignToken(text, index);
                case TokenType.DeclAssign: return new DeclAssignToken(text, index);
                case TokenType.PlusAssign: return new PlusAssignToken(text, index);
                case TokenType.MinusAssign: return new MinusAssignToken(text, index);
                case TokenType.MultiplyAssign: return new MultiplyAssignToken(text, index);
                case TokenType.DivideAssign: return new DivideAssignToken(text, index);
                case TokenType.ModuloAssign: return new ModuloAssignToken(text, index);
                case TokenType.BitwiseAndAssign: return new BitwiseAndAssignToken(text, index);
                case TokenType.BitwiseOrAssign: return new BitwiseOrAssignToken(text, index);
                case TokenType.BitwiseXorAssign: return new BitwiseXorAssignToken(text, index);
                case TokenType.LeftShiftAssign: return new LeftShiftAssignToken(text, index);
                case TokenType.RightShiftAssign: return new RightShiftAssignToken(text, index);
                case TokenType.NullCoalescingAssign: return new NullCoalescingAssignToken(text, index);
                case TokenType.Equals: return new EqualsToken(text, index);
                case TokenType.NotEquals: return new NotEqualsToken(text, index);
                case TokenType.GreaterThan: return new GreaterThanToken(text, index);
                case TokenType.LessThan: return new LessThanToken(text, index);
                case TokenType.GreaterThanOrEqualTo: return new GreaterThanOrEqualToToken(text, index);
                case TokenType.LessThanOrEqualTo: return new LessThanOrEqualToToken(text, index);
                case TokenType.And: return new AndToken(text, index);
                case TokenType.Or: return new OrToken(text, index);
                case TokenType.IntLiteral: return new IntLiteralToken(text, index);
                case TokenType.StringLiteral: return new StringLiteralToken(text, index);
                case TokenType.CharLiteral: return new CharLiteralToken(text, index);
                case TokenType.TrueKeyword: return new TrueKeywordToken(text, index);
                case TokenType.FalseKeyword: return new FalseKeywordToken(text, index);
                default: throw new NotImplementedException();
            }
        }

        public TokenCollection GetCollection()
        {
            return new TokenCollection(tokens);
        }
    }
}
