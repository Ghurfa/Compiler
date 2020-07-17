using CodeGeneratorLib.AttributeInfos;
using CodeGeneratorLib.ClassItems;
using CodeGeneratorLib.FieldInfos;
using CodeGeneratorLib.SyntaxTreeItemsFieldInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneratorLib
{
    public abstract class FieldInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public AttributeInfo[] Attributes { get; set; }
        public BackingFieldAttribute BackingField { get; set; }

        public string LowerCaseName => Name.Substring(0, 1).ToLower() + Name.Substring(1);

        protected FieldInfo(string type, string name, AttributeInfo[] attributes)
        {
            Type = type;
            Name = name;
            Attributes = attributes;
            foreach (AttributeInfo attr in attributes)
            {
                if (attr is BackingFieldAttribute backingField)
                {
                    if (BackingField == null) BackingField = backingField;
                    else throw new InvalidOperationException();
                }
            }
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
            string[] parts = current.Split(';');

            var (type, name) = ReadDeclaration(parts[0], tokenNames);

            LinkedList<AttributeInfo> attributes = new LinkedList<AttributeInfo>();
            for (int i = 1; i < parts.Length; i++)
            {
                attributes.AddLast(AttributeInfo.ReadAttribute(parts[i]));
            }
            AttributeInfo[] attributesArr = attributes.ToArray();

            switch (type)
            {
                case "Expression": return new ExpressionFieldInfo(type, name, attributesArr);
                case "UnaryExpression": return new UnaryExpressionFieldInfo(type, name, attributesArr);
                case "PrimaryExpression": return new PrimaryExpressionFieldInfo(type, name, attributesArr);
                case "Statement": return new StatementFieldInfo(type, name, attributesArr);
                case "Type": return new TypeFieldInfo(type, name, attributesArr);
                case "Token": return new PlaceHolderFieldInfo(type, name, attributesArr);
                default:
                    {
                        if (tokenNames.Contains(type)) return new TokenFieldInfo(type, name, attributesArr);
                        else
                        {
                            if (type.First() == '[') return new ArrayFieldInfo(type, name, attributesArr);
                            else return new NormalFieldInfo(type, name, attributesArr);
                        }
                    }
            }
        }

        public static (string type, string name) ReadDeclaration(string declTexts, string[] tokenNames)
        {
            string[] parts = declTexts.Split(':');
            if (parts.Length > 2) throw new InvalidOperationException();
            string type = parts[0];

            string name = parts.Length > 1 ? parts[1] : type;
            if (tokenNames.Contains(type + "Token")) type += "Token";

            if (name.First() == '[') throw new InvalidOperationException();

            return (type, name);
        }

        public bool HasAttribute<T>()
        {
            foreach (AttributeInfo attr in Attributes)
            {
                if (attr is T) return true;
            }
            return false;
        }

        public abstract string[] GetCreationStatements();

        public virtual GetSetPropertyInfo[] GetDeclaration()
        {
            if (BackingField == null)
            {
                return new GetSetPropertyInfo[] { new GetSetPropertyInfo(Type, Name, null, "private set;") };
            }
            else
            {
                return new GetSetPropertyInfo[]
                {
                    new GetSetPropertyInfo(Type, Name, $"get => {BackingField.Name};", $"private set {{ {BackingField.Name} = value; }}", BackingField.Name)
                };
            }
        }

        public virtual string[] GetToString() => new string[] { $"ret += {Name}.ToString();" };
    }
}
