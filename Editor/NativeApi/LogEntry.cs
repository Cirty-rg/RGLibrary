/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;
using RGSoft.Reflections;
using UnityEngine;

namespace RGSoft.NativeApi
{
	/// <summary>
	/// Log messages.
	/// </summary>
	[Serializable]
	public class LogEntry
	{
		[SerializeField]
		private string condition;
		[SerializeField]
		private int errorNum;
		[SerializeField]
		private string file;
		[SerializeField]
		private int line;
		[SerializeField]
		private int mode;
		[SerializeField]
		private int instanceId;
		[SerializeField]
		private int identifier;
		[SerializeField]
		private int isWorldPlaying;

		public string Condition
		{
			get { return condition; }
			set { condition = value; }
		}

		public int ErrorNum
		{
			get { return errorNum; }
			set { errorNum = value; }
		}

		public string File
		{
			get { return file; }
			set { file = value; }
		}

		public int Line
		{
			get { return line; }
			set { line = value; }
		}

		public int Mode
		{
			get { return mode; }
			set { mode = value; }
		}

		public int InstanceId
		{
			get { return instanceId; }
			set { instanceId = value; }
		}

		public int Identifier
		{
			get { return identifier; }
			set { identifier = value; }
		}

		public int IsWorldPlaying
		{
			get { return isWorldPlaying; }
			set { isWorldPlaying = value; }
		}

		/// <summary>
		/// NativeObject to C#Object
		/// </summary>
		/// <param name="nativeLogEntry"></param>
		public void SetValue(object nativeLogEntry)
		{
			condition = (string)ConditionField.GetValue(nativeLogEntry);
			errorNum = (int)ErrorNumField.GetValue(nativeLogEntry);
			file = (string)FileField.GetValue(nativeLogEntry);
			line = (int)LineField.GetValue(nativeLogEntry);
			mode = (int)ModeField.GetValue(nativeLogEntry);
			instanceId = (int)InstanceIdField.GetValue(nativeLogEntry);
			identifier = (int)IdentifierField.GetValue(nativeLogEntry);
			isWorldPlaying = (int)IsWorldPlayingField.GetValue(nativeLogEntry);
		}



		private static readonly Type LogEntryType;
		private static readonly FieldInfo ConditionField;
		private static readonly FieldInfo ErrorNumField;
		private static readonly FieldInfo FileField;
		private static readonly FieldInfo LineField;
		private static readonly FieldInfo ModeField;
		private static readonly FieldInfo InstanceIdField;
		private static readonly FieldInfo IdentifierField;
		private static readonly FieldInfo IsWorldPlayingField;

		public static object CreateNativeObject()
		{
			return Activator.CreateInstance(LogEntryType);
		}

		static LogEntry()
		{
			LogEntryType = TypeLoader.Load("UnityEditor", "UnityEditor.LogEntry");
			ConditionField = LogEntryType.GetField("condition");
			ErrorNumField = LogEntryType.GetField("errorNum");
			FileField = LogEntryType.GetField("file");
			LineField = LogEntryType.GetField("line");
			ModeField = LogEntryType.GetField("mode");
			InstanceIdField = LogEntryType.GetField("instanceID");
			IdentifierField = LogEntryType.GetField("identifier");
			IsWorldPlayingField = LogEntryType.GetField("isWorldPlaying");
		}

	}
}

