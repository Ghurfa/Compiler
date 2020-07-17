using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib
{
    abstract class AttributeInfo
    {
        public abstract string Keyword { get; set; }

        public static AttributeInfo ReadAttribute(string text)
        {
            string[] parts = text.Split(' ');
            switch (parts[0].ToLower())
            {
                case "untilpop": return new UntilPopAttribute(text, parts);
                case "disablecreation": return new DisableCreationAttribute(text, parts);
            }            
        }
    }
}
