using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Turnamentor.Core.Loading
{
    class SourceCodeTypeLoader : AssemblyTypeLoader
    {
        protected override bool IsWantedFile(FileInfo file) => file.Extension == ".cs";

        protected override Assembly GetAssembly(string fileName)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            string assemblyName = GetAssemblyName(fileName);

            var parameters = new CompilerParameters();
            parameters.GenerateExecutable = false; // creating dll
            parameters.OutputAssembly = assemblyName;

            var results = provider.CompileAssemblyFromFile(parameters, fileName);

            return Assembly.LoadFrom(results.PathToAssembly);
        }

        private string GetAssemblyName(string fileName)
        {
            string shortName = GetShortName(fileName);
            string dllShortName = GetDllName(shortName);

            return Path.Combine(Environment.CurrentDirectory, "compiled_contestants", dllShortName);
        }

        private string GetShortName(string fileName)
        {
            var splitedName = fileName.Split(Path.DirectorySeparatorChar);
            return splitedName[splitedName.Length - 1];
        }

        private string GetDllName(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fileName.Length - 3; i++)
                sb.Append(fileName[i]);

            sb.Append(".dll");

            return sb.ToString();
        }
    }
}
