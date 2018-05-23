using System.Collections.Generic;
using System.Reflection;

namespace RGSoft.Reflections
{

	/// <summary>
	/// Utility class for loading assemblies
	/// </summary>
	public static class AssemblyLoader
	{
		private static readonly Dictionary<string, Assembly> CachedAssemblies = new Dictionary<string, Assembly>();

		/// <summary>
		/// Load and cache assembly
		/// </summary>
		/// <param name="assemblyName"></param>
		/// <returns></returns>
		public static Assembly Load(string assemblyName)
		{
			return CachedAssemblies.ContainsKey(assemblyName) ? CachedAssemblies[assemblyName] : GetAndCacheAssembly(assemblyName);
		}

		private static Assembly GetAndCacheAssembly(string assemblyName)
		{
			var assembly = Assembly.Load(assemblyName);
			CachedAssemblies.Add(assemblyName, assembly);
			return assembly;
		}
	}
}


