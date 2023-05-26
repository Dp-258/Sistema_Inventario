using System;
using System.Reflection;
using System.Runtime.Loader;

namespace MVCInventario.Extension
{
    public class CustomAssemblyLoadContext: AssemblyLoadContext
    {

        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }
        protected override IntPtr LoadUnmanagedDll(string unmanageedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanageedDllName);
        }
        protected override Assembly Load(AssemblyName assemblyName)
        {
            throw new NotImplementedException();
        }



    }
}
