using System;
using System.Collections.Generic;
using System.Text;

namespace Turnamentor.Core.Loading
{
    interface ITypeLoader
    {
        IList<Type> LoadTypes<T>(IEnumerable<string> directories);
    }
}
