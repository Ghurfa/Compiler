using CodeGenerator.ClassItems;
using CodeGenerator.Modifiers;
using CodeGenerator.SyntaxTreeItemsFieldInfos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CodeGenerator
{
    static partial class SyntaxTreeItemsGenerator
    {
        private static LinkedList<FieldInfo> CreateFieldInfoList(IEnumerable<string> fieldDeclarations, string[] tokenNames)
        {
            LinkedList<FieldInfo> list = new LinkedList<FieldInfo>();
            foreach (string declaration in fieldDeclarations)
            {
                var newItem = ClassItemInfo.CreateItemInfo(declaration, tokenNames);
                if (newItem is FieldInfo field)
                {
                    list.AddLast(field);
                }
                else throw new NotImplementedException();
            }
            return list;
        }

        private static void GenerateFile(string name, LinkedList<string> interfaces, IEnumerable<ClassItemInfo> items, string directory)
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
            foreach (ClassItemInfo item in items)
            {
                if (item is FieldInfo field)
                {
                    parametersStr += ", " + field.Type + (field is TokenFieldInfo ? "? " : " ") + field.LowerCaseName + " = null";
                }
            }

            text.AddLast("using System;");
            text.AddLast("using System.Linq;");
            text.AddLast("using System.Collections.Generic;");
            text.AddLast("using System.Text;");
            text.AddLast("");
            text.AddLast("namespace Compiler");
            text.AddLast("{");
            text.AddLast($"    public class {name}{interfacesStr}");
            text.AddLast("    {");
            foreach (ClassItemInfo item in items)
            {
                foreach (string declarationLine in item.GetDeclaration())
                {
                    text.AddLast("        " + declarationLine);
                }
            }
            text.AddLast("");

            string overrideStr = interfaces.Count == 0 ? "" : "override";
            foreach (ClassItemInfo item in items)
            {
                ClassItemInfo innerMostItem = item;
                if(item is CheckStatement checkStatement)
                {
                    ClassItemInfo iter = checkStatement;
                    while (iter is CheckStatement chk) iter = chk.Inner;
                    innerMostItem = iter;
                }

                if(innerMostItem is FieldInfo field)
                {
                    if (innerMostItem is TokenFieldInfo)
                    {
                        text.AddLast($"        public {overrideStr} IToken LeftToken => {field.Name};");
                    }
                    else if (innerMostItem is ArrayFieldInfo arr)
                    {
                        if (arr.Type.EndsWith("Token[]"))
                        {
                            text.AddLast($"        public {overrideStr} IToken LeftToken => {field.Name}.First();");
                        }
                        else
                        {
                            text.AddLast($"        public {overrideStr} IToken LeftToken => {field.Name}.First().LeftToken;");
                        }
                    }
                    else
                    {
                        text.AddLast($"        public {overrideStr} IToken LeftToken => {field.Name}.LeftToken;");
                    }
                    break;
                }
            }

            if (name == "IfStatement")
                ;
            foreach (ClassItemInfo item in items.Reverse())
            {
                ClassItemInfo innerMostItem = item;
                if (innerMostItem is CheckStatement checkStatement)
                {
                    ClassItemInfo iter = checkStatement;
                    while (iter is CheckStatement chk) iter = chk.Inner;
                    innerMostItem = iter;
                }
                if(innerMostItem is ArrayFieldInfo arrField)
                {
                    if (arrField.WhileModifier is UntilPopModifier untilPop)
                    {
                        text.AddLast($"        public {overrideStr} IToken RightToken => {untilPop.TokenToPop};");
                        break;
                    }
                    else
                    {
                        innerMostItem = arrField;
                    }
                }

                if (innerMostItem is FieldInfo field)
                {
                    if (innerMostItem is TokenFieldInfo)
                    {
                        text.AddLast($"        public {overrideStr} IToken RightToken => {field.Name};");
                    }
                    else if (innerMostItem is ArrayFieldInfo arr)
                    {
                        if (arr.Type.EndsWith("Token[]"))
                        {
                            text.AddLast($"        public {overrideStr} IToken RightToken => {field.Name}.Last();");
                        }
                        else
                        {
                            text.AddLast($"        public {overrideStr} IToken RightToken => {field.Name}.Last().RightToken;");
                        }
                    }
                    else
                    {
                        text.AddLast($"        public {overrideStr} IToken RightToken => {field.Name}.RightToken;");
                    }
                    break;
                }
            }
            text.AddLast("");
            text.AddLast($"        public {name}({parametersStr})");
            text.AddLast("        {");
            foreach (ClassItemInfo item in items)
            {
                foreach (string statement in item.GetCreationStatements())
                {
                    text.AddLast("            " + statement);
                }
            }
            text.AddLast("        }");
            text.AddLast("");
            text.AddLast("        public override string ToString()");
            text.AddLast("        {");
            text.AddLast("            string ret = \"\";");
            foreach (ClassItemInfo item in items)
            {
                if (!(item is ArrayFieldInfo))
                {
                    text.AddLast($"            ");
                }
                else
                {
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
