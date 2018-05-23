// ReSharper disable InconsistentNaming

using System;
using UnityEngine;

namespace RGSoft.IMGUI
{

    /// <summary>
    /// Add GUI drawing function of automatic layout.
    /// </summary>
    public class AdditionalGUILayout
    {

        /// <summary>
        /// Make a single press button.
        /// </summary>
        /// <param name="image">Texture to display on the button.</param>
        /// <param name="action">Executed when button is pressed.</param>
        /// <param name="options">An optional list of layout options that specify extra layouting properties.</param>
        public static void Button(Texture image, Action action, params GUILayoutOption[] options)
        {
            var content = AdditionalGUIUtility.TempContent(image);
            Button(content, GUI.skin.button, action, options);
        }

        /// <summary>
        /// Make a single press button.
        /// </summary>
        /// <param name="text">Text to display on the button.</param>
        /// <param name="action">Executed when button is pressed.</param>
        /// <param name="options">An optional list of layout options that specify extra layouting properties.</param>
        public static void Button(string text, Action action, params GUILayoutOption[] options)
        {
            var content = AdditionalGUIUtility.TempContent(text);
            Button(content, GUI.skin.button, action, options);
        }

        /// <summary>
        /// Make a single press button.
        /// </summary>
        /// <param name="content">Text, image and tooltips for this button.</param>
        /// <param name="action">Executed when button is pressed.</param>
        /// <param name="options">An optional list of layout options that specify extra layouting properties.</param>
        public static void Button(GUIContent content, Action action, params GUILayoutOption[] options)
        {
            Button(content, GUI.skin.button, action, options);
        }

        /// <summary>
        /// Make a single press button.
        /// </summary>
        /// <param name="image">Texture to display on the button.</param>
        /// <param name="style">The style to use. If left out, the button style from the current GUISkin is used.</param>
        /// <param name="action">Executed when button is pressed.</param>
        /// <param name="options">An optional list of layout options that specify extra layouting properties.</param>
        public static void Button(Texture image,GUIStyle style, Action action, params GUILayoutOption[] options)
        {
            var content = AdditionalGUIUtility.TempContent(image);
            Button(content, style, action, options);
        }

        /// <summary>
        /// Make a single press button.
        /// </summary>
        /// <param name="text">Text to display on the button.</param>
        /// <param name="style">The style to use. If left out, the button style from the current GUISkin is used.</param>
        /// <param name="action">Executed when button is pressed.</param>
        /// <param name="options">An optional list of layout options that specify extra layouting properties.</param>
        public static void Button(string text, GUIStyle style, Action action, params GUILayoutOption[] options)
        {
            var content = AdditionalGUIUtility.TempContent(text);
            Button(content,style,action, options);
        }

        /// <summary>
        /// Make a single press button.
        /// </summary>
        /// <param name="content">Text, image and tooltips for this button.</param>
        /// <param name="style">The style to use. If left out, the button style from the current GUISkin is used.</param>
        /// <param name="action">Executed when button is pressed.</param>
        /// <param name="options">An optional list of layout options that specify extra layouting properties.</param>
        public static void Button(GUIContent content, GUIStyle style, Action action, params GUILayoutOption[] options)
        {
            var position = GUILayoutUtility.GetRect(content, style, options);
            AdditionalGUI.Button(position,content,style,action);
        }
    }

}


