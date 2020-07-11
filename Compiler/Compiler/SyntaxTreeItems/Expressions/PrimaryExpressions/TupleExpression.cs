using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class TupleExpression : PrimaryExpression
    {
        public readonly Token OpenPeren;
        public readonly TupleItem[] Values;
        public readonly Token ClosePeren;

        public TupleExpression(TokenCollection tokens, Token openPerens, Expression firstValue, Token firstComma)
        {
            OpenPeren = openPerens;

            var tupleVals = new LinkedList<TupleItem>();
            tupleVals.AddLast(new TupleItem(firstValue, firstComma));

            bool lastMissingComma = false;
            while (!lastMissingComma)
            {
                if (lastMissingComma) throw new SyntaxTreeBuildingException(tokens.PeekToken());
                var tupleVal = new TupleItem(tokens);
                tupleVals.AddLast(tupleVal);
                lastMissingComma = tupleVal.CommaToken == null;
            }
            Values = tupleVals.ToArray();

            ClosePeren = tokens.PopToken(TokenType.ClosePeren);
        }
    }
}
