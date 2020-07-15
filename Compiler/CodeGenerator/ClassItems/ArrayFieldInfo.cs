using CodeGenerator.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.SyntaxTreeItemsFieldInfos
{
    class ArrayFieldInfo : FieldInfo
    {
        string BaseType;
        string ItemInitializer;
        public WhileModifier WhileModifier;
        public ArrayFieldInfo(string type, string name, InitialModifier modifier, string[] tokenNames) : base(type, name)
        {
            BaseType = type.Substring(0, type.Length - 2);
            if (modifier is WhileModifier whileMod)
            {
                WhileModifier = whileMod;
            }
            else throw new InvalidOperationException();

            switch (BaseType)
            {
                case "Expression": ItemInitializer = "Expression.ReadExpression(tokens)"; break;
                case "UnaryExpression": ItemInitializer = "UnaryExpression.ReadUnaryExpression(tokens)"; break;
                case "PrimaryExpression": ItemInitializer = "PrimaryExpression.ReadPrimaryExpression(tokens)"; break;
                case "Statement": ItemInitializer = "Statement.ReadStatement(tokens)"; break;
                case "ClassItemDeclaration": ItemInitializer = "ClassItemDeclaration.ReadClassItem(tokens)"; break;
                case "Type": ItemInitializer = "Type.ReadType(tokens)"; break;
                default:
                    if (tokenNames.Contains(BaseType)) ItemInitializer = $"tokens.PopToken<{BaseType}>()";
                    else ItemInitializer = $"new {BaseType}(tokens)";
                    break;
            }
        }

        public override string[] GetDeclaration()
        {
            if (WhileModifier is UntilPopModifier untilPop)
            {
                return new string[]
                {
                $"public readonly {Type} {Name};",
                $"public readonly {untilPop.TokenToPop}Token {untilPop.TokenToPop};"
                };
            }
            else return base.GetDeclaration();
        }

        public override string[] GetCreationStatements()
        {
            LinkedList<string> ret = new LinkedList<string>();
            UntilModifier until = WhileModifier is UntilModifier ? (UntilModifier)WhileModifier : null;

            ret.AddLast($"var {LowerCaseName}List = new LinkedList<{BaseType}>();");
            ret.AddLast($"if ({LowerCaseName} != null)");
            ret.AddLast("{");
            ret.AddLast($"    foreach (var item in {LowerCaseName})");
            ret.AddLast("    {");
            ret.AddLast($"        {LowerCaseName}List.AddLast(item);");
            ret.AddLast("    }");
            ret.AddLast("}");
            if (until != null) ret.AddLast($"bool {until.ConditionName} = false;");
            ret.AddLast($"while({WhileModifier.GetCondition()})");
            ret.AddLast("{");
            ret.AddLast($"    var newItem = {ItemInitializer};");
            ret.AddLast($"    {LowerCaseName}List.AddLast(newItem);");
            if (until != null)
            {
                if (until.ConditionName.Substring(0, 11) == "lastMissing")
                {
                    string missingItem = until.ConditionName.Substring(11);
                    ret.AddLast($"    {until.ConditionName} = newItem.{missingItem} == null;");
                }
                else throw new InvalidOperationException();
            }
            ret.AddLast("}");
            ret.AddLast($"{Name} = {LowerCaseName}List.ToArray();");
            return ret.ToArray();
        }
        public override string[] GetToString()
        {
            LinkedList<string> text = new LinkedList<string>();
            text.AddLast($"foreach (var item in {Name})");
            text.AddLast("{");
            text.AddLast("    ret += item.ToString();");
            text.AddLast("}");

            if(WhileModifier is UntilPopModifier untilPop)
            {
                text.AddLast($"ret += {untilPop.TokenToPop}Token.ToString();");
            }

            return text.ToArray();
        }
    }
}
