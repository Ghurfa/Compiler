using CodeGeneratorLib.AttributeInfos;
using CodeGeneratorLib.AttributeInfos.ConditionAttributes;
using CodeGeneratorLib.AttributeInfos.ValueAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib
{
    public abstract class AttributeInfo
    {
        public string Keyword { get; set; }

        protected AttributeInfo(string keyword)
        {
            Keyword = keyword;
        }

        public static AttributeInfo ReadAttribute(string text)
        {
            string[] parts = text.Split(' ');
            switch (parts[0].ToLower())
            {
                //General flags
                case "disablecreation": return new DisableCreationAttribute(parts);
                case "backingfield": return new BackingFieldAttribute(parts);

                //Conditions
                case "notpop": return new NotPopAttribute(parts);
                case "peek": return new PeekAttribute(parts);
                case "notmissingcomma": return new NotMissingCommaAttribute(parts);

                //Initialization modifiers
                case "optional": return new OptionalAttribute(parts);
                case "argument": return new ArgumentAttribute(parts);
                case "optionalarg": return new OptionalArgAttribute(parts);
                case "ensurevalidstatementending": return new ValidStatementEndingAttribute(parts);
                default: throw new InvalidOperationException();
            }            
        }
    }
}
