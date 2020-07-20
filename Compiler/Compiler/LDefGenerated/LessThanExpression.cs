using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class LessThanExpression : E5
    {
        public E5 Left { get; private set; }
        public LessThanToken LessThan { get; private set; }
        public E4 Right { get; private set; }
    
        public LessThanExpression (E5 left, LessThanToken lessThan, E4 right)
        {
            Left = left;
            LessThan = lessThan;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out LessThanExpression lessThanExpression)
        {
            if (ps.CheckCache(out LessThanExpression cached))
            {
                lessThanExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E5.TryParse(ps, out E5 left)
                && ps.TryParse(out LessThanToken lessThan)
                && E4.TryParse(ps, out E4 right))
            {
                lessThanExpression = new LessThanExpression(left, lessThan, right);
                ps.CacheAndPop(lessThanExpression);
                return true;
            }
            else
            {
                lessThanExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
