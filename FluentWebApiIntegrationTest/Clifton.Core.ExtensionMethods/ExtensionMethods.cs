using System;
using System.Collections.Generic;

namespace Clifton.Core.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static void ForEach(this int n, Action<int> action, int offset = 0)
        {
            for (int i = offset; i < n + offset; i++)
            {
                action(i);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static void IfNotNull<T>(this T obj, Action action) where T: new()
        {
            if (obj != null)
            {
                action();
            }
        }
    }
}
