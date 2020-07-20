using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class UnaryExpression : E1
    {
        public static bool TryParse(ParseStack ps, out UnaryExpression unaryExpression)
        {
            if (ps.CheckCache(out UnaryExpression cached))
            {
                unaryExpression = cached;
                return true;
            }
            
            ps.Save();
            if (NotExpression.TryParse(ps, out NotExpression notExpression))
            {
                unaryExpression = notExpression;
                ps.Pop();
                return true;
            }
            else
            {
                unaryExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
