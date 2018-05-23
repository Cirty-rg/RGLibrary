namespace RGSoft.NativeApi
{
	/// <summary>
	/// Flag used in the console.
	/// </summary>
	[System.Flags]
	public enum ConsoleFlags
	{
		None = 0,
		Collapse = 1 << 0,
		ClearOnPlay = 1 << 1,
		ErrorPause = 1 << 2,
		Verbose = 1 << 3,
		StopForAssert = 1 << 4,
		StopForError = 1 << 5,
		AutoScroll = 1 << 6,
		LogLevelLog = 1 << 7,
		LogLevelWarning = 1 << 8,
		LogLevelError = 1 << 9,
		ShowTimestamp = 11 << 10
	}

}
