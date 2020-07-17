using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos
{
    public class BackingFieldAttribute : AttributeInfo
    {
        public string Name { get; set; }
        public BackingFieldAttribute(string[] parts)
            : base(parts[0])
        {
            if (parts.Length != 2) throw new InvalidOperationException();
            Name = parts[1];
        }
    }
}
