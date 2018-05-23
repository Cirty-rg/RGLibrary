/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System.IO;
using UnityEditorInternal;
using UnityEngine;

namespace RGSoft.NativeApi
{
	/// <summary>
	/// Reimplementing <see cref="UnityEditor.ScriptableSingleton{T}"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
	{

		/// <summary>
		/// Constructor
		/// </summary>
		protected ScriptableSingleton()
		{
			if (instance != null)
			{
				Debug.LogError("ScriptableSingleton already exists. Did you query the singleton in a constructor?");
			}
			else
			{
				instance = (this as T);
			}
		}

		private static T instance;

		/// <summary>
		/// Instance
		/// </summary>
		public static T Instance
		{
			get {
				if (instance == null)
				{
					CreateAndLoad();
				}
				return instance;
			}
		}

		private static void CreateAndLoad()
		{
			var filePath = GetFilePath();
			if (!string.IsNullOrEmpty(filePath))
			{
				InternalEditorUtility.LoadSerializedFileAndForget(filePath);
			}

			if (instance != null) return;
			var t = CreateInstance<T>();
			t.hideFlags = HideFlags.HideAndDontSave;
		}

		/// <summary>
		/// Save the instance.
		/// <see cref="FilePathAttribute"/> is required.
		/// </summary>
		/// <param name="saveAsText"></param>
		protected virtual void Save(bool saveAsText)
		{
			if (instance == null)
			{
				Debug.Log("Can not save ScriptableSingleton: no instance!");
			}
			else
			{
				var filePath = GetFilePath();
				if (string.IsNullOrEmpty(filePath)) return;
				var directoryName = Path.GetDirectoryName(filePath);

				if (!Directory.Exists(directoryName))
				{
					if (directoryName != null) Directory.CreateDirectory(directoryName);
				}
				InternalEditorUtility.SaveToSerializedFileAndForget(new Object[] { instance }, filePath, saveAsText);
			}
		}

		private static string GetFilePath()
		{
			var typeFromHandle = typeof(T);
			var customAttributes = typeFromHandle.GetCustomAttributes(true);
			foreach (var obj in customAttributes)
			{
				if (!(obj is FilePathAttribute)) continue;
				var filePathAttribute = obj as FilePathAttribute;
				return filePathAttribute.FilePath;
			}
			return null;
		}
	}
}