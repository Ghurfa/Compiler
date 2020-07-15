using CodeGenerator.ClassItems;
using CodeGenerator.SyntaxTreeItemsFieldInfos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CodeGenerator
{
    static class ExpressionsFileWriter
    {
        public static void GenerateFile(string name, LinkedList<string> interfaces, IEnumerable<FieldInfo> fields, string directory)
        {
            LinkedList<string> text = new LinkedList<string>();

            string interfacesStr;
            if (interfaces.Count == 0) interfacesStr = "";
            else
            {
                interfacesStr = " : ";
                bool isFirstInterface = true;
                foreach (string inter in interfaces)
                {
                    if (isFirstInterface) isFirstInterface = false;
                    else interfacesStr += ", ";
                    interfacesStr += inter;
                }
            }
            string parametersStr = "TokenCollection tokens";
            foreach (FieldInfo field in fields)
            {
                parametersStr += ", " + field.Type + (field is TokenFieldInfo ? "? " : " ") + field.LowerCaseName + " = null";
            }

            text.AddLast("using System;");
            text.AddLast("using System.Linq;");
            text.AddLast("using System.Collections.Generic;");
            text.AddLast("using System.Text;");
            text.AddLast("using Compiler.SyntaxTreeItems.Statements;");
            text.AddLast("");
            text.AddLast("namespace Compiler.SyntaxTreeItems");
            text.AddLast("{");
            text.AddLast($"    public class {name}{interfacesStr}");
            text.AddLast("    {");
            foreach (FieldInfo field in fields)
            {
                foreach (string declarationLine in field.GetDeclaration())
                {
                    text.AddLast("        " + declarationLine);
                }
            }
            text.AddLast("");
            text.AddLast($"        public {name}({parametersStr})");
            text.AddLast("        {");
            foreach (FieldInfo field in fields)
            {
                foreach (string statement in field.GetCreationStatements())
                {
                    text.AddLast("            " + statement);
                }
            }
            text.AddLast("        }");
            text.AddLast("");
            text.AddLast("        public override string ToString()");
            text.AddLast("        {");
            text.AddLast("            string ret = \"\";");
            foreach (FieldInfo item in fields)
            {
                foreach (string toStringLine in item.GetToString())
                {
                    text.AddLast("            " + toStringLine);
                }
            }
            text.AddLast("            return ret;");
            text.AddLast("        }");
            text.AddLast("    }");
            text.AddLast("}");
            Directory.CreateDirectory(directory);
            File.WriteAllLines(directory + name + ".cs", text);
        }
    }
}
