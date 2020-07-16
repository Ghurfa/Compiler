using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib
{
    public static class Extensions
    {
        public static List<T> Clone<T>(this List<T> list)
        {
            List<T> newList = new List<T>();
            foreach(T item in list)
            {
                newList.Add(item);
            }
            return newList;
        }
    }
}
