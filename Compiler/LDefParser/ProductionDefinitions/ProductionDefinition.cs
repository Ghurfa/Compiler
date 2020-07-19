using LDefParser.ProductionDefinitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDefParser
{
    abstract class ProductionDefinition
    {
        public static bool TryReadProductionDefinition(StringBuffer sb, out ProductionDefinition productionDefinition, string inheritFrom)
        {
            sb.Save();
            if (SingleDefinition.TryReadSingleDefinition(sb, out var singleDef))
            {
                productionDefinition = singleDef;
                sb.Pop();
                return true;
            }
            else if(SetDefinition.TryReadSingleDefinition(sb, out var setDef, inheritFrom))
            {
                productionDefinition = setDef;
                sb.Pop();
                return true;
            }
            else
            {
                productionDefinition = default;
                sb.Restore();
                return false;
            }
        }
    }
}
