using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class NullCoalescingExpression : E12
    {
        public E11 Left { get; private set; }
        public NullCoalescingToken NullCoalescing { get; private set; }
        public E12 Right { get; private set; }
    
        public NullCoalescingExpression (E11 left, NullCoalescingToken nullCoalescing, E12 right)
        {
            Left = left;
            NullCoalescing = nullCoalescing;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out NullCoalescingExpression nullCoalescingExpression)
        {
            if (ps.CheckCache(out NullCoalescingExpression cached))
            {
                nullCoalescingExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E11.TryParse(ps, out E11 left)
                && ps.TryParse(out NullCoalescingToken nullCoalescing)
                && E12.TryParse(ps, out E12 right))
            {
                nullCoalescingExpression = new NullCoalescingExpression(left, nullCoalescing, right);
                ps.CacheAndPop(nullCoalescingExpression);
                return true;
            }
            else
            {
                nullCoalescingExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
