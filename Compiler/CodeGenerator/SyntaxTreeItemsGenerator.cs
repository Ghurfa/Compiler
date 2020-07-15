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
    static partial class SyntaxTreeItemsGenerator
    {
        struct Context
        {
            public string Directory { get; set; }
            public string Suffix { get; set; }

            private string pattern;
            public string Pattern
            {
                get => pattern;
                set
                {
                    if (pattern == "") pattern = value;
                    else throw new InvalidOperationException();
                }
            }
            public LinkedList<string> Interfaces { get; }
            public Context(string directory)
            {
                Directory = directory;
                Suffix = "";
                pattern = "";
                Interfaces = new LinkedList<string>();
            }
            public Context Clone()
            {
                Context ret = new Context(Directory);
                ret.Suffix = Suffix;
                ret.pattern = pattern;
                foreach (string interfaceStr in Interfaces)
                {
                    ret.Interfaces.AddLast(interfaceStr);
                }
                return ret;
            }
        }

        public static void GenerateItems(string baseDir, string definitionsFolderPath, string[] tokenNames)
        {
            ClearBaseDirectory(baseDir);
            EnumerationOptions searchOptions = new EnumerationOptions();
            searchOptions.RecurseSubdirectories = true;

            foreach (string filePath in Directory.GetFiles(definitionsFolderPath, "*.*", searchOptions))
            {
                string[] lines = File.ReadAllLines(filePath);
                int i = 0;
                GenerateSet(lines, ref i, tokenNames, new Context(baseDir));
            }
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
                    if (innerDir.Name != "SpeciallyDefined") innerDir.Delete(true);
                }
            }
            else
            {
                Directory.CreateDirectory(baseDir);
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
                    case '>': context.Directory += current.Substring(1) + "\\"; i++; break;
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

            var fieldInfos = CreateFieldInfoList(fieldDeclarations, tokenNames).ToArray();
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
                    case '>': context.Directory += current.Substring(1) + "\\"; break;
                    case '$': context.Suffix = current.Substring(1) + context.Suffix; break;
                    case '+': context.Interfaces.AddLast(current.Substring(1)); break;
                    case '=': throw new InvalidOperationException();
                    case '{': i++; GenerateSet(lines, ref i, tokenNames, context.Clone()); break;
                    default:
                        var (type, name) = FieldInfo.ReadDeclaration(current, tokenNames); //Re-reads in fieldInfo.CreateFieldInfo
                        var classItem = ClassItemInfo.CreateItemInfo(current, tokenNames);
                        if (classItem is TokenFieldInfo tokenFieldInfo)
                        {
                            fieldInfos[indexToReplace] = tokenFieldInfo;
                            GenerateFile(name + context.Suffix, context.Interfaces, fieldInfos, context.Directory);
                            break;
                        }
                        else throw new InvalidOperationException();
                }
            }
        }
        private static void GenerateItem(string[] lines, ref int i, string[] tokenNames, Context context)
        {
            string itemName = lines[i++].Trim().Substring(1);
            LinkedList<ClassItemInfo> items = new LinkedList<ClassItemInfo>();

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
                    default: items.AddLast(ClassItemInfo.CreateItemInfo(current, tokenNames)); break;
                }
                i++;
            }
            GenerateFile(itemName + context.Suffix, context.Interfaces, items, context.Directory);
        }
    }
}
