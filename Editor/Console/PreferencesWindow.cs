using RGSoft.NativeApi;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace RGSoft.Debuggers
{
	public class PreferencesWindow : EditorWindow
	{

		[MenuItem("RGSoft/Debuggers/Preferences")]
		internal static void Open()
		{
			GetWindow<PreferencesWindow>(true, "Preferences Window");
		}


		private ReorderableList reorderableList;

		private void OnGUI()
		{
			if (reorderableList == null)
			{
				reorderableList = new ReorderableList(ConsoleConfiguration.DefaultFilters, typeof(LogFilter))
				{
					elementHeight = 16 * 4,
					drawElementCallback = OnElementDraw
				};
			}
			reorderableList.DoLayoutList();
		}

		private void OnElementDraw(Rect rect, int index, bool active, bool focused)
		{
			var element = ConsoleConfiguration.DefaultFilters[index];
			var pos = new Rect(rect) { height = 16f };
			EditorGUI.BeginChangeCheck();
			element.Name = EditorGUI.TextField(pos, "Name", element.Name);
			pos.y += 16;
			element.Pattern = EditorGUI.TextField(pos, "Pattern", element.Pattern);
			pos.y += 16;
			element.Mode = (LogMode)EditorGUI.EnumFlagsField(pos, "Pattern", element.Mode);
			pos.y += 16;
			element.BackgroundColor = EditorGUI.ColorField(pos, "BackgroundColor", element.BackgroundColor);
			if (EditorGUI.EndChangeCheck())
			{
				ConsoleConfiguration.Save();
			}
		}
	}
}


