using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LDefParser
{
    static class Parser
    {
        public static ClassInfo[] Parse(string filePath)
        {
            StringBuffer sb = new StringBuffer(File.ReadAllText(filePath));
            List<Definition> definitions = new List<Definition>();
            while (!sb.ReachedEnd())
            {
                if (Definition.TryReadDefinition(sb, out Definition def, null, true))
                {
                    definitions.Add(def);
                }
                else throw new InvalidOperationException();
            }
            return ClassInfo.GetClassInfos(definitions).ToArray();
        }
    }
}
