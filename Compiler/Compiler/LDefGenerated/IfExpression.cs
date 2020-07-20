using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class IfExpression : E13
    {
        public E12 Condition { get; private set; }
        public QuestionMarkToken QuestionMark { get; private set; }
        public E12 IfTrue { get; private set; }
        public BackslashToken Backslash { get; private set; }
        public E13 IfFalse { get; private set; }
    
        public IfExpression (E12 condition, QuestionMarkToken questionMark, E12 ifTrue, BackslashToken backslash, E13 ifFalse)
        {
            Condition = condition;
            QuestionMark = questionMark;
            IfTrue = ifTrue;
            Backslash = backslash;
            IfFalse = ifFalse;
        }
    
        public static bool TryParse(ParseStack ps, out IfExpression ifExpression)
        {
            if (ps.CheckCache(out IfExpression cached))
            {
                ifExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E12.TryParse(ps, out E12 condition)
                && ps.TryParse(out QuestionMarkToken questionMark)
                && E12.TryParse(ps, out E12 ifTrue)
                && ps.TryParse(out BackslashToken backslash)
                && E13.TryParse(ps, out E13 ifFalse))
            {
                ifExpression = new IfExpression(condition, questionMark, ifTrue, backslash, ifFalse);
                ps.CacheAndPop(ifExpression);
                return true;
            }
            else
            {
                ifExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
