using System;
using System.Runtime.CompilerServices;

namespace CommandModel
{
	public class BaseCommand
	{
		public ZC_Command Command
		{
			get;
			set;
		}

		public BaseCommand() { }

		public BaseCommand(ZC_Command command)
		{
			this.Command = command;
		}
	}
}