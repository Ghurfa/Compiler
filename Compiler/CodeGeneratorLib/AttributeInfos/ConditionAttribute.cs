using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos
{
    public abstract class ConditionAttribute : AttributeInfo
    {
        protected ConditionAttribute(string keyword) : base(keyword)
        {

        }

        public abstract string[] GetInitializingStatements();
        public abstract string GetCondition();
        public abstract string[] GetUpdateStatements();
    }
}
