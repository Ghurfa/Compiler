using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator
{
    static class Extensions
    {
        public static string[] ExceptFirst(this string[] arr)
        {
            if (arr.Length == 0) throw new InvalidOperationException();
            var ret = new string[arr.Length - 1];
            for(int i = 0; i < ret.Length; i++)
            {
                ret[i] = arr[i + 1];
            }
            return ret;
        }
    }
}
