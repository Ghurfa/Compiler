using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib
{
    public class GetSetPropertyInfo
    {
        public List<string> Modifiers { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Get { get; set; }
        public string Set { get; set; }
        public string BackingFieldName { get; set; }
        public GetSetPropertyInfo(string type, string name, string get, string set, string backingFieldName = null, params string[] modifiers)
        {
            Type = type ?? throw new InvalidOperationException();
            Name = name ?? throw new InvalidOperationException();
            Get = get ?? "get;";
            Set = set ?? "set;";
            BackingFieldName = backingFieldName;
            if (modifiers == null || modifiers.Length == 0) Modifiers = new List<string>() { "public" };
            else
            {
                Modifiers = new List<string>();
                foreach (string modifier in modifiers) Modifiers.Add(modifier);
            }
        }
    }
}
