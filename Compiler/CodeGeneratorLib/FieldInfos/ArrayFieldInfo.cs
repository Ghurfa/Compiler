using CodeGeneratorLib.AttributeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.FieldInfos
{
    public class ArrayFieldInfo : FieldInfo
    {
        public string BaseType { get; set; }
        ConditionAttribute Condition { get; set; }

        public ArrayFieldInfo(string type, string name, AttributeInfo[] attributes) : base(type.Substring(1) + "[]", name, attributes)
        {
            BaseType = type.Substring(1);
            foreach (AttributeInfo attr in attributes)
            {
                if (attr is ConditionAttribute cond)
                {
                    if (Condition == null) Condition = cond;
                    else throw new InvalidOperationException();
                }
            }
        }

        public override string[] GetCreationStatements()
        {
            List<string> statements = new List<string>();
            statements.Add($"LinkedList<{BaseType}> {LowerCaseName}List = new LinkedList<{BaseType}>();");
            foreach (string initStatement in Condition.GetInitializingStatements())
            {
                statements.Add(initStatement);
            }
            statements.Add($"while ({Condition.GetCondition()})");
            statements.Add("{");
            statements.Add($"    {LowerCaseName}List.AddLast({GetItemCreationStatement()});");
            foreach (string updateStatement in Condition.GetUpdateStatements())
            {
                statements.Add("    " + updateStatement);
            }
            statements.Add("}");
            statements.Add($"{Name} = {LowerCaseName}List.ToArray();");
            return statements.ToArray();
        }

        private string GetItemCreationStatement()
        {
            switch (BaseType)
            {
                case "Expression": return "Expression.ReadExpression(tokens)";
                case "UnaryExpression": return "UnaryExpression.ReadUnaryExpression(tokens)";
                case "PrimaryExpression": return "PrimaryExpression.ReadPrimaryExpression(tokens)";
                case "Statement": return "Statement.ReadStatement(tokens)";
                case "Type": return "Type.ReadType(tokens)";
                case "ClassItemDeclaration": return "ClassItemDeclaration.ReadClassItem(tokens)";
                default:
                    {
                        if (BaseType.EndsWith("Token")) return $"tokens.PopToken<{BaseType}>()";
                        else return $"new {BaseType}(tokens)";
                    }
            }
        }
    }
}
