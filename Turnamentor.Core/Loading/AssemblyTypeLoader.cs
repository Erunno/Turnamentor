using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Turnamentor.Core.Extensions;

namespace Turnamentor.Core.Loading
{
    class AssemblyTypeLoader : ITypeLoader
    {
        public IList<Type> LoadTypes<T>(IEnumerable<string> directories)
        {
            var assemblies = directories
                .Select(dir => GetAndLoadAssemblies(dir))
                .ConcatAll();

            var result =
                from assembly in assemblies
                from type in assembly.GetTypes()
                where typeof(T).IsAssignableFrom(type) && !type.IsInterface
                select type;

            return result.ToList();
        }

        private IEnumerable<Assembly> GetAndLoadAssemblies(string directory)
        {
            DirectoryInfo d = new DirectoryInfo(directory);
            FileInfo[] files = d.GetFiles("*", SearchOption.AllDirectories);
            
            return
                from file in files
                where IsWantedFile(file)
                select GetAssembly(file.FullName);
        }

        protected virtual Assembly GetAssembly(string fileName) => Assembly.LoadFrom(fileName);
        protected virtual bool IsWantedFile(FileInfo file) => file.Extension == ".dll" || file.Extension == ".exe";
    }
}
