using LDefParser.ClassInfos;
using LDefParser.ProductionDefinitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDefParser
{
    abstract class ClassInfo
    {
        public string Name { get; protected set; }
        public string Parent { get; protected set; }

        public string LowerCaseName => Name.Substring(0, 1).ToLower() + Name.Substring(1);

        protected ClassInfo(string name, string parent)
        {
            Name = name;
            Parent = parent;
        }

        internal static IEnumerable<ClassInfo> GetClassInfos(IEnumerable<Definition> definitions)
        {
            List<ClassInfo> ret = new List<ClassInfo>();
            foreach(Definition def in definitions)
            {
                foreach(ClassInfo classInfo in GetClassInfos(def))
                {
                    yield return classInfo;
                }
            }
        }

        internal static IEnumerable<ClassInfo> GetClassInfos(Definition definition)
        {
            if (definition.ProductionDefinition is SingleDefinition singleDef)
            {
                yield return new ConcreteClassInfo(definition.Name, definition.Parent, singleDef);
            }
            else if (definition.ProductionDefinition is SetDefinition setDef)
            {
                var abstractClassInfo =  new AbstractClassInfo(definition.Name, definition.Parent, setDef);
                yield return abstractClassInfo;
                foreach (ClassInfo classInfo in abstractClassInfo.GetInnerClassInfos())
                {
                    yield return classInfo;
                }
            }
            else throw new InvalidOperationException();
        }
    }
}
