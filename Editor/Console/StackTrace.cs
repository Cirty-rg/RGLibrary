/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace RGSoft.Debuggers
{

	[System.Serializable]
	public class StackTrace
	{
		private static readonly Regex ScriptRegex = new Regex(@"\(at.*\.cs:\d*\)");

		[SerializeField]
		private string[] contents;
		[SerializeField]
		private string[] files;
		[SerializeField]
		private int[] lines;

		public string[] Contents => contents;

		public StackTrace()
		{
			contents = new string[0];
			files = new string[0];
			lines = new int[0];
		}

		public StackTrace(string message)
		{
			var split = message.Replace("\r\n", "\n").Split('\n');
			contents = new string[split.Length];
			files = new string[split.Length];
			lines = new int[split.Length];
			for (var i = 0; i < split.Length; i++)
			{
				contents[i] = split[i];
				var output = ScriptRegex.Match(contents[i]);
				if (!output.Success)
				{
					files[i] = "";
					lines[i] = -1;
					continue;
				}

				var length = output.Length;
				var colonIndex = output.Value.IndexOf(':');
				var fileStart = 4;
				var fileEnd = colonIndex - fileStart;
				files[i] = output.Value.Substring(fileStart, fileEnd);
				var lineStart = colonIndex + 1;
				var lineEnd = length - lineStart - 1;
				var line = output.Value.Substring(lineStart, lineEnd);
				lines[i] = int.Parse(line);
			}
		}

		public void OpenFile(int index)
		{
			var file = files[index];
			var line = lines[index];
			if (string.IsNullOrEmpty(file) || line < 0)
				return;
			var scriptFile = AssetDatabase.LoadAssetAtPath<Object>(file);
			AssetDatabase.OpenAsset(scriptFile, line);
			Resources.UnloadAsset(scriptFile);
			//InternalEditorUtility.OpenFileAtLineExternal(file, line);
		}
	}

}

