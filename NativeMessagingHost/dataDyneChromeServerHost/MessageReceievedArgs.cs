using Newtonsoft.Json.Linq;
using System;
using System.Runtime.CompilerServices;

namespace ZChangerMMO.BaseHost
{
	public  class  MessageReceievedArgs : EventArgs
	{
		public JObject Data
		{
			get;
			set;
		}

		public MessageReceievedArgs()
		{
		}
	}
}