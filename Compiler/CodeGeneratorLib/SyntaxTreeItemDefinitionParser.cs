using CodeGeneratorLib.SyntaxTreeItemsFieldInfos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeGeneratorLib
{
    public static class SyntaxTreeItemDefinitionParser
    {
        public static List<ClassInfo> ParseFile(string baseDir, string filePath, string namespaceName, string[] tokenNames)
        {
            string[] lines = File.ReadAllLines(filePath);
            int i = 0;
            return ParseSet(lines, ref i, tokenNames, new Context(baseDir, namespaceName));
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

        private static List<ClassInfo> ParseSet(string[] lines, ref int i, string[] tokenNames, Context context)
        {
            var ret = new List<ClassInfo>();
            while (i < lines.Length)
            {
                string current = lines[i].Trim();

                if (current.Length == 0) { i++; continue; }
                if (current == "}") { i++; return ret; }

                if (context.Pattern == "")
                {
                    List<ClassInfo> newClasses = ParseBlock(lines, ref i, tokenNames, context.Clone());
                    foreach (ClassInfo newClass in newClasses) ret.Add(newClass);
                }
                else
                {
                    List<ClassInfo> newClasses = ParsePatternedBlock(lines, ref i, tokenNames, context.Clone());
                    foreach (ClassInfo newClass in newClasses) ret.Add(newClass);
                }
            }
            return ret;
        }

        private static List<ClassInfo> ParseBlock(string[] lines, ref int i, string[] tokenNames, Context context)
        {
            var ret = new List<ClassInfo>();
            while (i < lines.Length)
            {
                string current = lines[i].Trim();

                if (current.Length == 0 || current == "}") return ret;

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
                    case '+': context.InheritsFrom.Add(current.Substring(1)); i++; break;
                    case '=': context.Pattern = current.Substring(1); i++; break;
                    case '~': context.Flags.Add(current.Substring(1)); i++; break;
                    case ':':
                        {
                            string[] parts = current.Split('=');
                            if (parts.Length != 2) throw new InvalidOperationException();
                            context.GetProperties.Add(new KeyValuePair<string, string>(parts[0].Substring(1), parts[1]));
                            i++;
                            break;
                        }
                    case '{':
                        i++;
                        {
                            List<ClassInfo> newClasses = ParseSet(lines, ref i, tokenNames, context.Clone());
                            foreach (ClassInfo newClass in newClasses) ret.Add(newClass);
                            break;
                        }
                    case '.': ret.Add(GenerateItem(lines, ref i, tokenNames, context.Clone())); break;
                    default: throw new InvalidOperationException();
                }
            }
            return ret;
        }

        private static List<ClassInfo> ParsePatternedBlock(string[] lines, ref int i, string[] tokenNames, Context context)
        {
            string[] fieldDeclarations = context.Pattern.Split(' ', '\t');

            var fieldInfos = FieldInfo.CreateFieldInfoList(fieldDeclarations, tokenNames);
            int indexToReplace;
            for (indexToReplace = 0; indexToReplace < fieldInfos.Count; indexToReplace++)
            {
                if (fieldInfos[indexToReplace] is PlaceHolderFieldInfo && fieldInfos[indexToReplace].Name == "Token")
                {
                    break;
                }
            }
            if (indexToReplace == fieldInfos.Count) throw new InvalidOperationException();

            var ret = new List<ClassInfo>();
            while (i < lines.Length)
            {
                string current = lines[i].Trim();

                if (current.Length == 0 || current == "}")
                    return ret;

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
                    case '+': context.InheritsFrom.Add(current.Substring(1)); i++; break;
                    case '=': throw new InvalidOperationException();
                    case '~': context.Flags.Add(current.Substring(1)); i++; break;
                    case ':':
                        {
                            string[] parts = current.Split('=');
                            if (parts.Length != 2) throw new InvalidOperationException();
                            context.GetProperties.Add(new KeyValuePair<string, string>(parts[0].Substring(1), parts[1]));
                            i++;
                            break;
                        }
                    case '{':
                        i++;
                        {
                            List<ClassInfo> newClasses = ParseSet(lines, ref i, tokenNames, context.Clone());
                            foreach (ClassInfo newClass in newClasses) ret.Add(newClass);
                            break;
                        }
                    default:
                        var fieldInfo = FieldInfo.GetFieldInfo(current, tokenNames);
                        if (fieldInfo is TokenFieldInfo tokenFieldInfo)
                        {
                            fieldInfos[indexToReplace] = tokenFieldInfo;
                            ret.Add(new ClassInfo(tokenFieldInfo.Name, fieldInfos.Clone(), context.Clone()));
                            i++;
                            break;
                        }
                        else throw new InvalidOperationException();
                }
            }
            return ret;
        }
        private static ClassInfo GenerateItem(string[] lines, ref int i, string[] tokenNames, Context context)
        {
            string itemName = lines[i++].Trim().Substring(1);
            List<FieldInfo> fields = new List<FieldInfo>();

            while (i < lines.Length)
            {
                string current = lines[i].Trim();

                if (current.Length == 0 || current == "}") break;

                switch (current.First())
                {
                    case '/': break;
                    case '>': context.Directory += current.Substring(1) + "\\"; break;
                    case '$': context.Suffix = current.Substring(1) + context.Suffix; break;
                    case '+': context.InheritsFrom.Add(current.Substring(1)); break;
                    case '=': throw new InvalidOperationException();
                    case '{': throw new InvalidOperationException();
                    default: fields.Add(FieldInfo.GetFieldInfo(current, tokenNames)); break;
                }
                i++;
            }
            return new ClassInfo(itemName, fields.Clone(), context.Clone());
        }
    }
}
