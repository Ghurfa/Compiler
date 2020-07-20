using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class AndExpression : E10
    {
        public E10 Left { get; private set; }
        public AndToken And { get; private set; }
        public E9 Right { get; private set; }
    
        public AndExpression (E10 left, AndToken and, E9 right)
        {
            Left = left;
            And = and;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out AndExpression andExpression)
        {
            if (ps.CheckCache(out AndExpression cached))
            {
                andExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E10.TryParse(ps, out E10 left)
                && ps.TryParse(out AndToken and)
                && E9.TryParse(ps, out E9 right))
            {
                andExpression = new AndExpression(left, and, right);
                ps.CacheAndPop(andExpression);
                return true;
            }
            else
            {
                andExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
