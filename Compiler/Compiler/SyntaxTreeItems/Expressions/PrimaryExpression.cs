using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class PrimaryExpression : Expression
    {
        public PrimaryExpression ReadPrimaryExpression(LinkedList<Token> tokens)
        {
            Token nextToken;
            PrimaryExpression exprSoFar;
            while (true)
            {
                if (tokens.PopIfMatches(out Token openPeren, TokenType.SyntaxChar))
                {
                    throw new NotImplementedException();
                }
                else
                {
                    PrimaryExpression baseExpr;
                    if (tokens.PopIfMatches(out Token newKeyword, TokenType.NewKeyword))
                    {
                        //baseExpr = newInstanceExpr
                        throw new NotImplementedException();
                    }
                    else if (tokens.PopIfMatches(out Token identifier, TokenType.Identifier))
                    {
                        //baseExpr = identifierExpr
                    }
                    else if (tokens.PopIfMatches(out Token valueKeyword, TokenType.ValueKeyword))
                    {
                        //baseExpr = valueKeywordExpr  (this, base, etc)
                    }
                    else if (tokens.PopIfMatches(out Token primitiveKeyword, TokenType.PrimitiveType))
                    {
                        //baseExpr = primitiveTypeExpr  (int)
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    /* if (tokens.PopIfMatches(out incrOperator, TokenType.Operator, "++"))
                     {
                         //return new incrExpr(baseExpr)
                     }
                     else if (tokens.PopIfMatches(out incrOperator, TokenType.Operator, "["))
                     {
                         //return new incrExpr(baseExpr)
                     }*/
                    throw new NotImplementedException();
                }
            }
        }
    }
}
