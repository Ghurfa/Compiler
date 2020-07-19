using LDefParser.ClassInfos;
using LDefParser.ProductionItems;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;

namespace LDefParser
{
    public static class Generator
    {
        public static void Generate(string definitionPath, string destinationDirectory)
        {
            ClassInfo[] classes = Parser.Parse(definitionPath);
            Directory.CreateDirectory(destinationDirectory);
            foreach (ClassInfo classInfo in classes)
            {
                List<string> text = new List<string>()
                {
                    "using System;",
                    "using System.Collections.Generic;",
                    "using System.Linq;",
                    "using System.Text;",
                  "",
                $"namespace Compiler.LDefGenerated",
                "{",
                };
                if (classInfo is AbstractClassInfo aci)
                {
                    foreach (string bodyLine in GenerateAbstractClass(aci))
                        text.Add("    " + bodyLine);
                }
                else if (classInfo is ConcreteClassInfo cci)
                {
                    foreach (string bodyLine in GenerateConcreteClass(cci))
                        text.Add("    " + bodyLine);
                }
                else throw new InvalidOperationException();
                text.Add("}");
                File.WriteAllLines(destinationDirectory + classInfo.Name + ".cs", text);
            }
        }

        private static IEnumerable<string> GenerateAbstractClass(AbstractClassInfo classInfo)
        {
            string inheritanceString = classInfo.Parent == null ? null : " : " + classInfo.Parent;
            string type = classInfo.Name;
            string ret = classInfo.LowerCaseName;

            yield return $"public abstract class {classInfo.Name}{inheritanceString}";
            yield return "{";
            yield return $"    public static bool TryParse(ParseStack ps, out {type} {ret})";
            yield return "    {";
            yield return $"        if (ps.CheckCache(out {type} cached))";
            yield return "        {";
            yield return $"            {ret} = cached;";
            yield return "            return true;";
            yield return "        }";
            yield return "        ";
            yield return "        ps.Save();";
            bool isFirstProd = true;
            foreach (Production prod in classInfo.SetDefinition.Productions)
            {
                string prodType = prod.InnerDefinition.Name;
                string prodRet = prod.InnerDefinition.LowerCaseName;
                if (isFirstProd)
                {
                    yield return $"        if ({prodType}.TryParse(ps, out {prodType} {prodRet})";
                    isFirstProd = false;
                }
                else yield return $"        else if ({prodType}.TryParse(ps, out {prodType} {prodRet})";
                yield return "        {";
                yield return $"            {ret} = {prodRet};";
                yield return $"            ps.Pop();";
                yield return $"            return true;";
                yield return "        }";
            }
            yield return "        else";
            yield return "        {";
            yield return $"            {ret} = null;";
            yield return $"            ps.Restore();";
            yield return $"            return false;";
            yield return "        }";
            yield return "    }";
            yield return "}";
        }

        private static IEnumerable<string> GenerateConcreteClass(ConcreteClassInfo classInfo)
        {
            string inheritanceString = classInfo.Parent == null ? null : " : " + classInfo.Parent;
            string type = classInfo.Name;
            string ret = classInfo.LowerCaseName;

            yield return $"public class {classInfo.Name}{inheritanceString}";
            yield return "{";

            string constructorParams = "";
            bool firstItem = true;
            foreach (ProductionItem item in classInfo.SingleDefinition.Items)
            {
                yield return $"    public {item.Type} {item.Name} {{ get; private set; }}";

                if (firstItem) firstItem = false;
                else constructorParams += ", ";

                constructorParams += $"{item.Type} {item.LowerCaseName}";
            }
            yield return "";
            yield return $"    public {type} ({constructorParams})";
            yield return "    {";
            foreach (ProductionItem item in classInfo.SingleDefinition.Items)
            {
                yield return $"        {item.Name} = {item.LowerCaseName};";
            }
            yield return "    }";
            yield return "";
            yield return $"    public static bool TryParse(ParseStack ps, out {type} {ret})";
            yield return "    {";
            yield return $"        if (ps.CheckCache(out {type} cached))";
            yield return "        {";
            yield return $"            {ret} = cached;";
            yield return "            return true;";
            yield return "        }";
            yield return "        ";
            yield return "        ps.Save();";
            for (int i = 0; i < classInfo.SingleDefinition.Items.Length; i++)
            {
                ProductionItem item = classInfo.SingleDefinition.Items[i];
                string condition;
                switch (item)
                {
                    case NormalProductionItem normalItem:
                        condition = $"{normalItem.Type}.TryParse(ps, out {normalItem.Type} {normalItem.LowerCaseName})";
                        break;
                    case TokenProductionItem tokenItem:
                        condition = $"ps.TryParse(out {tokenItem.Type} {tokenItem.LowerCaseName})";
                        break;
                    case RepeatedProductionItem repeatedItem:
                        condition = $"TryParse{repeatedItem.Name}(out {repeatedItem.Type} {repeatedItem.LowerCaseName})";
                        break;
                    default: throw new InvalidOperationException();
                }

                if (i == 0) condition = "if (" + condition;
                else condition = "    && " + condition;

                if (i == classInfo.SingleDefinition.Items.Length - 1) condition += ")";

                yield return "        " + condition;
            }
            yield return "        {";

            string creationArgs = "";
            firstItem = true;
            foreach (ProductionItem item in classInfo.SingleDefinition.Items)
            {
                if (firstItem) firstItem = false;
                else creationArgs += ", ";

                creationArgs += item.LowerCaseName;
            }
            yield return $"            {ret} = new {type}({creationArgs});";
            yield return $"            ps.CacheAndPop({ret});";
            yield return $"            return true;";
            yield return "        }";
            yield return "        else";
            yield return "        {";
            yield return $"            {ret} = null;";
            yield return $"            ps.Restore();";
            yield return $"            return false;";
            yield return "        }";
            yield return "    }";
            foreach(ProductionItem item in classInfo.SingleDefinition.Items)
            {
                if(item is RepeatedProductionItem repeatedItem)
                {
                    string innerType = repeatedItem.Inner.Type;
                    yield return "";
                    yield return $"    private static bool TryParse{repeatedItem.Name}(ParseStack ps, out {repeatedItem.Type} {repeatedItem.LowerCaseName})";
                    yield return "    {";
                    yield return $"        List<{innerType}> items = new List<{innerType}";
                    yield return $"        while ({innerType}.TryParse(ps, out {innerType} {repeatedItem.Inner.LowerCaseName})";
                    yield return "        {";
                    yield return $"        items.Add({repeatedItem.Inner.LowerCaseName});";
                    yield return "        }";
                    yield return $"        {repeatedItem.LowerCaseName} = items.ToArray();";
                    yield return "        return true;";
                    yield return "    }";
                }
            }
            yield return "}";
        }
    }
}
