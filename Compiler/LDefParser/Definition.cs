using System;
using System.Collections.Generic;
using System.Text;

namespace LDefParser
{
    class Definition
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public ProductionDefinition ProductionDefinition { get; set; }

        public string LowerCaseName => Name.Substring(0, 1).ToLower() + Name.Substring(1);

        public static bool TryReadDefinition(StringBuffer sb, out Definition definition, string inheritFrom = null)
        {
            sb.Save();
            if (sb.TryReadWord(out string name) && ProductionDefinition.TryReadProductionDefinition(sb, out var def, name))
            {
                definition = new Definition() { Name = name, Parent = inheritFrom, ProductionDefinition = def };
                sb.Pop();
                return true;
            }
            else
            {
                definition = default;
                sb.Restore();
                return false;
            }
        }
    }
}
