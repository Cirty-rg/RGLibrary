using System;
using RGSoft.NativeApi;
using UnityEngine;

namespace RGSoft.Debuggers
{

	/// <summary>
	/// ConsoleLogFilter
	/// </summary>
	[System.Serializable]
	public class LogFilter
	{

		[SerializeField] private string name;
		[SerializeField] private bool enabled;
		[SerializeField] private string pattern;
		[SerializeField] private LogMode mode;
		[SerializeField] private Color backgroundColor;

		public string Name { get { return name; } internal set { name = value; } }
		public bool Enabled { get { return enabled; } internal set { enabled = value; } }
		public string Pattern { get { return pattern; } internal set { pattern = value; } }
		public LogMode Mode { get { return mode; } internal set { mode = value; } }
		public Color BackgroundColor { get { return backgroundColor; } internal set { backgroundColor = value; } }


		/// <summary>
		/// Initialize instance.
		/// </summary>
		public LogFilter()
		{
			name = "NewFilter";
			enabled = true;
			pattern = string.Empty;
			mode = LogMode.None;
			backgroundColor = Color.white;
		}

		/// <summary>
		/// Initialize instance with parameter.
		/// </summary>
		/// <param name="name">filter Name</param>
		/// <param name="mode">filteringlog Mode</param>
		/// <param name="backgroundColor">background Color</param>
		internal LogFilter(string name, LogMode mode, Color backgroundColor)
		{
			this.name = name;
			enabled = true;
			pattern = string.Empty;
			this.mode = mode;
			this.backgroundColor = backgroundColor;
		}

		public bool IsMatch(ConsoleEntry entry)
		{
			return (entry.Mode & mode) != LogMode.None && entry.Content.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0;
		}
	}
}

