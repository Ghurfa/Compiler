using CodeGenerator.Modifiers;
using CodeGenerator.SyntaxTreeItemsFieldInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.ClassItems
{
    abstract class ClassItemInfo
    {
        public abstract string[] GetCreationStatements();
        public abstract string[] GetDeclaration();
        public abstract string[] GetToString();
        public static ClassItemInfo CreateItemInfo(string text, string[] tokenNames)
        {
            ClassItemInfo baseItem = null;
            (string type, string name) = (null, null);

            string[] statements = text.Split(';');

            if (text.First() == ';')
            {
                baseItem = InitialStatementItemInfo.ReadStatementItem(statements[1]);
                statements = statements.ExceptFirst().ExceptFirst();
            }
            else (type, name) = FieldInfo.ReadDeclaration(statements[0], tokenNames);

            LinkedList<Modifier> modifiersList = new LinkedList<Modifier>();

            if(statements.Length > 0)
            {
                foreach (string modifierText in statements.ExceptFirst())
                {
                    modifiersList.AddLast(Modifier.ReadModifier(modifierText));
                }
            }

            InitialModifier initialModifier;
            if (modifiersList.Count > 0 && modifiersList.First() is InitialModifier first)
            {
                initialModifier = first;
                modifiersList.RemoveFirst();
            }
            else
            {
                initialModifier = null;
            }

            baseItem ??= getBaseClassItem(type, name, tokenNames, initialModifier);

            ClassItemInfo itemSoFar = baseItem;
            foreach (Modifier modifier in modifiersList)
            {
                if (modifier is CheckModifier checkMod)
                {
                    itemSoFar = new CheckStatement(checkMod, itemSoFar);
                }
                else throw new InvalidOperationException();
            }
            return itemSoFar;
        }
        private static ClassItemInfo getBaseClassItem(string type, string name, string[] tokenNames, InitialModifier initialModifier)
        {
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
                        if (type.Last() == ']') return new ArrayFieldInfo(type, name, initialModifier, tokenNames);

                        if (tokenNames.Contains(type)) return new TokenFieldInfo(type, name, initialModifier);
                        else if (type.Last() == '?' && tokenNames.Contains(type.Substring(0, type.Length - 1))) return new NullableTokenFieldInfo(type, name, initialModifier);
                        else return new NormalFieldInfo(type, name);
                    }
            }
        }
    }
}
