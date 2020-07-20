using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class PlusExpression : E3
    {
        public E3 Left { get; private set; }
        public PlusToken Plus { get; private set; }
        public E2 Right { get; private set; }
    
        public PlusExpression (E3 left, PlusToken plus, E2 right)
        {
            Left = left;
            Plus = plus;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out PlusExpression plusExpression)
        {
            if (ps.CheckCache(out PlusExpression cached))
            {
                plusExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E3.TryParse(ps, out E3 left)
                && ps.TryParse(out PlusToken plus)
                && E2.TryParse(ps, out E2 right))
            {
                plusExpression = new PlusExpression(left, plus, right);
                ps.CacheAndPop(plusExpression);
                return true;
            }
            else
            {
                plusExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
