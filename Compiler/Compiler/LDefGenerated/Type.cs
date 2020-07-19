using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class Type
    {
        public static bool TryParse(ParseStack ps, out Type type)
        {
            if (ps.CheckCache(out Type cached))
            {
                type = cached;
                return true;
            }
            
            ps.Save();
            if (ArrayType.TryParse(ps, out ArrayType arrayType)
            {
                type = arrayType;
                ps.Pop();
                return true;
            }
            else if (NonArrayType.TryParse(ps, out NonArrayType nonArrayType)
            {
                type = nonArrayType;
                ps.Pop();
                return true;
            }
            else
            {
                type = null;
                ps.Restore();
                return false;
            }
        }
    }
}
