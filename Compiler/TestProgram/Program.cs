using Compiler;
using Compiler.SyntaxTreeItems;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TypeChecker;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText(@"..\..\..\..\..\FizzBuzz.txt");
            var tokens = Tokenizer.Tokenize(text);
            NamespaceDeclaration namespaceDecl;
            try
            {
                namespaceDecl = new NamespaceDeclaration(tokens);
                VisualizationOptions options = 0
                    //| VisualizationOptions.PrintNames
                    | VisualizationOptions.PrintNullFieldNames
                    //| VisualizationOptions.PrintTypes
                    //| VisualizationOptions.LineFeedAfterType
                    ;
                DepthWriteStyle style = DepthWriteStyle.ColorPipes;
                //Visualizer.PrintObject(namespaceDecl, "Namespace", style, options);
                TypeChecker.TypeChecker.CheckNamespace(namespaceDecl);
            }
            catch (SyntaxTreeBuildingException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(ex.GetType().Name + ": ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ex.Message);
                if (ex is InvalidTokenException invToken)
                    Visualizer.PrintContext(tokens, invToken.Token, 10, 10);
            }

            Console.ReadLine();
        }
    }
}
