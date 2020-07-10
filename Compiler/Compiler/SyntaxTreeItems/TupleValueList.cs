using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TupleValueList
    {
        public readonly TupleValue[] TupleValues;
        public TupleValueList(LinkedList<Token> tokens, TupleValue firstValue = null)
        {
            var tupleVals = new LinkedList<TupleValue>();
            if (firstValue != null) tupleVals.AddLast(firstValue);
            bool lastMissingComma = false;
            while (!lastMissingComma)
            {
                if (lastMissingComma) throw new SyntaxTreeBuildingException(tokens.First.Value);
                var tupleVal = new TupleValue(tokens);
                tupleVals.AddLast(tupleVal);
                lastMissingComma = tupleVal.CommaToken == null;
            }
        }
    }
}
