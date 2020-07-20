using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class GreaterThanOrEqualToExpression : E5
    {
        public E5 Left { get; private set; }
        public GreaterThanOrEqualToToken GreaterThanOrEqualTo { get; private set; }
        public E4 Right { get; private set; }
    
        public GreaterThanOrEqualToExpression (E5 left, GreaterThanOrEqualToToken greaterThanOrEqualTo, E4 right)
        {
            Left = left;
            GreaterThanOrEqualTo = greaterThanOrEqualTo;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out GreaterThanOrEqualToExpression greaterThanOrEqualToExpression)
        {
            if (ps.CheckCache(out GreaterThanOrEqualToExpression cached))
            {
                greaterThanOrEqualToExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E5.TryParse(ps, out E5 left)
                && ps.TryParse(out GreaterThanOrEqualToToken greaterThanOrEqualTo)
                && E4.TryParse(ps, out E4 right))
            {
                greaterThanOrEqualToExpression = new GreaterThanOrEqualToExpression(left, greaterThanOrEqualTo, right);
                ps.CacheAndPop(greaterThanOrEqualToExpression);
                return true;
            }
            else
            {
                greaterThanOrEqualToExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
