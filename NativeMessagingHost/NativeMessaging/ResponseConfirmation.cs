using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.CompilerServices;

namespace NativeMessaging
{
	public class ResponseConfirmation
	{
		[JsonProperty("data")]
		public JObject Data
		{
			get;
			set;
		}

		[JsonProperty("message")]
		public string Message
		{
			get;
			set;
		}

		public ResponseConfirmation(JObject data)
		{
			this.Data = data;
			this.Message = "Confirmation of received data";
		}

		public ResponseConfirmation(string msg)
		{
			this.Message = msg;
		}

		public JObject GetJObject()
		{
			return JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(this));
		}
	}
}