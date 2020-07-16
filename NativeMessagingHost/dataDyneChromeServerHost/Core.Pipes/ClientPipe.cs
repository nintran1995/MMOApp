using System;
using System.IO.Pipes;

namespace Clifton.Core.Pipes
{
	public class ClientPipe : BasicPipe
	{
		protected NamedPipeClientStream clientPipeStream;

		public ClientPipe(string serverName, string pipeName, Action<BasicPipe> asyncReaderStart)
		{
			this.asyncReaderStart = asyncReaderStart;
			this.clientPipeStream = new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
			this.pipeStream = this.clientPipeStream;
		}

		public void Connect()
		{
			this.clientPipeStream.Connect();
			this.asyncReaderStart(this);
		}
	}
}