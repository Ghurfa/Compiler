using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E13 : E14
    {
        public static bool TryParse(ParseStack ps, out E13 e13)
        {
            if (ps.CheckCache(out E13 cached))
            {
                e13 = cached;
                return true;
            }
            
            ps.Save();
            if (IfExpression.TryParse(ps, out IfExpression ifExpression))
            {
                e13 = ifExpression;
                ps.Pop();
                return true;
            }
            else if (E12.TryParse(ps, out E12 e12))
            {
                e13 = e12;
                ps.Pop();
                return true;
            }
            else
            {
                e13 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
