using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class LessThanOrEqualToExpression : E5
    {
        public E5 Left { get; private set; }
        public LessThanOrEqualToToken LessThanOrEqualTo { get; private set; }
        public E4 Right { get; private set; }
    
        public LessThanOrEqualToExpression (E5 left, LessThanOrEqualToToken lessThanOrEqualTo, E4 right)
        {
            Left = left;
            LessThanOrEqualTo = lessThanOrEqualTo;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out LessThanOrEqualToExpression lessThanOrEqualToExpression)
        {
            if (ps.CheckCache(out LessThanOrEqualToExpression cached))
            {
                lessThanOrEqualToExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E5.TryParse(ps, out E5 left)
                && ps.TryParse(out LessThanOrEqualToToken lessThanOrEqualTo)
                && E4.TryParse(ps, out E4 right))
            {
                lessThanOrEqualToExpression = new LessThanOrEqualToExpression(left, lessThanOrEqualTo, right);
                ps.CacheAndPop(lessThanOrEqualToExpression);
                return true;
            }
            else
            {
                lessThanOrEqualToExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
