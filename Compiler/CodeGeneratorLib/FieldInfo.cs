using CodeGeneratorLib.AttributeInfos;
using CodeGeneratorLib.AttributeInfos.ValueAttributes;
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
        public ValueAttribute ValueAttr { get; set; }

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
                else if (attr is ValueAttribute valueAttr)
                {
                    if (ValueAttr == null) ValueAttr = valueAttr;
                    else throw new InvalidOperationException();

                    if (valueAttr is OptionalAttribute popAttr)
                    {
                        popAttr.NameToPop = LowerCaseName;
                        popAttr.TypeToPop = Type;
                        Type += "?";
                    }
                    else if (valueAttr is ArgumentAttribute argumentAttr)
                    {
                        argumentAttr.ArgumentName = LowerCaseName;
                        argumentAttr.Type = Type;
                    }
                    else if (valueAttr is OptionalArgAttribute optionalArgAttr)
                    {
                        optionalArgAttr.ArgumentName = LowerCaseName;
                        optionalArgAttr.Type = Type.EndsWith("Token") ? Type + "?" : Type;
                        optionalArgAttr.NormalInitialization = NormalInitialization(Type);
                    }
                    else if (valueAttr is ValidStatementEndingAttribute)
                    {
                        Type += "?";
                    }
                }
            }
        }

        public static string NormalInitialization(string type)
        {
            switch (type)
            {
                case "Expression": return "Expression.ReadExpression(tokens)";
                case "UnaryExpression": return "UnaryExpression.ReadUnaryExpression(tokens)";
                case "PrimaryExpression": return "PrimaryExpression.ReadPrimaryExpression(tokens)";
                case "Statement": return "Statement.ReadStatement(tokens)";
                case "Type": return "Type.ReadType(tokens)";
                case "ClassItemDeclaration": return "ClassItemDeclaration.ReadClassItem(tokens)";
                default:
                    {
                        if (type.EndsWith("Token")) return $"tokens.PopToken<{type}>()";
                        else return $"new {type}(tokens)";

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

            if (type == "Token") return new PlaceHolderFieldInfo(type, name, attributesArr);
            else if (type.First() == '[') return new ArrayFieldInfo(type, name, attributesArr);
            else return new NormalFieldInfo(type, name, attributesArr);
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

        public bool HasAttribute<T>(out T attribute) where T : AttributeInfo
        {
            foreach (AttributeInfo attr in Attributes)
            {
                if (attr is T)
                {
                    attribute = (T)attr;
                    return true;
                }
            }
            attribute = default;
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

        public virtual string[] GetToString()
        {
            if (Type.Last() == '?') return new string[] { $"ret += {Name}?.ToString();" };
            else return new string[] { $"ret += {Name}.ToString();" };
        }
    }
}
