using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class OrExpression : E11
    {
        public E11 Left { get; private set; }
        public OrToken Or { get; private set; }
        public E10 Right { get; private set; }
    
        public OrExpression (E11 left, OrToken or, E10 right)
        {
            Left = left;
            Or = or;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out OrExpression orExpression)
        {
            if (ps.CheckCache(out OrExpression cached))
            {
                orExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E11.TryParse(ps, out E11 left)
                && ps.TryParse(out OrToken or)
                && E10.TryParse(ps, out E10 right))
            {
                orExpression = new OrExpression(left, or, right);
                ps.CacheAndPop(orExpression);
                return true;
            }
            else
            {
                orExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
