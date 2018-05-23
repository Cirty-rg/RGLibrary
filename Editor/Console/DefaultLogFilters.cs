using RGSoft.NativeApi;
using UnityEngine;

namespace RGSoft.Debuggers
{
	internal static class DefaultLogFilters
	{
		public static readonly LogFilter Log = new LogFilter("Log", LogMode.LogAll, Color.white);
		public static readonly LogFilter Warning = new LogFilter("Warning", LogMode.WarningAll, new Color(1f, 1f, 0.5f));
		public static readonly LogFilter Error = new LogFilter("Error", LogMode.ErrorAll, new Color(1f, 0.51f, 0.49f));
		public static readonly LogFilter Exception = new LogFilter("Exception", LogMode.ExceptionAll, new Color(1f, 0.5f, 1f));
		public static readonly LogFilter Assert = new LogFilter("Assert", LogMode.AssertAll, new Color(1f, 0.72f, 0f));
		public static readonly LogFilter Other = new LogFilter("Other", LogMode.All, Color.white);
	}
}


