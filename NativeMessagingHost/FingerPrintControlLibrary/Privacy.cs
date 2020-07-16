using FingerPrintControlLibrary.Models.Privacy;
using HostClientCommunication;
using Newtonsoft.Json.Linq;
using System;
using System.IO.Pipes;
using System.Security.Principal;
using System.Web.Script.Serialization;

namespace FingerPrintControlLibrary
{
	public static class Privacy
	{
		public static bool SetCanvasRandom()
		{
			bool flag;
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "dataDyneChromeServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
			pipeClient.Connect();
			ServerCommunication serverCommunication = new ServerCommunication(pipeClient);
			if (serverCommunication.ReadMessage() != "dataDyne Chrome Server")
			{
				flag = false;
			}
			else
			{
				serverCommunication.SendMessage("{command:'SetCanvasRandom'");
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				flag = response == "Success";
			}
			return flag;
		}

		public static bool SetLanguage(string language = "en")
		{
			bool flag;
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "dataDyneChromeServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
			pipeClient.Connect();
			ServerCommunication serverCommunication = new ServerCommunication(pipeClient);
			if (serverCommunication.ReadMessage() != "dataDyne Chrome Server")
			{
				flag = false;
			}
			else
			{
				SetLanguageRequest setLanguageRequest = new SetLanguageRequest()
				{
					Value = language
				};
				serverCommunication.SendMessage((new JavaScriptSerializer()).Serialize(setLanguageRequest));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				flag = response == "Success";
			}
			return flag;
		}
	}
}