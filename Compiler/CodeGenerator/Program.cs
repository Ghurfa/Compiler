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
        static void CreateTokenClasses()
        {
            string path = @"..\..\..\..\..\TokenTypes.txt";
            string[] lines = File.ReadAllLines(path);

            string baseDir = @"..\..\..\..\..\Compiler\Compiler\Tokens\";
            if(Directory.Exists(baseDir))
            {
                DirectoryInfo dir = new DirectoryInfo(baseDir);
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo innerDir in dir.EnumerateDirectories())
                {
                    innerDir.Delete(true);
                }
            }
            else
            {
                Directory.CreateDirectory(baseDir);
            }

            LinkedList<string> caseStatements = new LinkedList<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                string firstLine = lines[i];
                if (firstLine.Length == 0) continue;

                if (firstLine.Length <= 2) throw new InvalidOperationException();
                string subFolderName;
                if (firstLine.Substring(0, 2) == "//")
                {
                    StringBuilder folderNameBuilder = new StringBuilder();
                    bool shouldCapitalize = true;
                    for(int j = 2; j < firstLine.Length; j++)
                    {
                        if (char.IsWhiteSpace(firstLine[j]))
                        {
                            shouldCapitalize = true;
                            continue;
                        }
                        if (shouldCapitalize) folderNameBuilder.Append(firstLine[j].ToString().ToUpper());
                        else folderNameBuilder.Append(firstLine[j].ToString().ToLower());
                        shouldCapitalize = false;
                    }
                    subFolderName = folderNameBuilder.ToString();
                    i++;
                }
                else
                {
                    subFolderName = "";
                }

                Directory.CreateDirectory(baseDir + subFolderName);
                string subFolder = baseDir + subFolderName + "\\";

                string interfaces = "";
                while (i < lines.Length)
                {
                    string line = lines[i].Trim();
                    foreach (char character in line)
                    {
                        if (char.IsWhiteSpace(character)) throw new InvalidOperationException();
                    }
                    if (line.Length == 0) break;

                    if (line[0] == '=')
                    {
                        string interfaceName = "I" + line.Substring(1) + "Token";
                        string inheritsFrom = interfaces == "" ? " : IToken" : " : " + interfaces;
                        string[] text = new string[]
                        {
                            "using System;",
                            "using System.Collections.Generic;",
                            "using System.Text;",
                            "",
                            "namespace Compiler",
                            "{",
                            $"    public interface {interfaceName}{inheritsFrom}",
                            "    {",
                            "    }",
                            "}"
                        };
                        File.WriteAllLines(subFolder + interfaceName + ".cs", text);
                    }
                    else if (line[0] == '+')
                    {
                        if (interfaces.Length != 0) interfaces += ", ";
                        interfaces += "I" + line.Substring(1) + "Token";
                    }
                    else
                    {
                        string tokenName = line + "Token";
                        string inheritsFrom = interfaces == "" ? " : IToken" : " : " + interfaces;

                        string[] text = new string[]
                        {
                            "using System;",
                            "using System.Collections.Generic;",
                            "using System.Text;",
                            "",
                            "namespace Compiler",
                            "{",
                            $"    public struct {tokenName}{inheritsFrom}",
                            "    {",
                            "        public string Text { get; private set; }",
                            "        public int Index { get; private set; }",
                            "",
                            $"        public {tokenName}(string text, int index)",
                            "        {",
                            "            Text = text;",
                            "            Index = index;",
                            "        }",
                            "",
                            "        public override string ToString() => Text;",
                            "    }",
                            "}"
                        };
                        caseStatements.AddLast($"                case TokenType.{line}: return new {tokenName}(text, index);");
                        File.WriteAllLines(subFolder + tokenName + ".cs", text);
                    }
                    i++;
                }
            }

            string TokenCollectionBuilderPath = @"..\..\..\..\..\Compiler\Compiler\TokenCollectionBuilder.cs";
            string[] TCBLines = File.ReadAllLines(TokenCollectionBuilderPath);
            LinkedList<string> newTCBLines = new LinkedList<string>();
            int m;
            for (m = 0; TCBLines[m] != "            switch(type)"; m++)
                newTCBLines.AddLast(TCBLines[m]);
            newTCBLines.AddLast(TCBLines[m++]); //Switch
            newTCBLines.AddLast(TCBLines[m++]); //Open brace
            foreach (string newLine in caseStatements)
            {
                newTCBLines.AddLast(newLine);
            }
            newTCBLines.AddLast("                default: throw new NotImplementedException();");
            while (TCBLines[m] != "            }") m++;
            while (m < TCBLines.Length) newTCBLines.AddLast(TCBLines[m++]);
            File.WriteAllLines(TokenCollectionBuilderPath, newTCBLines.ToArray());
        }
        static void Main(string[] args)
        {
            CreateTokenClasses();
        }
    }
}
