using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E10 : E11
    {
        public static bool TryParse(ParseStack ps, out E10 e10)
        {
            if (ps.CheckCache(out E10 cached))
            {
                e10 = cached;
                return true;
            }
            
            ps.Save();
            if (AndExpression.TryParse(ps, out AndExpression andExpression))
            {
                e10 = andExpression;
                ps.Pop();
                return true;
            }
            else if (E9.TryParse(ps, out E9 e9))
            {
                e10 = e9;
                ps.Pop();
                return true;
            }
            else
            {
                e10 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
