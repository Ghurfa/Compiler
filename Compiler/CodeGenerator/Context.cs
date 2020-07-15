using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator
{
    struct Context
    {
        public string Directory { get; set; }
        public string Suffix { get; set; }

        private string pattern;
        public string Pattern
        {
            get => pattern;
            set
            {
                if (pattern == "") pattern = value;
                else throw new InvalidOperationException();
            }
        }
        public LinkedList<string> Interfaces { get; }
        public Context(string directory)
        {
            Directory = directory;
            Suffix = "";
            pattern = "";
            Interfaces = new LinkedList<string>();
        }
        public Context Clone()
        {
            Context ret = new Context(Directory);
            ret.Suffix = Suffix;
            ret.pattern = pattern;
            foreach (string interfaceStr in Interfaces)
            {
                ret.Interfaces.AddLast(interfaceStr);
            }
            return ret;
        }
    }
}
