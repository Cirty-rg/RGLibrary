using System;
using System.Collections.Generic;
using System.Reflection;

namespace RGSoft.Reflections
{

	/// <summary>
	/// Utility class for loading types from assembly
	/// </summary>
	public static class TypeLoader
	{

		private static readonly Dictionary<string, Type> CachedTypes = new Dictionary<string, Type>();


		/// <summary>
		/// Load and cache type
		/// </summary>
		/// <param name="assemblyName"></param>
		/// <param name="typePath"></param>
		/// <returns></returns>
		public static Type Load(string assemblyName, string typePath)
		{
			var fullName = assemblyName + ":" + typePath;
			if (CachedTypes.ContainsKey(fullName)) return CachedTypes[fullName];
			var assembly = AssemblyLoader.Load(assemblyName);
			return GetAndCacheType(assembly, typePath, fullName);
		}



		private static Type GetAndCacheType(Assembly assembly, string typeFullName, string key)
		{
			var type = assembly.GetType(typeFullName);
			CachedTypes.Add(key, type);
			return type;
		}
	}
}

