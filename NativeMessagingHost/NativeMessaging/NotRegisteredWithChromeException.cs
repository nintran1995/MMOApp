using System;
using System.Runtime.Serialization;

namespace NativeMessaging
{
	[Serializable]
	public class NotRegisteredWithChromeException : Exception
	{
		public NotRegisteredWithChromeException()
		{
		}

		public NotRegisteredWithChromeException(string message) : base(message)
		{
		}

		public NotRegisteredWithChromeException(string message, Exception inner) : base(message, inner)
		{
		}

		protected NotRegisteredWithChromeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}