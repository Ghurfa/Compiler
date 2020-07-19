using System;
using System.Collections.Generic;
using System.Text;

namespace LDefParser
{
    class Production
    {
        public Definition InnerDefinition { get; set; }

        public static bool TryReadProduction(StringBuffer sb, out Production production, string inheritFrom = null)
        {
            sb.Save();
            if(sb.TryRead('|'))
            {
                if (Definition.TryReadDefinition(sb, out Definition def, inheritFrom))
                {
                    production = new Production() { InnerDefinition = def };
                    sb.Pop();
                    return true;
                }
                else throw new InvalidOperationException();
            }
            else
            {
                production = default;
                sb.Restore();
                return false;
            }
        }
    }
}
