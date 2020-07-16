using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeGeneratorLib
{
    public struct Context
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
        public string Namespace;
        public List<KeyValuePair<string, string>> GetProperties { get; }
        public List<GetSetPropertyInfo> GetSetProperties { get; }
        public List<string> InheritsFrom { get; }
        public List<string> Flags { get; }
        public Context(string directory, string namespaceStr)
        {
            Directory = directory;
            Suffix = "";
            pattern = "";
            Namespace = namespaceStr;
            GetProperties = new List<KeyValuePair<string, string>>();
            GetSetProperties = new List<GetSetPropertyInfo>();
            InheritsFrom = new List<string>();
            Flags = new List<string>();
        }
        public Context Clone()
        {
            Context ret = new Context(Directory, Namespace);
            ret.Suffix = Suffix;
            ret.pattern = pattern;
            foreach (KeyValuePair<string, string> pair in GetProperties) ret.GetProperties.Add(pair);
            foreach (GetSetPropertyInfo prop in GetSetProperties) ret.GetSetProperties.Add(prop);
            foreach (string item in InheritsFrom) ret.InheritsFrom.Add(item);
            foreach (string flag in Flags) ret.Flags.Add(flag);
            return ret;
        }
    }
}
