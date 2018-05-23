using System;
using UnityEngine;
using UnityEditorInternal;

namespace RGSoft.NativeApi
{
	[AttributeUsage(AttributeTargets.Class)]
	public class FilePathAttribute : Attribute
	{
		public FilePathAttribute(string relativePath, Location location)
		{
			if (string.IsNullOrEmpty(relativePath))
			{
				Debug.LogError("Invalid relative path! (its null or empty)");
			}
			else
			{
				if (relativePath[0] == '/')
				{
					relativePath = relativePath.Substring(1);
				}

				if (location == Location.PreferencesFolder)
				{
					FilePath = InternalEditorUtility.unityPreferencesFolder + "/" + relativePath;
				}
				else
				{
					FilePath = relativePath;
				}
			}
		}

		public string FilePath { get; set; }

		public enum Location
		{
			PreferencesFolder,
			ProjectFolder
		}
	}
}