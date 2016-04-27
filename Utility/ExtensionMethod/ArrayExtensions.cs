
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility.ExtensionMethod
{
    public static class ArrayExtensions
    {
        public static List<object> ToList(this Array arr)
        {
            return arr.Cast<object>().ToList();
        }
    }
}
