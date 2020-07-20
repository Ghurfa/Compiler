using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E12 : E13
    {
        public static bool TryParse(ParseStack ps, out E12 e12)
        {
            if (ps.CheckCache(out E12 cached))
            {
                e12 = cached;
                return true;
            }
            
            ps.Save();
            if (NullCoalescingExpression.TryParse(ps, out NullCoalescingExpression nullCoalescingExpression))
            {
                e12 = nullCoalescingExpression;
                ps.Pop();
                return true;
            }
            else if (E11.TryParse(ps, out E11 e11))
            {
                e12 = e11;
                ps.Pop();
                return true;
            }
            else
            {
                e12 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
