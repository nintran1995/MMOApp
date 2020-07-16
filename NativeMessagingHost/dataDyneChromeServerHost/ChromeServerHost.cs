using NativeMessaging;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ZChangerMMO.BaseHost
{
	public  class  ChromeServerHost : Host
	{
		public override string Hostname
		{
			get
			{
				return "zchanger.com";
			}
		}

		//public Action<object, EventArgs> LostConnectionToChrome
		//{
		//	get;
		//	public  set;
		//}

		public ChromeServerHost() : base(true)
		{
		}

		protected override void ProcessReceivedMessage(JObject data)
		{
			if (this.MessageReceived != null)
			{
				this.MessageReceived(this, new MessageReceievedArgs()
				{
					Data = data
				});
			}
		}

		public event MessageReceivedHandler MessageReceived;
	}
}