﻿using CodeGeneratorLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator
{
    public static class TypesGenerator
    {
        public static void Generate()
        {
            List<ClassInfo> classInfos = DefinitionParser.ParseFile(baseDir: @"..\..\..\..\..\Compiler\Compiler\SyntaxTreeItems\",
                                                                                     filePath: @"..\..\..\..\..\Definitions\TypeDefinitions.txt",
                                                                                     namespaceName: "Compiler.SyntaxTreeItems",
                                                                                     TokensGenerator.TokenNames.ToArray());
            ClassWriter.GenerateFiles(classInfos);
        }
    }
}
