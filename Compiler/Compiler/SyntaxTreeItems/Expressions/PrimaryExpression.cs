using Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class PrimaryExpression : Expression
    {
        public static PrimaryExpression ReadPrimaryExpression(LinkedList<Token> tokens)
        {
            PrimaryExpression baseExpr;
            if (tokens.PopIfMatches(out Token newKeyword, TokenType.NewKeyword))
            {
                baseExpr = new NewObjectExpression(tokens, newKeyword);
            }
            else if (tokens.PopIfMatches(out Token identifier, TokenType.Identifier))
            {
                baseExpr = new IdentifierExpression(tokens, identifier);
            }
            else if (tokens.PopIfMatches(out Token intToken, TokenType.IntLiteral))
            {
                baseExpr = new IntLiteral(tokens, intToken);
            }
            else if (tokens.PopIfMatches(out Token strOpenToken, TokenType.SyntaxChar, "\""))
            {
                baseExpr = new StringLiteral(tokens, strOpenToken);
            }
            else if (tokens.PopIfMatches(out Token charOpenToken, TokenType.SyntaxChar, "'"))
            {
                baseExpr = new CharLiteral(tokens, charOpenToken);
            }
            else if (tokens.PopIfMatches(out Token valueKeyword, TokenType.ValueKeyword))
            {
                baseExpr = new ValueKeywordExpression(tokens, valueKeyword);
            }
            else if (tokens.PopIfMatches(out Token primitiveKeyword, TokenType.PrimitiveType))
            {
                baseExpr = new PrimitiveTypeExpression(tokens, primitiveKeyword);
            }
            else throw new SyntaxTreeBuildingException();

            PrimaryExpression exprSoFar = baseExpr;
            bool finishedParsing = false;
            while (!finishedParsing)
            {
                if (tokens.PopIfMatches(out Token dot, TokenType.Operator, "."))
                {
                    exprSoFar = new MemberAccessExpression(tokens, exprSoFar, dot);
                }
                else if (tokens.PopIfMatches(out Token openPerentheses, TokenType.Operator, "("))
                {
                    exprSoFar = new MethodCallExpression(tokens, exprSoFar, openPerentheses);
                }
                else if (tokens.PopIfMatches(out Token openArrBracket, TokenType.Operator, "["))
                {
                    throw new NotImplementedException();
                }
                else if (tokens.PopIfMatches(out Token incrOperator, TokenType.Operator, "++"))
                {
                    throw new NotImplementedException();
                }
                else finishedParsing = true;
            }
            return exprSoFar;
        }
    }
}
