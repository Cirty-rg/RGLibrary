/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

// ReSharper disable InconsistentNaming

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RGSoft.IMGUI
{

	/// <summary>
	/// Add utilities for GUI drawing.
	/// </summary>
	public class AdditionalGUIUtility
	{

		private static readonly GUIContent textContent = new GUIContent();
		private static readonly GUIContent imageContent = new GUIContent();
		private static readonly GUIContent textImageContent = new GUIContent();

		/// <summary>
		/// Return cached GUIContent.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static GUIContent TempContent(string text)
		{
			textContent.text = text;
			return textContent;
		}

		/// <summary>
		/// Return cached GUIContent.
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public static GUIContent TempContent(Texture image)
		{
			imageContent.image = image;
			return imageContent;
		}

		/// <summary>
		/// Return cached GUIContent.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="image"></param>
		/// <returns></returns>
		public static GUIContent TempContent(string text, Texture image)
		{
			textImageContent.text = text;
			textImageContent.image = image;
			return textImageContent;
		}
	}
}


