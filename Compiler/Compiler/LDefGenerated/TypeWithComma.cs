using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class TypeWithComma
    {
        public Type Type { get; private set; }
        public CommaToken Comma { get; private set; }
    
        public TypeWithComma (Type type, CommaToken comma)
        {
            Type = type;
            Comma = comma;
        }
    
        public static bool TryParse(ParseStack ps, out TypeWithComma typeWithComma)
        {
            if (ps.CheckCache(out TypeWithComma cached))
            {
                typeWithComma = cached;
                return true;
            }
            
            ps.Save();
            if (Type.TryParse(ps, out Type type)
                && ps.TryParse(out CommaToken comma))
            {
                typeWithComma = new TypeWithComma(type, comma);
                ps.CacheAndPop(typeWithComma);
                return true;
            }
            else
            {
                typeWithComma = null;
                ps.Restore();
                return false;
            }
        }
    }
}
