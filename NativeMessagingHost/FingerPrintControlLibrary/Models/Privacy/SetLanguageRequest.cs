using FingerPrintControlLibrary.Models;
using System;
using System.Runtime.CompilerServices;

namespace FingerPrintControlLibrary.Models.Privacy
{
	public class SetLanguageRequest : BaseMessage
	{
		public string Value
		{
			get;
			set;
		}

		public SetLanguageRequest()
		{
			base.Command = "SetLanguage";
		}
	}
}