using System;
using System.ComponentModel;

namespace CommandModel
{
	public enum ZC_Command
	{
		[Description("NOTIFICATION")]
		NOTIFICATION = 1,
		[Description("CHECK_CONNECT_STATUS")]
		CHECK_CONNECT_STATUS = 2,
		[Description("SET_LANGUAGE_REQUEST")]
		SET_LANGUAGE = 3,
		[Description("SET_PROFILE_REQUEST")]
		SET_PROFILE_REQUEST = 4,
		[Description("SET_PROFILE_RESPONSE")]
		SET_PROFILE_RESPONSE = 5,
		[Description("CONFIRM_CONNECT_HOST")]
		CONFIRM_CONNECT_HOST = 6,
		[Description("CLOSE_BROWSER")]
		CLOSE_BROWSER = 7,
		[Description("DISCONNECT")]
		DISCONNECT = 8
	}
}