/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System.Reflection;
using RGSoft.Reflections;

namespace RGSoft.NativeApi
{
	/// <summary>
	/// Used to pull log messages from Cpp side to mono window.
	/// </summary>
	public static class LogEntries
	{
		/// <summary>
		/// Console flags.
		/// </summary>
		public static ConsoleFlags ConsoleFlags
		{
			get { return (ConsoleFlags)ConsoleFlagsProperty.GetValue(null); }
			set { ConsoleFlagsProperty.SetValue(null, (int)value); }
		}

		public static void Clear()
		{
			ClearMethod.Invoke(null, null);
		}

		/// <summary>
		/// Get the total number of logs.
		/// </summary>
		/// <returns></returns>
		public static int GetCount()
		{
			return (int)GetCountMethod.Invoke(null, null);
		}

		public static int StartGettingEntries()
		{
			return (int)StartGettingEntriesMethod.Invoke(null, null);
		}

		public static void GetLinesAndModeFromEntryInternal(int row, int numberOfLines, ref int mask, ref string outString)
		{
			GetLineAndModeFromEntryInternalMethodArgs[0] = row;
			GetLineAndModeFromEntryInternalMethodArgs[1] = numberOfLines;
			GetLineAndModeFromEntryInternalMethodArgs[2] = 0;
			GetLineAndModeFromEntryInternalMethodArgs[3] = "";

			GetLinesAndModeFromEntryInternalMethod.Invoke(null, GetLineAndModeFromEntryInternalMethodArgs);
			mask = (int)GetLineAndModeFromEntryInternalMethodArgs[2];
			outString = (string)GetLineAndModeFromEntryInternalMethodArgs[3];
		}

		public static int GetEntryCount(int row)
		{
			SingleArg[0] = row;
			return (int)GetEntryCountMethod.Invoke(null, SingleArg);
		}

		public static bool GetEntryInternal(int row, LogEntry outputEntry)
		{
			GetEntryInternalArgs[0] = row;
			var result = (bool)GetEntryInternalMethod.Invoke(null, GetEntryInternalArgs);
			outputEntry.SetValue(GetEntryInternalArgs[1]);
			return result;
		}

		public static void EndGettingEntries()
		{
			EndGettingEntriesMethod.Invoke(null, null);
		}

		public static void RowGotDoubleClicked(int index)
		{
			SingleArg[0] = index;
			RowGotDoubleClickedMethod.Invoke(null, SingleArg);
		}

		private static readonly PropertyInfo ConsoleFlagsProperty;
		private static readonly MethodInfo ClearMethod;
		private static readonly MethodInfo GetCountMethod;
		private static readonly MethodInfo StartGettingEntriesMethod;
		private static readonly MethodInfo GetLinesAndModeFromEntryInternalMethod;
		private static readonly MethodInfo GetEntryCountMethod;
		private static readonly MethodInfo GetEntryInternalMethod;
		private static readonly MethodInfo EndGettingEntriesMethod;
		private static readonly MethodInfo RowGotDoubleClickedMethod;

		private static readonly object[] GetLineAndModeFromEntryInternalMethodArgs;
		private static readonly object[] GetEntryInternalArgs;
		private static readonly object[] SingleArg;
		static LogEntries()
		{
			const BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			var type = TypeLoader.Load("UnityEditor", "UnityEditor.LogEntries");

			ConsoleFlagsProperty = type.GetProperty("consoleFlags", flags);
			ClearMethod = type.GetMethod("Clear", flags);
			GetCountMethod = type.GetMethod("GetCount", flags);
			StartGettingEntriesMethod = type.GetMethod("StartGettingEntries", flags);
			GetLinesAndModeFromEntryInternalMethod = type.GetMethod("GetLinesAndModeFromEntryInternal", flags);
			GetEntryCountMethod = type.GetMethod("GetEntryCount", flags);
			GetEntryInternalMethod = type.GetMethod("GetEntryInternal", flags);
			EndGettingEntriesMethod = type.GetMethod("EndGettingEntries", flags);
			RowGotDoubleClickedMethod = type.GetMethod("RowGotDoubleClicked", flags);

			GetLineAndModeFromEntryInternalMethodArgs = new object[4];
			GetEntryInternalArgs = new object[2];
			GetEntryInternalArgs[1] = LogEntry.CreateNativeObject();
			SingleArg = new object[1];
		}
	}
}