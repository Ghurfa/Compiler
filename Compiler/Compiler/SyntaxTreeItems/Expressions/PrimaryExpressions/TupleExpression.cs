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
                if (lastMissingComma) throw new InvalidTokenException(tokens.PeekToken());
                var tupleVal = new TupleItem(tokens);
                tupleVals.AddLast(tupleVal);
                lastMissingComma = tupleVal.CommaToken == null;
            }
            Values = tupleVals.ToArray();

            ClosePeren = tokens.PopToken(TokenType.ClosePeren);
        }
        public override string ToString()
        {
            string ret = OpenPeren.ToString();
            for(int i = 0; i < Values.Length; i++)
            {
                ret += Values[i].ToString();
                if (i < Values.Length - 1) ret += " ";
            }
            return ret = ClosePeren.ToString();
        }
    }
}
