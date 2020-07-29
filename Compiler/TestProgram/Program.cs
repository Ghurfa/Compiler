using Microsoft.VisualBasic.CompilerServices;
using Parser;
using Parser.SyntaxTreeItems;
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
            Compiler.Compiler.Compile(@"..\..\..\..\..\TestPrograms\TypeCheckerTest.txt", null);

            Console.WriteLine("Finished");
            Console.ReadLine();
            //ILDASM
            //PEVerify
        }
    }
}
