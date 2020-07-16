using CodeGeneratorLib.ClassItems;
using CodeGeneratorLib.SyntaxTreeItemsFieldInfos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CodeGeneratorLib
{
    public static class ClassWriter
    {
        public static void GenerateFiles(IEnumerable<ClassInfo> classInfos)
        {
            foreach (ClassInfo classInfo in classInfos) GenerateFile(classInfo);
        }
        public static void GenerateFile(ClassInfo classInfo)
        {
            LinkedList<string> text = new LinkedList<string>();

            foreach (string usingStatement in classInfo.UsingStatements) text.AddLast(usingStatement);

            text.AddLast("");
            text.AddLast($"namespace {classInfo.Namespace}");
            text.AddLast("{");
            text.AddLast($"    public class {classInfo.Name}{CreateInheritanceString(classInfo)}");
            text.AddLast("    {");
            if (classInfo.GetProperties.Count > 0)
            {
                foreach (KeyValuePair<string, string> property in classInfo.GetProperties)
                {
                    text.AddLast($"        public {property.Key} => {property.Value};");
                }
                text.AddLast("");
            }
            if (classInfo.GetSetProperties.Count > 0)
            {
                foreach (GetSetPropertyInfo property in classInfo.GetSetProperties)
                {
                    if (property.BackingFieldName != null)
                    {
                        text.AddLast($"        private {property.Type} {property.BackingFieldName};");
                    }
                    string propLine = "        ";
                    foreach (string modifier in property.Modifiers) propLine += modifier + " ";
                    propLine += property.Type + " ";
                    propLine += property.Name + " { ";
                    propLine += property.Get + " ";
                    propLine += property.Set + " }";
                    text.AddLast(propLine);
                }
                text.AddLast("");
            }
            text.AddLast($"        public {classInfo.Name}({GetConstructorParameterString(classInfo)})");
            text.AddLast("        {");
            foreach (string line in classInfo.ConstructorLines)
            {
                text.AddLast("            " + line);
            }
            text.AddLast("        }");
            foreach (MethodInfo method in classInfo.Methods)
            {
                text.AddLast("");
                text.AddLast($"        {method.Signature}");
                text.AddLast("        {");
                foreach (string line in method.Body)
                {
                    text.AddLast("            " + line);
                }
                text.AddLast("        }");
            }
            text.AddLast("    }");
            text.AddLast("}");
            Directory.CreateDirectory(classInfo.Directory);
            File.WriteAllLines(classInfo.Directory + classInfo.Name + ".cs", text);
        }
        private static string CreateInheritanceString(ClassInfo classInfo)
        {
            string inheritStr;
            if (classInfo.InheritsFrom.Count == 0) inheritStr = "";
            else
            {
                inheritStr = " : ";
                bool isFirst = true;
                foreach (string inter in classInfo.InheritsFrom)
                {
                    if (isFirst) isFirst = false;
                    else inheritStr += ", ";
                    inheritStr += inter;
                }
            }
            return inheritStr;
        }
        private static string GetConstructorParameterString(ClassInfo classInfo)
        {
            string parametersStr = "TokenCollection tokens";
            foreach (FieldInfo field in classInfo.InstanceFields)
            {
                parametersStr += ", " + field.Type + (field is TokenFieldInfo ? "? " : " ") + field.LowerCaseName + " = null";
            }
            return parametersStr;
        }
    }
}
