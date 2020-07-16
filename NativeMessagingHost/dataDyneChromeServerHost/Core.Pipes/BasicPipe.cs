using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clifton.Core.Pipes
{
	public abstract class BasicPipe
	{
		protected PipeStream pipeStream;

		protected Action<BasicPipe> asyncReaderStart;

		public BasicPipe()
		{
		}

		public void Close()
		{
			this.pipeStream.WaitForPipeDrain();
			this.pipeStream.Close();
			this.pipeStream.Dispose();
			this.pipeStream = null;
		}

		public void Flush()
		{
			this.pipeStream.Flush();
		}

		public void StartByteReaderAsync()
		{
			this.StartByteReaderAsync((byte[] b) => {
				EventHandler<PipeEventArgs> eventHandler = this.DataReceived;
				if (eventHandler != null)
				{
					eventHandler(this, new PipeEventArgs(b, (int)b.Length));
				}
				else
				{
				}
			});
		}

		protected void StartByteReaderAsync(Action<byte[]> packetReceived)
		{
			int intSize = 4;
			byte[] numArray1 = new byte[intSize];
			this.pipeStream.ReadAsync(numArray1, 0, intSize).ContinueWith((Task<int> t) => {
				int result = t.Result;
				if (result != 0)
				{
					int dataLength = BitConverter.ToInt32(numArray1, 0);
					byte[] numArray = new byte[dataLength];
					this.pipeStream.ReadAsync(numArray, 0, dataLength).ContinueWith((Task<int> t2) => {
						result = t2.Result;
						if (result != 0)
						{
							packetReceived(numArray);
							this.StartByteReaderAsync(packetReceived);
						}
						else
						{
							EventHandler<EventArgs> pipeClosed = this.PipeClosed;
							if (pipeClosed != null)
							{
								pipeClosed(this, EventArgs.Empty);
							}
							else
							{
							}
						}
					});
				}
				else
				{
					EventHandler<EventArgs> eventHandler = this.PipeClosed;
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
					else
					{
					}
				}
			});
		}

		public void StartStringReaderAsync()
		{
			this.StartByteReaderAsync((byte[] b) => {
				string str = Encoding.UTF8.GetString(b).TrimEnd(new char[1]);
				EventHandler<PipeEventArgs> eventHandler = this.DataReceived;
				if (eventHandler != null)
				{
					eventHandler(this, new PipeEventArgs(str));
				}
				else
				{
				}
			});
		}

		public Task WriteBytes(byte[] bytes)
		{
			byte[] blength = BitConverter.GetBytes((int)bytes.Length);
			byte[] bfull = blength.Concat<byte>(bytes).ToArray<byte>();
			Task task = this.pipeStream.WriteAsync(bfull, 0, (int)bfull.Length);
			return task;
		}

		public Task WriteString(string str)
		{
			return this.WriteBytes(Encoding.UTF8.GetBytes(str));
		}

		public event EventHandler<PipeEventArgs> DataReceived;

		public event EventHandler<EventArgs> PipeClosed;
	}
}