using HostClientCommunication;
using Newtonsoft.Json.Linq;
using System;
using System.IO.Pipes;
using System.Security.Principal;

namespace ChromeControlLibrary
{
	public static class ChromeControl
	{
		public static bool ChangeTabUrl(int tabId, string url)
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
				serverCommunication.SendMessage(string.Format("changetaburl {0} {1}", tabId, url));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				flag = response == "Tab URL updated";
			}
			return flag;
		}

		public static bool ChangeWindowState(int windowId, ChromeWindowStates windowState)
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
				serverCommunication.SendMessage(string.Format("changestate {0} {1}", windowId, windowState.ToString().ToLower()));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				flag = !response.Contains("invalid");
			}
			return flag;
		}

		public static bool CloseTab(int tabId)
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
				serverCommunication.SendMessage(string.Format("closetab {0}", tabId));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				flag = response == string.Format("Tab {0} removed", tabId);
			}
			return flag;
		}

		public static bool CloseWindow(int windowId)
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
				serverCommunication.SendMessage(string.Format("closewindow {0}", windowId));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				flag = response == string.Format("Window {0} removed", windowId);
			}
			return flag;
		}

		public static bool FocusWindow(int windowId)
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
				serverCommunication.SendMessage(string.Format("focuswindow {0}", windowId));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				flag = response == string.Format("Window Id: {0} focused", windowId);
			}
			return flag;
		}

		public static bool GetTabIds(out int[] ids)
		{
			bool flag;
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "dataDyneChromeServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
			pipeClient.Connect();
			ServerCommunication serverCommunication = new ServerCommunication(pipeClient);
			if (serverCommunication.ReadMessage() != "dataDyne Chrome Server")
			{
				ids = null;
				flag = false;
			}
			else
			{
				serverCommunication.SendMessage("gettabs");
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				try
				{
					string[] stringIds = response.Split(new char[] { ',' });
					int[] finalIds = new int[(int)stringIds.Length];
					for (int i = 0; i < (int)stringIds.Length; i++)
					{
						finalIds[i] = int.Parse(stringIds[i]);
					}
					ids = finalIds;
					flag = true;
				}
				catch (Exception exception)
				{
					ids = null;
					flag = false;
				}
			}
			return flag;
		}

		public static bool GetTabIdsInWindow(int windowId, out int[] ids)
		{
			bool flag;
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "dataDyneChromeServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
			pipeClient.Connect();
			ServerCommunication serverCommunication = new ServerCommunication(pipeClient);
			if (serverCommunication.ReadMessage() != "dataDyne Chrome Server")
			{
				ids = null;
				flag = false;
			}
			else
			{
				serverCommunication.SendMessage(string.Format("gettabsinwindow {0}", windowId));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				try
				{
					string[] stringIds = response.Split(new char[] { ',' });
					int[] finalIds = new int[(int)stringIds.Length];
					for (int i = 0; i < (int)stringIds.Length; i++)
					{
						finalIds[i] = int.Parse(stringIds[i]);
					}
					ids = finalIds;
					flag = true;
				}
				catch (Exception exception)
				{
					ids = null;
					flag = false;
				}
			}
			return flag;
		}

		public static bool GetUrl(int tabId, out string url)
		{
			bool flag;
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "dataDyneChromeServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
			pipeClient.Connect();
			ServerCommunication serverCommunication = new ServerCommunication(pipeClient);
			if (serverCommunication.ReadMessage() != "dataDyne Chrome Server")
			{
				url = "";
				flag = false;
			}
			else
			{
				serverCommunication.SendMessage(string.Format("geturl {0}", tabId));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				url = response;
				flag = !response.Contains("does not exist");
			}
			return flag;
		}

		public static bool GetWindowIds(out int[] ids)
		{
			bool flag;
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "dataDyneChromeServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
			pipeClient.Connect();
			ServerCommunication serverCommunication = new ServerCommunication(pipeClient);
			if (serverCommunication.ReadMessage() != "dataDyne Chrome Server")
			{
				ids = null;
				flag = false;
			}
			else
			{
				serverCommunication.SendMessage("getwindows");
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				try
				{
					string[] stringIds = response.Split(new char[] { ',' });
					int[] finalIds = new int[(int)stringIds.Length];
					for (int i = 0; i < (int)stringIds.Length; i++)
					{
						finalIds[i] = int.Parse(stringIds[i]);
					}
					ids = finalIds;
					flag = true;
				}
				catch (Exception exception)
				{
					ids = null;
					flag = false;
				}
			}
			return flag;
		}

		public static bool GetWindowPosition(int windowId, out int x, out int y)
		{
			bool flag;
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "dataDyneChromeServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
			pipeClient.Connect();
			ServerCommunication serverCommunication = new ServerCommunication(pipeClient);
			if (serverCommunication.ReadMessage() != "dataDyne Chrome Server")
			{
				x = -9999;
				y = -9999;
				flag = false;
			}
			else
			{
				serverCommunication.SendMessage(string.Format("getwindowpos {0}", windowId));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				if (!response.Contains("does not exist"))
				{
					JObject ro = JObject.Parse(response);
					JToken left = ro["Left"];
					JToken top = ro["Top"];
					x = int.Parse(left.ToString());
					y = int.Parse(top.ToString());
					flag = true;
				}
				else
				{
					x = -9999;
					y = -9999;
					flag = false;
				}
			}
			return flag;
		}

		public static bool MoveWindow(int windowId, int xPos, int yPos)
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
				serverCommunication.SendMessage(string.Format("movewindow {0} {1} {2}", windowId, xPos, yPos));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				flag = !response.Contains("invalid");
			}
			return flag;
		}

		public static bool OpenTab(int windowId, string url, out int tabId)
		{
			bool flag;
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "dataDyneChromeServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
			pipeClient.Connect();
			ServerCommunication serverCommunication = new ServerCommunication(pipeClient);
			if (serverCommunication.ReadMessage() != "dataDyne Chrome Server")
			{
				tabId = -1;
				flag = false;
			}
			else
			{
				serverCommunication.SendMessage(string.Format("opentab {0} {1}", windowId, url));
				string response = serverCommunication.ReadMessageAsJObject()["text"].ToString();
				tabId = int.Parse(response);
				flag = true;
			}
			return flag;
		}

		public static bool OpenWindow(string url, out int windowId, out int tabId)
		{
			bool flag;
			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "dataDyneChromeServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
			pipeClient.Connect();
			ServerCommunication serverCommunication = new ServerCommunication(pipeClient);
			if (serverCommunication.ReadMessage() != "dataDyne Chrome Server")
			{
				windowId = -1;
				tabId = -1;
				flag = false;
			}
			else
			{
				serverCommunication.SendMessage(string.Concat("openwindow ", url));
				JObject responseObject = serverCommunication.ReadMessageAsJObject();
				JObject ro = JObject.Parse(responseObject["text"].ToString());
				JToken windowIdString = ro["windowId"];
				JToken tabIdString = ro["tabId"];
				windowId = int.Parse(windowIdString.ToString());
				tabId = int.Parse(tabIdString.ToString());
				flag = true;
			}
			return flag;
		}
	}
}