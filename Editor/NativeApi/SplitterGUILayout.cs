/*
Copyright (c) [2018] [cirty]

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using RGSoft.Reflections;
using UnityEngine;

namespace RGSoft.NativeApi
{

	public class SplitterGUILayout
	{
		public static void BeginSplit(SplitterState state, GUIStyle style, bool vertical, params GUILayoutOption[] options)
		{
			var native = state.State;
			BeginSplitMethod.Invoke(null, new[] { native, style, vertical, options });
			state.Apply();
		}

		public static void BeginHorizontalSplit(SplitterState state, params GUILayoutOption[] options)
		{
			BeginSplit(state, GUIStyle.none, false, options);
		}

		public static void BeginVerticalSplit(SplitterState state, params GUILayoutOption[] options)
		{
			BeginSplit(state, GUIStyle.none, true, options);
		}

		public static void BeginHorizontalSplit(SplitterState state, GUIStyle style, params GUILayoutOption[] options)
		{
			BeginSplit(state, style, false, options);
		}

		public static void BeginVerticalSplit(SplitterState state, GUIStyle style, params GUILayoutOption[] options)
		{
			BeginSplit(state, style, true, options);
		}

		public static void EndVerticalSplit()
		{
			EndVerticalSplitMethod.Invoke(null, null);
		}

		public static void EndHorizontalSplit()
		{
			EndHorizontalSplitMethod.Invoke(null, null);
		}

		private static readonly MethodInfo BeginSplitMethod;
		private static readonly MethodInfo EndVerticalSplitMethod;
		private static readonly MethodInfo EndHorizontalSplitMethod;

		static SplitterGUILayout()
		{
			var type = TypeLoader.Load("UnityEditor", "UnityEditor.SplitterGUILayout");
			const BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			BeginSplitMethod = type.GetMethod("BeginSplit", flags);
			EndVerticalSplitMethod = type.GetMethod("EndVerticalSplit", flags);
			EndHorizontalSplitMethod = type.GetMethod("EndHorizontalSplit", flags);
		}
	}
}


