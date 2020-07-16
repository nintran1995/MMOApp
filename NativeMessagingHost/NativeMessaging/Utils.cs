using System;
using System.IO;
using System.Reflection;

namespace NativeMessaging
{
	internal static class Utils
	{
		public const string APP_NAME = "dataDyneChromeServerHost.exe";
		public static string MessageLogLocation
		{
			get
			{
				return Path.Combine(Utils.AssemblyLoadDirectory(), "native-messaging.log");
			}
		}

		public static string AssemblyExecuteablePath()
		{
			//string codeBase = Assembly.GetEntryAssembly().CodeBase;
			string codeBase = $"{AppDomain.CurrentDomain.BaseDirectory}{APP_NAME}";
			return Uri.UnescapeDataString((new UriBuilder(codeBase)).Path);
		}

		public static string AssemblyLoadDirectory()
		{
			string codeBase = Assembly.GetEntryAssembly().CodeBase;
			string path = Uri.UnescapeDataString((new UriBuilder(codeBase)).Path);
			return Path.GetDirectoryName(path);
		}

		public static void LogMessage(string msg)
		{
			Utils.LogMessage(new string[] { msg });
		}

		public static void LogMessage(string[] msgs)
		{
			try
			{
				File.AppendAllLines(Utils.MessageLogLocation, msgs);
			}
			catch (IOException oException)
			{
				Console.WriteLine("Could Not Log To File");
			}
		}
	}
}