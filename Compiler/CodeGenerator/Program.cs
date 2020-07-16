using CodeGeneratorLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            TokensGenerator.GenerateTokenClasses(tokensDefPath: @"..\..\..\..\..\Definitions\TokenTypes.txt",
                                                 baseDir: @"..\..\..\..\..\Compiler\Compiler\Tokens\",
                                                 TCBPath: @"..\..\..\..\..\Compiler\Compiler\TokenCollectionBuilder.cs");

            List<ClassInfo> classInfos = DefinitionParser.ParseFile(baseDir: @"..\..\..\..\..\Compiler\Compiler\SyntaxTreeItems\",
                                                                                  filePath: @"..\..\..\..\..\Definitions\ExpressionDefinitions.txt",
                                                                                  namespaceName: "Compiler.SyntaxTreeItems",
                                                                                  TokensGenerator.TokenNames.ToArray());
            foreach (ClassInfo classInfo in classInfos)
            {
                if (classInfo.InheritsFrom.Contains("ICompleteStatement")) classInfo.UsingStatements.Add("using Compiler.SyntaxTreeItems.Statements;");

                MethodInfo toStringMethod = new MethodInfo("public override string ToString()");
                toStringMethod.Body.Add("string ret = \"\";");
                bool isFirst = true;
                foreach (FieldInfo item in classInfo.InstanceFields)
                {
                    if (isFirst) isFirst = false;
                    else toStringMethod.Body.Add("ret += \" \";");
                    foreach (string toStringLine in item.GetToString())
                    {
                        toStringMethod.Body.Add(toStringLine);
                    }
                }
                toStringMethod.Body.Add("return ret;");

                if (classInfo.Flags.Contains("BinaryExpression"))
                {
                    string leftName = classInfo.InstanceFields.First().Name;
                    string leftProp = classInfo.InstanceFields.First().Type == "Expression" ?
                        $"override Expression LeftExpr {{ get => {leftName}; set {{ {leftName} = value; }} }}" :
                            classInfo.Flags.Contains("AssignExpression") ?
                            $"override Expression LeftExpr {{ get => {leftName}; set {{ if (value is UnaryExpression unary) {leftName} = unary; else throw new InvalidAssignmentLeftException(value);}} }}":
                            $"override Expression LeftExpr {{ get => {leftName}; set => throw new InvalidOperationException(); }}";
                    string rightName = classInfo.InstanceFields.Last().Name;
                    string rightProp = classInfo.InstanceFields.Last().Type == "Expression" ?
                        $"override Expression RightExpr {{ get => {rightName}; set {{ {rightName} = value; }} }}" :
                        $"override Expression RightExpr {{ get => {rightName}; set => throw new InvalidOperationException(); }}";
                    classInfo.GetSetProperties.Add(leftProp);
                    classInfo.GetSetProperties.Add(rightProp);
                }

                classInfo.Methods.Add(toStringMethod);
            }

            ClassWriter.GenerateFiles(classInfos);
        }
    }
}
