using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos
{
    public abstract class ValueAttribute : AttributeInfo
    {
        public ValueAttribute(string keyword) : base(keyword)
        {

        }
        public abstract string GetConstructorParam();
        public abstract string[] GetBeforeCreationLines();
        public abstract string GetValue();
        public abstract string[] GetAfterCreationLines();
    }
}
