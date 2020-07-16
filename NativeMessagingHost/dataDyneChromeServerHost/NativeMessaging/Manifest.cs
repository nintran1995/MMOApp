using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

namespace NativeMessaging
{
	internal class Manifest
	{
		[JsonProperty("allowed_origins")]
		public string[] AllowedOrigins
		{
			get;
			set;
		}

		[JsonProperty("description")]
		public string Description
		{
			get;
			set;
		}

		[JsonProperty("path")]
		public string ExecuteablePath
		{
			get;
			set;
		}

		[JsonProperty("name")]
		public string Name
		{
			get;
			set;
		}

		[JsonProperty("type")]
		public string Type
		{
			get
			{
				return "stdio";
			}
		}

		public Manifest(string hostname, string description, string executeablePath, string[] allowedOrigins)
		{
			this.Name = hostname;
			this.Description = description;
			this.AllowedOrigins = allowedOrigins;
			this.ExecuteablePath = executeablePath;
		}
	}
}