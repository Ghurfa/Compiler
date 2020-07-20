using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class NotExpression : UnaryExpression
    {
        public NotToken Not { get; private set; }
        public UnaryExpression UnaryExpression { get; private set; }
    
        public NotExpression (NotToken not, UnaryExpression unaryExpression)
        {
            Not = not;
            UnaryExpression = unaryExpression;
        }
    
        public static bool TryParse(ParseStack ps, out NotExpression notExpression)
        {
            if (ps.CheckCache(out NotExpression cached))
            {
                notExpression = cached;
                return true;
            }
            
            ps.Save();
            if (ps.TryParse(out NotToken not)
                && UnaryExpression.TryParse(ps, out UnaryExpression unaryExpression))
            {
                notExpression = new NotExpression(not, unaryExpression);
                ps.CacheAndPop(notExpression);
                return true;
            }
            else
            {
                notExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
