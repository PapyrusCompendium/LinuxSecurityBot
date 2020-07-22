using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinuxSecurityBot
{
	public static class Log
	{
		private enum LogType
		{
			Info,
			Warning,
			Error,
			Debug,
		}
		private static string _logName { get => typeof(Log).Namespace; }
		private static string _timeStamp { get => $"[{DateTime.Now.ToLongTimeString()}]"; }
		private static string FormatOutput(string logText) => $"[{_logName} @ {_timeStamp}] {logText}";
		public static void Info(string logText)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			WriteLog(logText);
		}

		public static void Warning(string logText)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			WriteLog(logText);
		}

		public static void Error(string logText)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			WriteLog(logText);
		}

		private static void WriteLog(string logText)
		{
			Console.WriteLine(FormatOutput(logText));
			Console.ResetColor();
		}
	}
}
