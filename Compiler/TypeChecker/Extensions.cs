using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker
{
    static class Extensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}
