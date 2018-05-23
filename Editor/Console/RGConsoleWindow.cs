/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System;
using RGSoft.IMGUI;
using RGSoft.NativeApi;
using UnityEditor;
using UnityEngine;

namespace RGSoft.Debuggers
{

	/// <summary>
	/// Enhanced <see cref="ConsoleWindow"/>
	/// </summary>
	public class RgConsoleWindow : EditorWindow, IHasCustomMenu
	{

		[MenuItem("RGSoft/Tools/Console")]
		private static void Open()
		{
			GetWindow<RgConsoleWindow>("RGConsole");
		}

		private readonly ConsoleStyles styles = new ConsoleStyles();
		private Vector2 logViewScroll;
		private Vector2 messageViewScroll;

		private int selectRow = -1;
		private int selectMessage = -1;

		private SplitterState splitterState = new SplitterState(new[] { 70f, 30f }, new[] { 32, 32 }, null);
		private void OnGUI()
		{
			styles.Initialize();
			DrawToolbar();
			SplitterGUILayout.BeginVerticalSplit(splitterState);
			DrawLogList();
			DrawMessagesView();
			SplitterGUILayout.EndVerticalSplit();
		}

		#region Toolbar

		private void DrawToolbar()
		{
			using (new EditorGUILayout.HorizontalScope(styles.Toolbar))
			{
				AdditionalGUILayout.Button("Clear", styles.ToolbarButton, ClearLogs);
				EditorGUILayout.Space();
				DrawConsoleFlags();
				EditorGUILayout.Space();
				DrawSearchField();
				GUILayout.FlexibleSpace();
				DrawFilters();
			}
		}

		private void ClearLogs()
		{
			LogEntries.Clear();
			selectRow = -1;
		}

		private void DrawConsoleFlags()
		{
			var flags = LogEntries.ConsoleFlags;
			EditorGUI.BeginChangeCheck();
			ToggleFlags("Collapse", ConsoleFlags.Collapse, ref flags);
			ToggleFlags("Clear on Play", ConsoleFlags.ClearOnPlay, ref flags);
			ToggleFlags("Error Pause", ConsoleFlags.ErrorPause, ref flags);
			if (EditorGUI.EndChangeCheck())
				LogEntries.ConsoleFlags = flags;
		}

		private void ToggleFlags(string label, ConsoleFlags target, ref ConsoleFlags flags)
		{
			var ret = (target & flags) != ConsoleFlags.None;
			EditorGUI.BeginChangeCheck();
			ret = GUILayout.Toggle(ret, label, EditorStyles.toolbarButton);
			if (!EditorGUI.EndChangeCheck())
				return;
			if (ret)
				flags |= target;
			else
				flags &= ~target;
		}

		private void DrawSearchField()
		{
			ConsoleEntries.SearchText = EditorGUILayout.TextField(GUIContent.none, ConsoleEntries.SearchText, styles.ToolbarSearchField, GUILayout.MinWidth(50), GUILayout.MaxWidth(120));
			var style = string.IsNullOrEmpty(ConsoleEntries.SearchText) ? styles.ToolbarSearchCancelEmpty : styles.ToolbarSearchCancel;
			AdditionalGUILayout.Button(GUIContent.none, style, ClearSearchText);
		}

		private void ClearSearchText()
		{
			ConsoleEntries.SearchText = string.Empty;
			GUIUtility.keyboardControl = 0;
		}

		private void DrawFilters()
		{
			var backgroundColor = GUI.backgroundColor;
			EditorGUI.BeginChangeCheck();
			foreach (var filter in ConsoleConfiguration.DefaultFilters)
			{
				GUI.backgroundColor = filter.BackgroundColor;
				filter.Enabled = GUILayout.Toggle(filter.Enabled, filter.Name, styles.ToolbarButton);
			}
			if (EditorGUI.EndChangeCheck())
			{
				ConsoleEntries.FilterOutLogs();
			}
			GUI.backgroundColor = backgroundColor;
		}

		#endregion

		#region LogList

		private void DrawLogList()
		{
			logViewScroll = EditorGUILayout.BeginScrollView(logViewScroll, "CN Box");
			ConsoleEntries.Update();
			var count = ConsoleEntries.Count;
			var bgColor = GUI.backgroundColor;
			for (var i = 0; i < count; i++)
			{
				DrawLog(i);
			}
			GUI.backgroundColor = bgColor;
			EditorGUILayout.EndScrollView();
		}

		private void DrawLog(int row)
		{
			var entry = ConsoleEntries.GetEntry(row);
			var rect = EditorGUILayout.BeginHorizontal();
			FixedRect(ref rect);
			DrawBackground(row, entry, rect);
			SelectLog(entry, rect);
			GUILayout.Label(entry.Content, styles.Log);
			GUILayout.FlexibleSpace();
			var c = entry.Count > 9999 ? "9999+" : entry.Count.ToString();
			GUILayout.Label(c, styles.LogCount);
			EditorGUILayout.EndHorizontal();
		}

		private void FixedRect(ref Rect rect)
		{
			rect.x -= 3;
			rect.width += 7;
			rect.y -= 1;
			rect.height += 2;
		}

		private void DrawBackground(int row, ConsoleEntry entry, Rect rect)
		{
			if (Event.current.type != EventType.Repaint) return;
			var bg = styles.GetLogBackgroundStyle(row);
			GUI.backgroundColor = entry.Row == selectRow ? ConsoleConfiguration.SelectEntryColor : ConsoleConfiguration.GetLogColor(entry);
			bg.Draw(rect, GUIContent.none, false, false, false, false);
		}

		private void SelectLog(ConsoleEntry entry, Rect rect)
		{
			var current = Event.current;
			if (current.type != EventType.MouseDown) return;
			if (!rect.Contains(current.mousePosition))
				return;
			if (current.clickCount == 1)
			{
				selectRow = entry.Row;
				selectMessage = -1;
			}
			else if (current.clickCount == 2)
			{
				entry.OpenFile();
			}
			current.Use();
		}
		#endregion

		#region MessageView

		private void DrawMessagesView()
		{
			messageViewScroll = EditorGUILayout.BeginScrollView(messageViewScroll, "CN Box");
			var entry = ConsoleEntries.GetEntryByRow(selectRow);
			var bgColor = GUI.backgroundColor;
			for (var i = 0; i < entry.Messages.Length; i++)
			{
				var rect = EditorGUILayout.BeginHorizontal();
				FixedRect(ref rect);
				DrawMessageBackground(i, rect);
				SelectMessage(entry, i, rect);
				GUILayout.Label(entry.Messages[i], styles.Log);
				EditorGUILayout.EndHorizontal();
			}
			GUI.backgroundColor = bgColor;
			EditorGUILayout.EndScrollView();
		}

		private void DrawMessageBackground(int i, Rect rect)
		{
			if (Event.current.type != EventType.Repaint) return;
			var bg = styles.GetLogBackgroundStyle(0);
			GUI.backgroundColor = i == selectMessage ? ConsoleConfiguration.SelectEntryColor : Color.white;
			bg.Draw(rect, GUIContent.none, false, false, false, false);
		}

		private void SelectMessage(ConsoleEntry entry, int index, Rect rect)
		{
			var current = Event.current;
			if (current.type != EventType.MouseDown) return;
			if (!rect.Contains(current.mousePosition))
				return;
			if (current.clickCount == 1)
			{
				selectMessage = index;
			}
			else if (current.clickCount == 2)
			{
				entry.OpenStackTraceFile(index);
			}
			current.Use();
		}
		#endregion

		public void AddItemsToMenu(GenericMenu menu)
		{
			menu.AddItem(new GUIContent("Preferences"), false, OpenConfigurationWindow);
			menu.AddItem(new GUIContent("TestLog"), false, DebugLog);
		}

		private void DebugLog()
		{
			Debug.Log("[Log] Log");
			Debug.Log("[Log] LoooooooooooooooooooooooooooooooooooooooooooongLog");
			Debug.LogWarning("[Warning] WarningLog");
			Debug.LogError("[Error] ErrorLog");
			Debug.LogException(new Exception("[Exception] ExceptionLog"));
			Debug.LogAssertion("[Assert] AssertLog");
			Debug.Assert(false, "[Assert] Assert");
		}

		private void OpenConfigurationWindow()
		{
			PreferencesWindow.Open();
		}
	}
}

