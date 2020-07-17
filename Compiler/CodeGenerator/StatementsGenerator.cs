using CodeGeneratorLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator
{
    public static class StatementsGenerator
    {
        public static void Generate()
        {
            List<ClassInfo> classInfos = DefinitionParser.ParseFile(baseDir: @"..\..\..\..\..\Compiler\Compiler\SyntaxTreeItems\",
                                                                                  filePath: @"..\..\..\..\..\Definitions\StatementDefinitions.txt",
                                                                                  namespaceName: "Compiler.SyntaxTreeItems",
                                                                                  TokensGenerator.TokenNames.ToArray());
            ClassWriter.GenerateFiles(classInfos);
        }
    }
}
