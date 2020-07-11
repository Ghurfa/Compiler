﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class ArrayAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression Array;
        public readonly Token OpenBracket;
        public readonly Expression Index;
        public readonly Token CloseBracket;

        public ArrayAccessExpression(TokenCollection tokens, PrimaryExpression baseExpr)
        {
            Array = baseExpr;
            OpenBracket = tokens.PopToken(TokenType.OpenBracket);
            Index = Expression.ReadExpression(tokens);
            CloseBracket = tokens.PopToken(TokenType.CloseBracket);
        }
    }
}