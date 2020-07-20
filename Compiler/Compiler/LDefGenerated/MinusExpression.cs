using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class MinusExpression : E3
    {
        public E3 Left { get; private set; }
        public MinusToken Minus { get; private set; }
        public E2 Right { get; private set; }
    
        public MinusExpression (E3 left, MinusToken minus, E2 right)
        {
            Left = left;
            Minus = minus;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out MinusExpression minusExpression)
        {
            if (ps.CheckCache(out MinusExpression cached))
            {
                minusExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E3.TryParse(ps, out E3 left)
                && ps.TryParse(out MinusToken minus)
                && E2.TryParse(ps, out E2 right))
            {
                minusExpression = new MinusExpression(left, minus, right);
                ps.CacheAndPop(minusExpression);
                return true;
            }
            else
            {
                minusExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
