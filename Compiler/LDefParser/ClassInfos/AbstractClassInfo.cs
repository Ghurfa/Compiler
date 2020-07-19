using LDefParser.ProductionDefinitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDefParser.ClassInfos
{
    class AbstractClassInfo : ClassInfo
    {
        public SetDefinition SetDefinition;

        internal AbstractClassInfo(string name, string parent, SetDefinition setDefinition)
            : base(name, parent)
        {
            this.SetDefinition = setDefinition;
        }

        internal IEnumerable<ClassInfo> GetInnerClassInfos()
        {
            foreach (ClassInfo classInfo in ClassInfo.GetClassInfos(SetDefinition.Definitions))
            {
                yield return classInfo;
            }
            foreach (Production production in SetDefinition.Productions)
            {
                foreach (ClassInfo classInfo in ClassInfo.GetClassInfos(production.InnerDefinition))
                {
                    yield return classInfo;
                }
            }
        }
    }
}
