using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class NotEqualsExpression : E6
    {
        public E6 Left { get; private set; }
        public NotEqualsToken NotEquals { get; private set; }
        public E5 Right { get; private set; }
    
        public NotEqualsExpression (E6 left, NotEqualsToken notEquals, E5 right)
        {
            Left = left;
            NotEquals = notEquals;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out NotEqualsExpression notEqualsExpression)
        {
            if (ps.CheckCache(out NotEqualsExpression cached))
            {
                notEqualsExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E6.TryParse(ps, out E6 left)
                && ps.TryParse(out NotEqualsToken notEquals)
                && E5.TryParse(ps, out E5 right))
            {
                notEqualsExpression = new NotEqualsExpression(left, notEquals, right);
                ps.CacheAndPop(notEqualsExpression);
                return true;
            }
            else
            {
                notEqualsExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
