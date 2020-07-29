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
                                                 baseDir: @"..\..\..\..\..\Compiler\Tokenizer\Tokens\",
                                                 TCBPath: @"..\..\..\..\..\Compiler\Tokenizer\TokenCollectionBuilder.cs");

            ExpressionsGenerator.Generate();
            MiscellaneousGenerator.Generate();
            TypesGenerator.Generate();
            StatementsGenerator.Generate();
        }
    }
}
