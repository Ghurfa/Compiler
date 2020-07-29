using Parser.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compiler
{
    public static class Compiler
    {
        public static void Compile(string inputPath, string outputPath)
        {
            var text = File.ReadAllText(inputPath);
            var tokens = Tokenizer.Tokenizer.Tokenize(text);
            NamespaceDeclaration namespaceDecl = new NamespaceDeclaration(tokens);
            TypeChecker.TypeChecker.CheckNamespace(namespaceDecl);
            Generator.Generator.Generate("Program.exe", TypeChecker.TypeChecker.Table);
        }
    }
}
