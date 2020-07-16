using System;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Clifton.Core.Pipes
{
	public class ServerPipe : BasicPipe
	{
		protected NamedPipeServerStream serverPipeStream;
		public long Id { get; set; }

		protected string PipeName
		{
			get;
			set;
		}

		public ServerPipe(string pipeName, Action<BasicPipe> asyncReaderStart)
		{
			this.asyncReaderStart = asyncReaderStart;
			this.PipeName = pipeName;
			this.serverPipeStream = new NamedPipeServerStream(pipeName, PipeDirection.InOut, -1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
			this.pipeStream = this.serverPipeStream;
			this.serverPipeStream.BeginWaitForConnection(new AsyncCallback(this.PipeConnected), null);
			//this.Id = id;
		}

		protected void PipeConnected(IAsyncResult ar)
		{
			this.serverPipeStream.EndWaitForConnection(ar);
			EventHandler<EventArgs> eventHandler = this.Connected;
			if (eventHandler != null)
			{
				eventHandler(this, new EventArgs());
			}
			else
			{
			}
			this.asyncReaderStart(this);
		}

		public event EventHandler<EventArgs> Connected;
	}
}