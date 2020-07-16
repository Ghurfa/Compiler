using CodeGeneratorLib.ClassItems;
using CodeGeneratorLib.SyntaxTreeItemsFieldInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneratorLib
{
    public abstract class FieldInfo
    {
        public string Name;
        public string Type;
        public string LowerCaseName => Name.Substring(0, 1).ToLower() + Name.Substring(1);
        public abstract string[] GetCreationStatements();
        public virtual GetSetPropertyInfo[] GetDeclaration() =>
            new GetSetPropertyInfo[] { new GetSetPropertyInfo(Type, Name, null, "private set;") };
        public virtual string[] GetToString() => new string[] { $"ret += {Name}.ToString();" };
        protected FieldInfo(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public static List<FieldInfo> CreateFieldInfoList(IEnumerable<string> fieldDeclarations, string[] tokenNames)
        {
            List<FieldInfo> list = new List<FieldInfo>();
            foreach (string declaration in fieldDeclarations)
            {
                var newField = GetFieldInfo(declaration, tokenNames);
                list.Add(newField);
            }
            return list;
        }

        public static FieldInfo GetFieldInfo(string current, string[] tokenNames)
        {
            var (type, name) = ReadDeclaration(current, tokenNames);
            switch (type)
            {
                case "Expression": return new ExpressionFieldInfo(type, name);
                case "UnaryExpression": return new UnaryExpressionFieldInfo(type, name);
                case "PrimaryExpression": return new PrimaryExpressionFieldInfo(type, name);
                case "Statement": return new StatementFieldInfo(type, name);
                case "Type": return new TypeFieldInfo(type, name);
                case "Token": return new PlaceHolderFieldInfo(type, name);
                default:
                    {
                        if (tokenNames.Contains(type)) return new TokenFieldInfo(type, name);
                        else return new NormalFieldInfo(type, name);
                    }
            }
        }

        public static (string type, string name) ReadDeclaration(string declTexts, string[] tokenNames)
        {
            string[] parts = declTexts.Split(':');
            if (parts.Length > 2) throw new InvalidOperationException();
            string type = parts[0];

            string arrBrackets = "";
            if (type.Last() == ']')
            {
                arrBrackets = "[]";
                type = type.Substring(0, type.Length - 2);
            }

            string name = parts.Length > 1 ? parts[1] : (type.Last() == '?' ? type.Substring(0, type.Length - 1) : type);
            if (type.First() == '@')
                type = type.Substring(1);
            else
            {
                if (type.Last() == '?')
                {
                    string withoutQuestionMark = type.Substring(0, type.Length - 1);
                    if (tokenNames.Contains(withoutQuestionMark + "Token")) type = withoutQuestionMark + "Token" + "?";
                }
                else if (tokenNames.Contains(type + "Token")) type += "Token";
            }
            return (type + arrBrackets, name);
        }
    }
}
