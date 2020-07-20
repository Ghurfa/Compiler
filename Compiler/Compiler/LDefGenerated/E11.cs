using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E11 : E12
    {
        public static bool TryParse(ParseStack ps, out E11 e11)
        {
            if (ps.CheckCache(out E11 cached))
            {
                e11 = cached;
                return true;
            }
            
            ps.Save();
            if (OrExpression.TryParse(ps, out OrExpression orExpression))
            {
                e11 = orExpression;
                ps.Pop();
                return true;
            }
            else if (E10.TryParse(ps, out E10 e10))
            {
                e11 = e10;
                ps.Pop();
                return true;
            }
            else
            {
                e11 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
