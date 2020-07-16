using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CodeGeneratorLib
{
    public class ClassInfo
    {
        public string Directory;
        public List<string> Flags { get; set; }

        public List<string> UsingStatements { get; set; }
        public string Namespace;

        public string Name;
        public List<string> InheritsFrom { get; set; }

        public List<KeyValuePair<string, string>> GetProperties { get; set; }
        public List<GetSetPropertyInfo> GetSetProperties { get; set; }
        public ReadOnlyCollection<FieldInfo> InstanceFields { get; private set; }
        public List<MethodInfo> Methods { get; set; }
        public List<string> ConstructorLines { get; set; }
        public ClassInfo(string name, List<FieldInfo> instanceFields, Context context)
        {
            Directory = context.Directory;
            Flags = context.Flags;
            UsingStatements = new List<string>()
            {
                "using System;",
                "using System.Collections.Generic;",
                "using System.Text;"
            };
            Namespace = context.Namespace;
            Name = name + context.Suffix;
            InheritsFrom = context.InheritsFrom;
            GetProperties = context.GetProperties;
            GetSetProperties = context.GetSetProperties;
            InstanceFields = instanceFields.AsReadOnly();

            ConstructorLines = new List<string>();
            foreach (FieldInfo field in instanceFields)
            {
                foreach(GetSetPropertyInfo property in field.GetDeclaration())
                {
                    GetSetProperties.Add(property);
                }
                foreach (string creationLine in field.GetCreationStatements())
                {
                    ConstructorLines.Add(creationLine);
                }
            }

            Methods = new List<MethodInfo>();
        }
    }
}
