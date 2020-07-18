using Compiler;
using Compiler.SyntaxTreeItems;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CompilerUnitTests
{
    public class ExpressionParsingTests
    {
        //[Theory]
        //[InlineData("a + b - c * e / f + d", )]
        public void TestSimpleExpressionParsing(string text)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void TestRandomlyGeneratedExpressions()
        {
            Random random = new Random();
            int count = 50;
            string[] exprsStrings = new string[count];
            for (int i = 0; i < count; i++)
            {
                LinkedList<IToken> tokens = new LinkedList<IToken>();
                int tokenPointer = 0;
                GenerateRandomExpression(i, ref tokenPointer, tokens, random);

                Expression total = Expression.ReadExpression(new TokenCollection(tokens));
                exprsStrings[i] = total.ToString();

                Queue<Expression> expressions = new Queue<Expression>();
                expressions.Enqueue(total);
                while (expressions.Count > 0)
                {
                    Expression expr = expressions.Dequeue();
                    if (expr is UnaryExpression) continue;

                    bool leftAssoc = expr.Precedence != 12 && expr.Precedence != 13 && expr.Precedence != 14;

                    Assert.True(ProperPrecedence(expr, expr.LeftExpr, leftAssoc));
                    Assert.True(ProperPrecedence(expr, expr.RightExpr, !leftAssoc));

                    expressions.Enqueue(expr.LeftExpr);
                    expressions.Enqueue(expr.RightExpr);
                    if (expr is IfExpression ifExpr) expressions.Enqueue(ifExpr.IfTrue);
                }
            }
            File.WriteAllLines(@"..\..\..\..\..\TestedExpressions.txt", exprsStrings);
        }

        private bool ProperPrecedence(Expression upper, Expression lower, bool mayEqual)
        {
            return lower.Precedence < upper.Precedence || (mayEqual && lower.Precedence == upper.Precedence);
        }

        private void GenerateRandomExpression(int maxLength, ref int tokenPointer, LinkedList<IToken> tokens, Random random)
        {
            int start = tokenPointer;
            while (tokenPointer - start < maxLength)
            {
                int choice = random.Next(20);

                tokens.AddLast(new IdentifierToken("a", tokenPointer++));
                if (choice == 19)
                {
                    tokens.AddLast(new QuestionMarkToken("?", tokenPointer++));
                    GenerateRandomExpression(maxLength / 4, ref tokenPointer, tokens, random);
                    tokens.AddLast(new BackslashToken("\\", tokenPointer++));
                }
                else
                {
                    tokens.AddLast(GetBinaryOperator(choice, tokenPointer++));
                }
            }
            tokens.AddLast(new IdentifierToken("a", tokenPointer++));
        }

        private IToken GetBinaryOperator(int choice, int i)
        {
            switch (choice)
            {
                case 0: return new PlusToken("+", i);
                case 1: return new MinusToken("-", i);
                case 2: return new AsteriskToken("*", i);
                case 3: return new DivideToken("/", i);
                case 4: return new ModuloToken("%", i);
                case 5: return new BitwiseAndToken("&", i);
                case 6: return new BitwiseOrToken("|", i);
                case 7: return new BitwiseXorToken("^", i);
                case 8: return new LeftShiftToken("<<", i);
                case 9: return new RightShiftToken(">>", i);
                case 10: return new NullCoalescingToken("??", i);
                case 11: return new EqualsToken("==", i);
                case 12: return new NotEqualsToken("!=", i);
                case 13: return new GreaterThanToken(">", i);
                case 14: return new LessThanToken("<", i);
                case 15: return new GreaterThanOrEqualToToken(">=", i);
                case 16: return new LessThanOrEqualToToken("<=", i);
                case 17: return new AndToken("&&", i);
                case 18: return new OrToken("||", i);
                default: throw new InvalidOperationException();
            }
        }
    }
}
