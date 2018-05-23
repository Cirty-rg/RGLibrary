/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using RGSoft.NativeApi;
using UnityEngine;

namespace RGSoft.Debuggers
{
	[System.Serializable]
	public class ConsoleEntry
	{
		[SerializeField]
		private int row;
		[SerializeField]
		private LogMode mode;
		[SerializeField]
		private int count;
		[SerializeField]
		private string content;
		[SerializeField]
		private int instanceId;
		[SerializeField]
		private StackTrace stackTrace;

		public int Row => row;

		public LogMode Mode => mode;

		public int Count => count;

		public string Content => content;

		public int InstanceId => instanceId;

		public string[] Messages => stackTrace.Contents;

		public ConsoleEntry()
		{
			Initialize();
		}

		private void Initialize()
		{
			row = -1;
			mode = LogMode.None;
			count = 0;
			content = "";
			instanceId = -1;
			stackTrace = new StackTrace();
		}

		public void SetValues(int logRow, LogMode logMode, string logContent, int logCount, LogEntry logEntry)
		{
			Initialize();
			row = logRow;
			mode = logMode;
			content = logContent;
			count = logCount;
			stackTrace = new StackTrace(logEntry.Condition);
		}

		public void SetCount(int value)
		{
			count = value;
		}

		public void OpenFile()
		{
			LogEntries.RowGotDoubleClicked(row);
		}

		public void OpenStackTraceFile(int index)
		{
			stackTrace.OpenFile(index);
		}
	}
}