using CodeGeneratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public static class ExpressionsGenerator
    {
        public static void Generate()
        {
            List<ClassInfo> classInfos = DefinitionParser.ParseFile(baseDir: @"..\..\..\..\..\Compiler\Compiler\SyntaxTreeItems\",
                                                                                  filePath: @"..\..\..\..\..\Definitions\ExpressionDefinitions.txt",
                                                                                  namespaceName: "Compiler.SyntaxTreeItems",
                                                                                  TokensGenerator.TokenNames.ToArray());
            foreach (ClassInfo classInfo in classInfos)
            {

                //LeftExpr and RightExpr properties
                if (classInfo.Flags.Contains("BinaryExpression"))
                {
                    string leftName = classInfo.InstanceFields.First().Name;
                    string leftSet = classInfo.InstanceFields.First().Type == "Expression" ?
                        $"set {{ {leftName} = value; }}" :
                            classInfo.Flags.Contains("AssignmentExpression") ?
                            $"set {{ if (value is UnaryExpression unary) {leftName} = unary; else throw new InvalidAssignmentLeftException(value);}}" :
                            $"set => throw new InvalidOperationException();";
                    GetSetPropertyInfo leftProp = new GetSetPropertyInfo("Expression", "LeftExpr", $"get => {leftName};", leftSet, null, "public", "override");

                    string rightName = classInfo.InstanceFields.Last().Name;
                    string rightSet = classInfo.InstanceFields.Last().Type == "Expression" ?
                        $"set {{ {rightName} = value; }}" :
                        $"set => throw new InvalidOperationException();";
                    GetSetPropertyInfo rightProp = new GetSetPropertyInfo("Expression", "RightExpr", $"get => {rightName};", rightSet, null, "public", "override");
                    classInfo.GetSetProperties.Add(leftProp);
                    classInfo.GetSetProperties.Add(rightProp);
                }

                //Check left-hand side of assignment expressions
                if (classInfo.Flags.Contains("AssignmentExpression"))
                {
                    foreach (GetSetPropertyInfo prop in classInfo.GetSetProperties)
                    {
                        if (prop.Name == "To")
                        {
                            prop.BackingFieldName = "to";
                            prop.Get = "get => to;";
                            prop.Set = "set { if (value is IAssignableExpression) to = value; else throw new InvalidAssignmentLeftException(value); }";
                            break;
                        }
                    }
                }
            }

            ClassWriter.GenerateFiles(classInfos);
        }
    }
}
