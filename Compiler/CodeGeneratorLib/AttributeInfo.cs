﻿using CodeGeneratorLib.AttributeInfos;
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
                case "notpop": return new NotPopAttribute(parts);
                case "disablecreation": return new DisableCreationAttribute(parts);
                case "backingfield": return new BackingFieldAttribute(parts);
                default: throw new InvalidOperationException();
            }            
        }
    }
}
