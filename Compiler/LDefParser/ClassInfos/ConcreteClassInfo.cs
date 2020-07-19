using LDefParser.ProductionDefinitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDefParser.ClassInfos
{
    class ConcreteClassInfo : ClassInfo
    {
        public SingleDefinition SingleDefinition { get; set; }

        internal ConcreteClassInfo(string name, string parent, SingleDefinition singleDef)
            :base(name, parent)
        {
            SingleDefinition = singleDef;
        }
    }
}
