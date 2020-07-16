using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace NativeMessaging
{
	public abstract class Host
	{
		private readonly bool SendConfirmationReceipt;

		public readonly string ManifestPath;

		public event EventHandler LostConnectionToChrome;

		private const string RegKeyBaseLocation = "SOFTWARE\\Google\\Chrome\\NativeMessagingHosts\\";

		public abstract string Hostname
		{
			get;
		}

		public Host(bool sendConfirmationReceipt = true)
		{
			this.SendConfirmationReceipt = sendConfirmationReceipt;
			this.ManifestPath = Path.Combine(Utils.AssemblyLoadDirectory(), string.Concat(this.Hostname, "-manifest.json"));
		}

		public void GenerateManifest(string description, string[] allowedOrigins)
		{
			//Utils.LogMessage("Generating Manifest");
			string manifest = JsonConvert.SerializeObject(new Manifest(this.Hostname, description, Utils.AssemblyExecuteablePath(), allowedOrigins));
			File.WriteAllText(this.ManifestPath, manifest);
			//Utils.LogMessage("Manifest Generated");
		}

		public bool IsRegisteredWithChrome()
		{
			string regHostnameKeyLocation = string.Concat("SOFTWARE\\Google\\Chrome\\NativeMessagingHosts\\", this.Hostname);
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey(regHostnameKeyLocation, true);
			return ((regKey == null ? true : regKey.GetValue("").ToString() != this.ManifestPath) ? false : true);
		}

		public void Listen()
		{
			if (!this.IsRegisteredWithChrome())
			{
				throw new NotRegisteredWithChromeException(this.Hostname);
			}
			while (true)
			{
				JObject jObjects = this.Read();
				JObject data = jObjects;
				if (jObjects == null)
				{
					break;
				}
				this.ProcessReceivedMessage(data);
			}

			if (LostConnectionToChrome != null)
			{
				LostConnectionToChrome(this, EventArgs.Empty);
			}
		}

		protected abstract void ProcessReceivedMessage(JObject data);

		private JObject Read()
		{
			//Utils.LogMessage("Waiting for Data");
			Stream stdin = Console.OpenStandardInput();
			byte[] lengthBytes = new byte[4];
			stdin.Read(lengthBytes, 0, 4);
			char[] buffer = new char[BitConverter.ToInt32(lengthBytes, 0)];
			using (StreamReader reader = new StreamReader(stdin))
			{
				while (reader.Peek() >= 0)
				{
					reader.Read(buffer, 0, (int)buffer.Length);
				}
			}
			return JsonConvert.DeserializeObject<JObject>(new string(buffer));
		}

		public void Register()
		{
			string regHostnameKeyLocation = string.Concat("SOFTWARE\\Google\\Chrome\\NativeMessagingHosts\\", this.Hostname);
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey(regHostnameKeyLocation, true) ?? Registry.CurrentUser.CreateSubKey(regHostnameKeyLocation);
			regKey.SetValue("", this.ManifestPath, RegistryValueKind.String);
			regKey.Close();
			//Utils.LogMessage(string.Concat("Registered:", this.Hostname));
		}

		public void SendMessage(JObject data)
		{
			//Utils.LogMessage(string.Concat("Sending Message:", JsonConvert.SerializeObject(data)));
			byte[] bytes = Encoding.UTF8.GetBytes(data.ToString(Formatting.None, new JsonConverter[0]));
			Stream stdout = Console.OpenStandardOutput();
			stdout.WriteByte((byte)((int)bytes.Length & 255));
			stdout.WriteByte((byte)((int)bytes.Length >> 8 & 255));
			stdout.WriteByte((byte)((int)bytes.Length >> 16 & 255));
			stdout.WriteByte((byte)((int)bytes.Length >> 24 & 255));
			stdout.Write(bytes, 0, (int)bytes.Length);
			stdout.Flush();
		}

		public void UnRegister()
		{
			string regHostnameKeyLocation = string.Concat("SOFTWARE\\Google\\Chrome\\NativeMessagingHosts\\", this.Hostname);
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey(regHostnameKeyLocation, true);
			if (regKey != null)
			{
				regKey.DeleteSubKey("", true);
			}
			regKey.Close();
			//Utils.LogMessage(string.Concat("Unregistered:", this.Hostname));
		}
	}
}