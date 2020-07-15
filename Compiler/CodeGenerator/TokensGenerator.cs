using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    static class TokensGenerator
    {
        public static List<string> TokenNames = new List<string>();
        public static void GenerateTokenClasses(string tokensDefPath, string baseDir, string TCBPath)
        {
            TokenNames.Clear();
            string[] lines = File.ReadAllLines(tokensDefPath);
            ClearBaseDirectory(baseDir);

            LinkedList<string> caseStatements = new LinkedList<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                string firstLine = lines[i];
                if (firstLine.Length == 0) continue;

                ReadBlock(baseDir, caseStatements, lines, ref i);
            }
            UpdateTokenCollectionBuilder(caseStatements, TCBPath);
        }
        private static void ClearBaseDirectory(string baseDir)
        {
            if (Directory.Exists(baseDir))
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
        }
        private static void ReadBlock(string baseDir, LinkedList<string> caseStatements, string[] lines, ref int i)
        {
            string subFolderName = ReadSubFolderName(lines[i], ref i);

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
                    CreateInterface(subFolder, line, interfaces);
                }
                else if (line[0] == '+')
                {
                    if (interfaces.Length != 0) interfaces += ", ";
                    interfaces += "I" + line.Substring(1) + "Token";
                }
                else
                {
                    CreateTokenClass(subFolder, line, interfaces, caseStatements);
                }
                i++;
            }
        }
        private static string ReadSubFolderName(string line, ref int i)
        {
            if (line.Length <= 2) throw new InvalidOperationException();
            if (line.Substring(0, 2) == "//")
            {
                StringBuilder folderNameBuilder = new StringBuilder();
                bool shouldCapitalize = true;
                for (int j = 2; j < line.Length; j++)
                {
                    if (char.IsWhiteSpace(line[j]))
                    {
                        shouldCapitalize = true;
                        continue;
                    }
                    if (shouldCapitalize) folderNameBuilder.Append(line[j].ToString().ToUpper());
                    else folderNameBuilder.Append(line[j].ToString().ToLower());
                    shouldCapitalize = false;
                }
                i++;
                return folderNameBuilder.ToString();
            }
            else
            {
                return "";
            }
        }
        private static void CreateInterface(string subFolder, string line, string interfaces)
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
        private static void CreateTokenClass(string subFolder, string line, string interfaces, LinkedList<string> caseStatements)
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
            TokenNames.Add(tokenName);
            caseStatements.AddLast($"                case TokenType.{line}: return new {tokenName}(text, index);");
            File.WriteAllLines(subFolder + tokenName + ".cs", text);
        }
        private static void UpdateTokenCollectionBuilder(LinkedList<string> caseStatements, string TCBPath)
        {
            string[] TCBLines = File.ReadAllLines(TCBPath);
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
            File.WriteAllLines(TCBPath, newTCBLines.ToArray());
        }
    }
}
