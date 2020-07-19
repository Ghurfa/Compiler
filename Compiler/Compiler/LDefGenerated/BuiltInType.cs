using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class BuiltInType : NonArrayType
    {
        public PrimitiveTypeToken PrimitiveType { get; private set; }
    
        public BuiltInType (PrimitiveTypeToken primitiveType)
        {
            PrimitiveType = primitiveType;
        }
    
        public static bool TryParse(ParseStack ps, out BuiltInType builtInType)
        {
            if (ps.CheckCache(out BuiltInType cached))
            {
                builtInType = cached;
                return true;
            }
            
            ps.Save();
            if (ps.TryParse(out PrimitiveTypeToken primitiveType))
            {
                builtInType = new BuiltInType(primitiveType);
                ps.CacheAndPop(builtInType);
                return true;
            }
            else
            {
                builtInType = null;
                ps.Restore();
                return false;
            }
        }
    }
}
