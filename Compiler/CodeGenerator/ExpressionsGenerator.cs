using CodeGenerator.ClassItems;
using CodeGenerator.SyntaxTreeItemsFieldInfos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    static class ExpressionsGenerator
    {
        public static void GenerateItems(string baseDir, string filePath, string[] tokenNames)
        {
            //ClearBaseDirectory(baseDir);
            EnumerationOptions searchOptions = new EnumerationOptions();
            searchOptions.RecurseSubdirectories = true;

            string[] lines = File.ReadAllLines(filePath);
            int i = 0;
            GenerateSet(lines, ref i, tokenNames, new Context(baseDir));
        }

        private static void ClearDirectory(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                DirectoryInfo dir = new DirectoryInfo(dirPath);
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo innerDir in dir.EnumerateDirectories())
                {
                    innerDir.Delete(true);
                }
            }
        }

        private static void GenerateSet(string[] lines, ref int i, string[] tokenNames, Context context)
        {
            while (i < lines.Length)
            {
                string current = lines[i].Trim();

                if (current.Length == 0) { i++; continue; }
                if (current == "}") { i++; return; }

                if (context.Pattern == "") GenerateBlock(lines, ref i, tokenNames, context.Clone());
                else GeneratePatternedBlock(lines, ref i, tokenNames, context.Clone());
            }
        }

        private static void GenerateBlock(string[] lines, ref int i, string[] tokenNames, Context context)
        {
            while (i < lines.Length)
            {
                string current = lines[i].Trim();

                if (current.Length == 0 || current == "}") return;

                switch (current.First())
                {
                    case '/': i++; break;
                    case '>':
                        {
                            if (current[1] == '#')
                            {
                                context.Directory += current.Substring(2) + "\\";
                                ClearDirectory(context.Directory);
                            }
                            else
                            {
                                context.Directory += current.Substring(1) + "\\";
                            }
                            i++;
                            break;
                        }
                    case '$': context.Suffix = current.Substring(1) + context.Suffix; i++; break;
                    case '+': context.Interfaces.AddLast(current.Substring(1)); i++; break;
                    case '=': context.Pattern = current.Substring(1); i++; break;
                    case '{': i++; GenerateSet(lines, ref i, tokenNames, context.Clone()); break;
                    case '.': GenerateItem(lines, ref i, tokenNames, context.Clone()); break;
                    default: throw new InvalidOperationException();
                }
            }
        }

        private static void GeneratePatternedBlock(string[] lines, ref int i, string[] tokenNames, Context context)
        {
            string[] fieldDeclarations = context.Pattern.Split(' ', '\t');

            var fieldInfos = FieldInfo.CreateFieldInfoList(fieldDeclarations, tokenNames);
            int indexToReplace;
            for (indexToReplace = 0; indexToReplace < fieldInfos.Length; indexToReplace++)
            {
                if (fieldInfos[indexToReplace] is PlaceHolderFieldInfo && fieldInfos[indexToReplace].Name == "Token")
                {
                    break;
                }
            }
            if (indexToReplace == fieldInfos.Length) throw new InvalidOperationException();

            for (; i < lines.Length; i++)
            {
                string current = lines[i].Trim();

                if (current.Length == 0 || current == "}") return;

                switch (current.First())
                {
                    case '/': break;
                    case '>':
                        {
                            if (current[1] == '#')
                            {
                                context.Directory += current.Substring(2) + "\\";
                                ClearDirectory(context.Directory);
                            }
                            else
                            {
                                context.Directory += current.Substring(1) + "\\";
                            }
                            break;
                        }
                    case '$': context.Suffix = current.Substring(1) + context.Suffix; break;
                    case '+': context.Interfaces.AddLast(current.Substring(1)); break;
                    case '=': throw new InvalidOperationException();
                    case '{': i++; GenerateSet(lines, ref i, tokenNames, context.Clone()); break;
                    default:
                        var fieldInfo = FieldInfo.GetFieldInfo(current, tokenNames);
                        if (fieldInfo is TokenFieldInfo tokenFieldInfo)
                        {
                            fieldInfos[indexToReplace] = tokenFieldInfo;
                            ExpressionsFileWriter.GenerateFile(tokenFieldInfo.Name + context.Suffix, context.Interfaces, fieldInfos, context.Directory);
                            break;
                        }
                        else throw new InvalidOperationException();
                }
            }
        }
        private static void GenerateItem(string[] lines, ref int i, string[] tokenNames, Context context)
        {
            string itemName = lines[i++].Trim().Substring(1);
            LinkedList<FieldInfo> items = new LinkedList<FieldInfo>();

            while (i < lines.Length)
            {
                string current = lines[i].Trim();

                if (current.Length == 0 || current == "}") break;

                switch (current.First())
                {
                    case '/': break;
                    case '>': context.Directory += current.Substring(1) + "\\"; break;
                    case '$': context.Suffix = current.Substring(1) + context.Suffix; break;
                    case '+': context.Interfaces.AddLast(current.Substring(1)); break;
                    case '=': throw new InvalidOperationException();
                    case '{': throw new InvalidOperationException();
                    default: items.AddLast(FieldInfo.GetFieldInfo(current, tokenNames)); break;
                }
                i++;
            }
            ExpressionsFileWriter.GenerateFile(itemName + context.Suffix, context.Interfaces, items, context.Directory);
        }
    }
}
