using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Turnamentor.Core.Loading
{
    class BothTypeLoader : ITypeLoader
    {
        SourceCodeTypeLoader sourceCodeLoader = new SourceCodeTypeLoader();
        AssemblyTypeLoader assemblyLoder = new AssemblyTypeLoader();

        public IList<Type> LoadTypes<T>(IEnumerable<string> directories)
        {
            var res1 = sourceCodeLoader.LoadTypes<T>(directories);
            var res2 = assemblyLoder.LoadTypes<T>(directories);

            return res1.Concat(res2).ToList();
        }
    }
}
