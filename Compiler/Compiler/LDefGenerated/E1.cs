using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E1 : E2
    {
        public static bool TryParse(ParseStack ps, out E1 e1)
        {
            if (ps.CheckCache(out E1 cached))
            {
                e1 = cached;
                return true;
            }
            
            ps.Save();
            if (CastExpression.TryParse(ps, out CastExpression castExpression))
            {
                e1 = castExpression;
                ps.Pop();
                return true;
            }
            else if (UnaryExpression.TryParse(ps, out UnaryExpression unaryExpression))
            {
                e1 = unaryExpression;
                ps.Pop();
                return true;
            }
            else
            {
                e1 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
