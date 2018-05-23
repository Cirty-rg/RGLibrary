using System;
using System.Collections.Generic;
using System.Linq;
using RGSoft.NativeApi;
using UnityEngine;

namespace RGSoft.Debuggers
{
	public class ConsoleEntries : ScriptableSingleton<ConsoleEntries>
	{

		[SerializeField] private string searchText = string.Empty;
		[SerializeField] private LogCache logCache = new LogCache();
		[SerializeField] private List<ConsoleEntry> allLogs = new List<ConsoleEntry>();
		[SerializeField] private List<ConsoleEntry> filteredLogs = new List<ConsoleEntry>();
		[SerializeField] private LogEntry logEntry = new LogEntry();
		[SerializeField] private ConsoleEntry empty = new ConsoleEntry();
		private static LogCache LogCache => Instance.logCache;

		private static List<ConsoleEntry> AllLogs => Instance.allLogs;

		private static List<ConsoleEntry> FilteredLogs => Instance.filteredLogs;

		public static int Count => FilteredLogs.Count;

		public static string SearchText
		{
			get { return Instance.searchText; }
			set {
				if (Instance.searchText == value)
					return;
				Instance.searchText = value;
				FilterOutLogs();
			}
		}

		public static void Update()
		{
			var count = LogEntries.GetCount();
			if (count == AllLogs.Count)
			{
				UpdateCounts();
				return;
			}
			UpdateLogs();
			FilterOutLogs();
		}

		public static ConsoleEntry GetEntry(int index)
		{
			return FilteredLogs[index];
		}

		public static ConsoleEntry GetEntryByRow(int row)
		{
			foreach (var entry in FilteredLogs)
			{
				if (row == entry.Row)
					return entry;
			}

			return Instance.empty;
		}

		private static void UpdateCounts()
		{
			foreach (var log in AllLogs)
			{
				var count = LogEntries.GetEntryCount(log.Row);
				log.SetCount(count);
			}
		}

		private static void UpdateLogs()
		{
			CacheAndClear();
			var count = LogEntries.StartGettingEntries();
			for (var i = 0; i < count; i++)
			{
				var mode = 0;
				var content = "";
				LogEntries.GetLinesAndModeFromEntryInternal(i, 1, ref mode, ref content);
				var entryCount = LogEntries.GetEntryCount(i);
				LogEntries.GetEntryInternal(i, Instance.logEntry);
				var entry = LogCache.GetOrNew();
				entry.SetValues(i, (LogMode)mode, content, entryCount, Instance.logEntry);
				AllLogs.Add(entry);
			}
			LogEntries.EndGettingEntries();
			FilterOutLogs();
		}

		private static void CacheAndClear()
		{
			LogCache.AddRange(AllLogs);
			AllLogs.Clear();
		}

		public static void FilterOutLogs()
		{
			FilteredLogs.Clear();
			foreach (var log in AllLogs)
			{
				if (log.Content.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) < 0)
					continue;
				foreach (var filter in ConsoleConfiguration.DefaultFilters.Reverse())
				{
					if (!filter.IsMatch(log))
						continue;
					if (!filter.Enabled)
						break;
					FilteredLogs.Add(log);
					break;
				}
			}
		}
	}

}


