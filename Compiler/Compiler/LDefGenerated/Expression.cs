using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class Expression
    {
        public static bool TryParse(ParseStack ps, out Expression expression)
        {
            if (ps.CheckCache(out Expression cached))
            {
                expression = cached;
                return true;
            }
            
            ps.Save();
            if (E14.TryParse(ps, out E14 e14))
            {
                expression = e14;
                ps.Pop();
                return true;
            }
            else
            {
                expression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
