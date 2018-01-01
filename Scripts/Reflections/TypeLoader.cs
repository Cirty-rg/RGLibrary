using System;
using System.Collections.Generic;
using System.Reflection;

namespace RGSoft.Reflections
{

    /// <summary>
    /// Utility class for loading types from assembly
    /// </summary>
    internal static class TypeLoader
    {

        private static readonly Dictionary<string, Type> CachedTypes = new Dictionary<string, Type>();


        /// <summary>
        /// Load and cache type
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type Load(string assemblyName, string typeName)
        {
            var fullName = assemblyName + "." + typeName;
            if (CachedTypes.ContainsKey(fullName)) return CachedTypes[fullName];
            var assembly = AssemblyLoader.Load(assemblyName);
            return GetAndCacheType(assembly, fullName);
        }

       

        private static Type GetAndCacheType(Assembly assembly, string typeFullName)
        {
            var type = assembly.GetType(typeFullName);
            CachedTypes.Add(typeFullName,type);
            return type;
        }
    }
}

