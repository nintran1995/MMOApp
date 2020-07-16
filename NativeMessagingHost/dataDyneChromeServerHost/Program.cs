using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Threading;
using Clifton.Core.Pipes;
using CommandModel;
using NativeMessaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZChangerMMO.BaseHost
{
	class Program
	{
		private static ChromeServerHost Host;

		private static bool ChromeConnectionStable;

		private static ClientPipe clientPipe;

		private static string[] AllowedOrigins;

		private static string Description;

		public static string AssemblyExecuteablePath
		{
			get
			{
				string codeBase = Assembly.GetEntryAssembly().CodeBase;
				return Uri.UnescapeDataString((new UriBuilder(codeBase)).Path);
			}
		}

		public static string AssemblyLoadDirectory
		{
			get
			{
				string codeBase = Assembly.GetEntryAssembly().CodeBase;
				string path = Uri.UnescapeDataString((new UriBuilder(codeBase)).Path);
				return Path.GetDirectoryName(path);
			}
		}

		static Program()
		{
			Program.ChromeConnectionStable = true;
			Program.AllowedOrigins = new string[] { "chrome-extension://khjlaccalgjpbimfpoifhncdempmnddn/" };
			Program.Description = "Description Goes Here";
		}

		public Program()
		{
		}

		public static void AppMessageRecieveHandler(object sender, PipeEventArgs arguments)
		{
			string messageFromServer = arguments.String;
			if (!string.IsNullOrEmpty(messageFromServer))
			{
				Host.SendMessage(JsonConvert.DeserializeObject<JObject>(messageFromServer));
			}
		}

		private static void Host_LostConnectionToChrome(object sender, EventArgs e)
		{
			BaseCommand disconnect = new BaseCommand(ZC_Command.DISCONNECT);
			var message = JsonConvert.SerializeObject(disconnect);
			//Host.SendMessage(JsonConvert.DeserializeObject<JObject>(message));
			if (clientPipe != null)
			{
				clientPipe.WriteString(message);
			}
			ChromeConnectionStable = false;
		}

		public static void HostMessageRevieveHandler(object sender, MessageReceievedArgs arguments)
		{
			if (arguments.Data != null)
			{
				string commandJsonString = arguments.Data.ToString();
				BaseCommand baseCommand = JsonConvert.DeserializeObject<BaseCommand>(commandJsonString);
				if (baseCommand != null)
				{
					if (baseCommand.Command == ZC_Command.CONFIRM_CONNECT_HOST)
					{
						ConfirmConnectedHost response = JsonConvert.DeserializeObject<ConfirmConnectedHost>(commandJsonString);
						if (response != null)
						{
							if ((!response.IsSuccess ? false : clientPipe == null))
							{
								clientPipe = new ClientPipe(".", "Test", (BasicPipe p) => p.StartStringReaderAsync());
								clientPipe.DataReceived += new EventHandler<PipeEventArgs>(AppMessageRecieveHandler);
								clientPipe.Connect();
							}
						}
					}
					if (clientPipe != null)
					{
						clientPipe.WriteString(commandJsonString);
					}
				}
			}
		}

		private static void Main(string[] args)
		{
			Host = new ChromeServerHost();
			if (args.Contains<string>("--register"))
			{
				Host.GenerateManifest(Program.Description, Program.AllowedOrigins);
				Host.Register();
			}
			else if (!args.Contains<string>("--unregister"))
			{
				Host.LostConnectionToChrome += Host_LostConnectionToChrome;
				Host.MessageReceived += HostMessageRevieveHandler;
				Host.Listen();
			}
			else
			{
				Host.UnRegister();
			}
		}
	}
}