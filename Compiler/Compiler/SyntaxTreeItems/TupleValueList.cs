using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Compiler.SyntaxTreeItems
{
    public class TupleValueList
    {
        public readonly TupleValue[] TupleValues;
        public TupleValueList(TokenCollection tokens, TupleValue firstValue = null)
        {
            var tupleVals = new LinkedList<TupleValue>();
            if (firstValue != null) tupleVals.AddLast(firstValue);
            bool lastMissingComma = false;
            while (!lastMissingComma)
            {
                if (lastMissingComma) throw new SyntaxTreeBuildingException(tokens.PeekToken());
                var tupleVal = new TupleValue(tokens);
                tupleVals.AddLast(tupleVal);
                lastMissingComma = tupleVal.CommaToken == null;
            }
        }
    }
}
