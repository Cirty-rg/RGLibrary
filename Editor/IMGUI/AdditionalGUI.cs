// ReSharper disable InconsistentNaming

using RGSoft.Linq;
using System;
using UnityEngine;

namespace RGSoft.IMGUI
{

	/// <summary>
	/// Add GUI drawing function of manual layout.
	/// </summary>
	public class AdditionalGUI
	{
		private static readonly GUIContent temp = new GUIContent();

		/// <summary>
		/// Make a single press button.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the GUI element.</param>
		/// <param name="text">Text to display on the button.</param>
		/// <param name="action">Executed when button is pressed.</param>
		public static void Button(Rect position, string text, Action action)
		{
			Button(position, AdditionalGUIUtility.TempContent(text), GUI.skin.button, action);
		}

		/// <summary>
		/// Make a single press button.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the GUI element.</param>
		/// <param name="image">Texture to display on the button.</param>
		/// <param name="action">Executed when button is pressed.</param>
		public static void Button(Rect position, Texture image, Action action)
		{
			Button(position, AdditionalGUIUtility.TempContent(image), GUI.skin.button, action);
		}

		/// <summary>
		/// Make a single press button.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the GUI element.</param>
		/// <param name="content">Text, image and tooltips for this button.</param>
		/// <param name="action">Executed when button is pressed.</param>
		public static void Button(Rect position, GUIContent content, Action action)
		{
			Button(position, content, GUI.skin.button, action);
		}

		/// <summary>
		/// Make a single press button.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the GUI element.</param>
		/// <param name="text">Text to display on the button.</param>
		/// <param name="style">The style to use. If left out, the button style from the current GUISkin is used.</param>
		/// <param name="action">Executed when button is pressed.</param>
		public static void Button(Rect position, string text, GUIStyle style, Action action)
		{
			var content = AdditionalGUIUtility.TempContent(text);
			Button(position, content, style, action);
		}

		/// <summary>
		/// Make a single press button.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the GUI element.</param>
		/// <param name="image">Texture to display on the button.</param>
		/// <param name="style">The style to use. If left out, the button style from the current GUISkin is used.</param>
		/// <param name="action">Executed when button is pressed.</param>
		public static void Button(Rect position, Texture image, GUIStyle style, Action action)
		{
			var content = AdditionalGUIUtility.TempContent(image);
			Button(position, content, style, action);
		}

		/// <summary>
		/// Make a single press button.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the GUI element.</param>
		/// <param name="content">Text, image and tooltips for this button.</param>
		/// <param name="style">The style to use. If left out, the button style from the current GUISkin is used.</param>
		/// <param name="action">Executed when button is pressed.</param>
		public static void Button(Rect position, GUIContent content, GUIStyle style, Action action)
		{
			if (GUI.Button(position, content, style))
			{
				action.SafetyInvoke();
			}
		}

		public static GUIContent Temp(string text)
		{
			temp.image = null;
			temp.tooltip = String.Empty;
			temp.text = text;
			return temp;
		}
	}
}


