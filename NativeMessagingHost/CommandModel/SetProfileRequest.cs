using System;
using System.Runtime.CompilerServices;

namespace CommandModel
{
	public class SetProfileRequest : BaseCommand
	{
		public BrowserProfile Profile
		{
			get;
			set;
		}

		public SetProfileRequest()
		{
		}
	}
}