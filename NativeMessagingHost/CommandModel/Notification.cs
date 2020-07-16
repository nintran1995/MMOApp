using System;
using System.Runtime.CompilerServices;

namespace CommandModel
{
	public class Notification : BaseCommand
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

		public NotificationType Type
		{
			get;
			set;
		}

		public Notification()
		{
			this.Command = ZC_Command.NOTIFICATION;
			this.Type = NotificationType.INFOR;
			this.Message = "";
		}

		public Notification(NotificationType type, string message)
		{
			this.Command = ZC_Command.NOTIFICATION;
			this.Type = type;
			this.Message = message;
		}
	}
}