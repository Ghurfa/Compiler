using Compiler;
using Compiler.SyntaxTreeItems;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText(@"..\..\..\..\..\PrecedenceTest.txt");
            var tokens = Tokenizer.Tokenize(text);
            try
            {
                NamespaceDeclaration namespaceDecl = new NamespaceDeclaration(tokens);
                VisualizationOptions options = 0
                    //| VisualizationOptions.PrintNames
                    | VisualizationOptions.PrintNullFieldNames
                    //| VisualizationOptions.PrintTypes
                    //| VisualizationOptions.LineFeedAfterType
                    ;
                DepthWriteStyle style = DepthWriteStyle.ColorPipes;
                Visualizer.PrintObject(namespaceDecl, "Namespace", style, options);
            }
            catch (InvalidTokenException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(ex.GetType().Name + ": ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ex.Message);
                Visualizer.PrintContext(tokens, ex.Token, 10, 10);
            }
            catch (InvalidStatementException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(ex.GetType().Name + ": ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ex.Message);
            }
            catch (InvalidAssignmentLeftException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(ex.GetType().Name + ": ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
