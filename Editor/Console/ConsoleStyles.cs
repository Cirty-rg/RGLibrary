/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using UnityEngine;
using System.Collections;
using UnityEditor;

namespace RGSoft.Debuggers
{

	/// <summary>
	/// Console styles.
	/// </summary>
	[System.Serializable]
	public class ConsoleStyles
	{

		private bool initialized;

		private GUIStyle toolbar;
		private GUIStyle toolbarButton;
		private GUIStyle toolbarDropDown;
		private GUIStyle toolbarSearchField;
		private GUIStyle toolbarSearchCancel;
		private GUIStyle toolbarSearchCancelEmpty;
		private GUIStyle logViewBackGroundEven;
		private GUIStyle logViewBackGroundOdd;
		private GUIStyle log;
		private GUIStyle logCount;
		private GUIStyle message;
		/// <summary>
		/// 
		/// </summary>
		public GUIStyle Toolbar => toolbar;

		public GUIStyle ToolbarButton => toolbarButton;

		public GUIStyle ToolbarDropDown => toolbarDropDown;

		public GUIStyle ToolbarSearchField => toolbarSearchField;

		public GUIStyle ToolbarSearchCancel => toolbarSearchCancel;

		public GUIStyle ToolbarSearchCancelEmpty => toolbarSearchCancelEmpty;

		public GUIStyle Log => log;

		public GUIStyle LogCount => logCount;

		public GUIStyle Message => message;

		public void Initialize()
		{
			if (initialized)
				return;
			toolbar = new GUIStyle(EditorStyles.toolbar);
			toolbarButton = new GUIStyle(EditorStyles.toolbarButton);
			toolbarDropDown = new GUIStyle(EditorStyles.toolbarDropDown);
			toolbarSearchField = new GUIStyle("ToolbarSeachTextField");
			toolbarSearchCancel = new GUIStyle("ToolbarSeachCancelButton");
			toolbarSearchCancelEmpty = new GUIStyle("ToolbarSeachCancelButtonEmpty");
			log = new GUIStyle(GUI.skin.label) { wordWrap = true, fixedHeight = 0, stretchHeight = false };
			logViewBackGroundEven = new GUIStyle("CN EntryBackodd");
			logViewBackGroundOdd = new GUIStyle("CN EntryBackEven");
			logCount = new GUIStyle(GUI.skin.label)
			{
				alignment = TextAnchor.MiddleRight,
				fontStyle = FontStyle.Bold,
				fixedWidth = 32,
			};
			message = new GUIStyle("CN Message");
			initialized = true;
		}

		public GUIStyle GetLogBackgroundStyle(int row)
		{
			return row % 2 == 0 ? logViewBackGroundEven : logViewBackGroundOdd;
		}
	}


}

