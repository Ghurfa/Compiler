using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class DivideExpression : E2
    {
        public E2 Left { get; private set; }
        public DivideToken Divide { get; private set; }
        public E1 Right { get; private set; }
    
        public DivideExpression (E2 left, DivideToken divide, E1 right)
        {
            Left = left;
            Divide = divide;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out DivideExpression divideExpression)
        {
            if (ps.CheckCache(out DivideExpression cached))
            {
                divideExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E2.TryParse(ps, out E2 left)
                && ps.TryParse(out DivideToken divide)
                && E1.TryParse(ps, out E1 right))
            {
                divideExpression = new DivideExpression(left, divide, right);
                ps.CacheAndPop(divideExpression);
                return true;
            }
            else
            {
                divideExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
