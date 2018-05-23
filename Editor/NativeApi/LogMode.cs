// ReSharper disable IdentifierTypo

namespace RGSoft.NativeApi
{

	/// <summary>
	/// ConsoleLogMode
	/// </summary>
	[System.Flags]
	public enum LogMode
	{
		None = 0,

		//Log
		Log = 1 << 2,
		ScriptingLog = 1 << 10,

		LogAll = Log | ScriptingLog,

		//Warning
		AssetImportWarning = 1 << 7,
		ScriptingWarning = 1 << 9,
		ScriptCompileWarning = 1 << 12,

		WarningAll = AssetImportWarning | ScriptingWarning | ScriptCompileWarning,

		//Error
		Error = 1 << 0,
		AssetImportError = 1 << 6,
		ScriptingError = 1 << 8,
		ScriptCompileError = 1 << 11,
		StickyError = 1 << 13,
		GraphCompileError = 1 << 20,

		ErrorAll = Error | AssetImportError | ScriptingError | ScriptCompileError |
				   StickyError | GraphCompileError,

		//Exception
		Fatal = 1 << 4,
		ScriptingException = 1 << 17,

		ExceptionAll = Fatal | ScriptingException,

		//Assert
		Assert = 1 << 1,
		ScriptingAssertion = 1 << 21,

		AssertAll = Assert | ScriptingAssertion,

		//Other
		DontPreprocessCondition = 1 << 5,
		MayIgnoreLineNumber = 1 << 14,
		ReportBug = 1 << 15,
		DisplayPreviousErrorInStatusBar = 1 << 16,
		DontExtractStacktrace = 1 << 18,
		ShouldClearOnPlay = 1 << 19,

		OtherAll = DontPreprocessCondition | MayIgnoreLineNumber | ReportBug | DisplayPreviousErrorInStatusBar |
				   DontExtractStacktrace | ShouldClearOnPlay,

		All = LogAll | WarningAll | ErrorAll | ExceptionAll | AssertAll | OtherAll,
	}
}


