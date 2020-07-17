using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos
{
    public class DisableCreationAttribute : AttributeInfo
    {
        public DisableCreationAttribute(string[] parts)
            : base(parts[0])
        {
            if (parts.Length != 1) throw new InvalidOperationException();
        }
    }
}
