using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Turnamentor.Core.Extensions
{
    static class IEnumerableExtensions
    {
        public static IEnumerable<T> ConcatAll<T>(this IEnumerable<IEnumerable<T>> enumerables)
        {
            foreach (var enumerable in enumerables)
                foreach (var item in enumerable)
                    yield return item;
        }

    }
}
