using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class GreaterThanExpression : E5
    {
        public E5 Left { get; private set; }
        public GreaterThanToken GreaterThan { get; private set; }
        public E4 Right { get; private set; }
    
        public GreaterThanExpression (E5 left, GreaterThanToken greaterThan, E4 right)
        {
            Left = left;
            GreaterThan = greaterThan;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out GreaterThanExpression greaterThanExpression)
        {
            if (ps.CheckCache(out GreaterThanExpression cached))
            {
                greaterThanExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E5.TryParse(ps, out E5 left)
                && ps.TryParse(out GreaterThanToken greaterThan)
                && E4.TryParse(ps, out E4 right))
            {
                greaterThanExpression = new GreaterThanExpression(left, greaterThan, right);
                ps.CacheAndPop(greaterThanExpression);
                return true;
            }
            else
            {
                greaterThanExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
