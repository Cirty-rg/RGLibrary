using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RGSoft.NativeApi;
using UnityEngine;

namespace RGSoft.Debuggers
{
	[FilePath("ConsoleConfiguration", FilePathAttribute.Location.PreferencesFolder)]
	internal class ConsoleConfiguration : ScriptableSingleton<ConsoleConfiguration>
	{

		[SerializeField]
		private List<LogFilter> defaultFilters = new List<LogFilter>
		{
			DefaultLogFilters.Log,
			DefaultLogFilters.Warning,
			DefaultLogFilters.Error,
			DefaultLogFilters.Exception,
			DefaultLogFilters.Assert,
		};

		[SerializeField]
		private List<LogFilter> customFilters = new List<LogFilter>();

		[SerializeField]
		private Color selectEntryColor = new Color(0.29f, 0.29f, 1f);

		/// <summary>
		/// Default log filtering settings.
		/// </summary>
		public static ReadOnlyCollection<LogFilter> DefaultFilters => Instance.defaultFilters.AsReadOnly();

		/// <summary>
		/// Custom log filtering settings.
		/// </summary>
		public static List<LogFilter> CustomFilters => Instance.customFilters;

		public static Color SelectEntryColor => Instance.selectEntryColor;

		/// <summary>
		/// Save configuration.
		/// </summary>
		public static void Save() => Instance.Save(true);


		public static Color GetLogColor(ConsoleEntry entry)
		{
			foreach (var filter in DefaultFilters.Reverse())
			{
				if (!filter.IsMatch(entry))
					continue;
				return filter.BackgroundColor;
			}

			return DefaultLogFilters.Other.BackgroundColor;
		}
	}
}


