using CodeGeneratorLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator
{
    public static class MiscellaneousGenerator
    {
        public static void Generate()
        {
            List<ClassInfo> classInfos = DefinitionParser.ParseFile(baseDir: @"..\..\..\..\..\Compiler\Compiler\SyntaxTreeItems\",
                                                                                  filePath: @"..\..\..\..\..\Definitions\MiscellaneousDefinitions.txt",
                                                                                  namespaceName: "Compiler.SyntaxTreeItems",
                                                                                  TokensGenerator.TokenNames.ToArray());
            ClassWriter.GenerateFiles(classInfos);
        }
    }
}
