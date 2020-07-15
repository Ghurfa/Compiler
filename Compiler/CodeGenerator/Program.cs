using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    struct A
    {
        public string a;
        public A Clone()
        {
            var newA = new A();
            newA.a = a;
            return newA;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TokensGenerator.GenerateTokenClasses(tokensDefPath: @"..\..\..\..\..\Definitions\TokenTypes.txt",
                                                  baseDir: @"..\..\..\..\..\Compiler\Compiler\Tokens\",
                                                  TCBPath: @"..\..\..\..\..\Compiler\Compiler\TokenCollectionBuilder.cs");

            ExpressionsGenerator.GenerateItems(baseDir: @"..\..\..\..\..\Compiler\Compiler\SyntaxTreeItems\",
                                               filePath: @"..\..\..\..\..\Definitions\ExpressionDefinitions.txt",
                                               TokensGenerator.TokenNames.ToArray());
        }
    }
}
