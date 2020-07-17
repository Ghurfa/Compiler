using CodeGeneratorLib.AttributeInfos;
using CodeGeneratorLib.FieldInfos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CodeGeneratorLib
{
    public class ClassInfo
    {
        public string Directory;
        public List<string> Flags { get; set; }

        public List<string> UsingStatements { get; set; }
        public string Namespace;

        public string Name;
        public List<string> InheritsFrom { get; set; }

        public List<KeyValuePair<string, string>> GetProperties { get; set; }
        public List<GetSetPropertyInfo> GetSetProperties { get; set; }
        public ReadOnlyCollection<FieldInfo> InstanceFields { get; private set; }
        public List<MethodInfo> Methods { get; set; }
        public List<string> ConstructorLines { get; set; }
        public ClassInfo(string name, List<FieldInfo> instanceFields, Context context)
        {
            Directory = context.Directory;
            Flags = context.Flags;
            UsingStatements = new List<string>()
            {
                "using System;",
                "using System.Collections.Generic;",
                "using System.Linq;",
                "using System.Text;"
            };
            Namespace = context.Namespace;
            Name = name + context.Suffix;
            InheritsFrom = context.InheritsFrom;
            GetProperties = context.GetProperties;
            GetSetProperties = context.GetSetProperties;
            InstanceFields = instanceFields.AsReadOnly();

            ConstructorLines = new List<string>();
            foreach (FieldInfo field in instanceFields)
            {
                foreach (GetSetPropertyInfo property in field.GetDeclaration())
                {
                    GetSetProperties.Add(property);
                }
                if (!(field.HasAttribute<DisableCreationAttribute>()))
                {
                    foreach (string creationLine in field.GetCreationStatements())
                    {
                        ConstructorLines.Add(creationLine);
                    }
                }
            }

            Methods = new List<MethodInfo>();
            CreateToStringMethod();
        }

        private void CreateToStringMethod()
        {
            MethodInfo toStringMethod = new MethodInfo("public override string ToString()");
            toStringMethod.Body.Add("string ret = \"\";");
            string[] noSpacesAfter = new string[] { "OpenPerenToken", "OpenBracketToken" };
            string[] noSpacesBefore = new string[] { "ClosePerenToken", "CloseBracketToken", "CommaToken", "SemicolonToken" };
            string[] noSpacesAround = new string[] { "CharLiteralToken", "StringLiteralToken", "DotToken", "ColonToken" };
            string[] alwaysSpacesAfter = new string[] { "CommaToken" };
            for (int i = 0; i < InstanceFields.Count; i++)
            {
                foreach (string toStringLine in InstanceFields[i].GetToString())
                {
                    toStringMethod.Body.Add(toStringLine);
                }
                if (i < InstanceFields.Count - 1)
                {
                    string type = InstanceFields[i].Type;
                    string realType = type.Last() == '?' ? type.Substring(0, type.Length - 1) : type;
                    string nextType = InstanceFields[i + 1].Type;
                    string realNextType = nextType.Last() == '?' ? nextType.Substring(0, nextType.Length - 1) : nextType;
                    if (!Flags.Contains("UnaryExpression") &&
                        !noSpacesAfter.Contains(realType) &&
                        !noSpacesAround.Contains(realType) &&
                        !noSpacesBefore.Contains(realNextType) &&
                        !noSpacesAround.Contains(realNextType))
                    {
                        toStringMethod.Body.Add("ret += \" \";");
                    }
                }
                else if(alwaysSpacesAfter.Contains(InstanceFields[i].Type))
                {
                    toStringMethod.Body.Add("ret += \" \";");
                }
            }
            toStringMethod.Body.Add("return ret;");
            Methods.Add(toStringMethod);
        }
    }
}
