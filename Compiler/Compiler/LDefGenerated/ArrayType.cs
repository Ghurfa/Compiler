using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class ArrayType : Type
    {
        public NonArrayType BaseType { get; private set; }
        public ArrayBrackets[] Brackets { get; private set; }
    
        public ArrayType (NonArrayType baseType, ArrayBrackets[] brackets)
        {
            BaseType = baseType;
            Brackets = brackets;
        }
    
        public static bool TryParse(ParseStack ps, out ArrayType arrayType)
        {
            if (ps.CheckCache(out ArrayType cached))
            {
                arrayType = cached;
                return true;
            }
            
            ps.Save();
            if (NonArrayType.TryParse(ps, out NonArrayType baseType)
                && TryParseBrackets(out ArrayBrackets[] brackets))
            {
                arrayType = new ArrayType(baseType, brackets);
                ps.CacheAndPop(arrayType);
                return true;
            }
            else
            {
                arrayType = null;
                ps.Restore();
                return false;
            }
        }
    
        private static bool TryParseBrackets(ParseStack ps, out ArrayBrackets[] brackets)
        {
            List<ArrayBrackets> items = new List<ArrayBrackets
            while (ArrayBrackets.TryParse(ps, out ArrayBrackets arrayBrackets)
            {
            items.Add(arrayBrackets);
            }
            brackets = items.ToArray();
            return true;
        }
    }
}
