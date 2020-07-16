using System;
using System.Runtime.CompilerServices;

namespace CommandModel
{
	public class ConfirmConnectedHost : BaseCommand
	{
		public bool IsSuccess
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public ConfirmConnectedHost()
		{
		}
	}
}