using CodeGenerator.ClassItems;
using CodeGenerator.SyntaxTreeItemsFieldInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    abstract class FieldInfo : ClassItemInfo
    {
        public string Name;
        public string Type;
        public string LowerCaseName => Name.Substring(0, 1).ToLower() + Name.Substring(1);
        protected FieldInfo(string type, string name)
        {
            Type = type;
            Name = name;
        }
        public override string[] GetDeclaration() => new string[] { $"public readonly {Type} {Name};" };
        public override string[] GetToString() => new string[] { $"ret += {Name}.ToString();" };

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
