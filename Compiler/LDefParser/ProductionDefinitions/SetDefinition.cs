using System;
using System.Collections.Generic;
using System.Text;

namespace LDefParser.ProductionDefinitions
{
    class SetDefinition : ProductionDefinition
    {
        internal Production[] Productions { get; set; }
        internal Definition[] Definitions { get; set; }

        public static bool TryReadSingleDefinition(StringBuffer sb, out SetDefinition setDefinition, string inheritFrom)
        {
            sb.Save();
            if (sb.TryRead('{'))
            {
                List<Production> productions = new List<Production>();
                List<Definition> definitions = new List<Definition>();
                while (true)
                {
                    if (Production.TryReadProduction(sb, out var prod, inheritFrom))
                    {
                        productions.Add(prod);
                    }
                    else if (Definition.TryReadDefinition(sb, out Definition def, inheritFrom))
                    {
                        definitions.Add(def);
                    }
                    else break;
                }
                if (!sb.TryRead('}')) throw new InvalidOperationException();
                if (productions.Count == 0) throw new InvalidOperationException();
                setDefinition = new SetDefinition()
                {
                    Productions = productions.ToArray(),
                    Definitions = definitions.ToArray()
                };
                sb.Pop();
                return true;
            }
            else
            {
                setDefinition = default;
                sb.Restore();
                return false;
            }
        }
    }
}
