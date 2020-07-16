using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib
{
    public class MethodInfo
    {
        public string Signature { get; set; }
        public List<string> Body;

        public MethodInfo()
        {
            Signature = "";
            Body = new List<string>();
        }
        public MethodInfo(string signature)
        {
            Signature = signature;
            Body = new List<string>();
        }
        public MethodInfo(string signature, List<string> body)
        {
            Signature = signature;
            Body = body;
        }
    }
}
