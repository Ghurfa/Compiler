using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E14 : Expression
    {
        public static bool TryParse(ParseStack ps, out E14 e14)
        {
            if (ps.CheckCache(out E14 cached))
            {
                e14 = cached;
                return true;
            }
            
            ps.Save();
            if (AssignExpression.TryParse(ps, out AssignExpression assignExpression))
            {
                e14 = assignExpression;
                ps.Pop();
                return true;
            }
            else if (DeclAssignExpression.TryParse(ps, out DeclAssignExpression declAssignExpression))
            {
                e14 = declAssignExpression;
                ps.Pop();
                return true;
            }
            else if (PlusAssignExpression.TryParse(ps, out PlusAssignExpression plusAssignExpression))
            {
                e14 = plusAssignExpression;
                ps.Pop();
                return true;
            }
            else if (MinusAssignExpression.TryParse(ps, out MinusAssignExpression minusAssignExpression))
            {
                e14 = minusAssignExpression;
                ps.Pop();
                return true;
            }
            else if (MultiplyAssignExpression.TryParse(ps, out MultiplyAssignExpression multiplyAssignExpression))
            {
                e14 = multiplyAssignExpression;
                ps.Pop();
                return true;
            }
            else if (DivideAssignExpression.TryParse(ps, out DivideAssignExpression divideAssignExpression))
            {
                e14 = divideAssignExpression;
                ps.Pop();
                return true;
            }
            else if (E13.TryParse(ps, out E13 e13))
            {
                e14 = e13;
                ps.Pop();
                return true;
            }
            else
            {
                e14 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
