using System;
using System.Runtime.CompilerServices;

namespace Clifton.Core.Pipes
{
	public class PipeEventArgs
	{
		public byte[] Data
		{
			get;
			protected set;
		}

		public int Len
		{
			get;
			protected set;
		}

		public string String
		{
			get;
			protected set;
		}

		public PipeEventArgs(string str)
		{
			this.String = str;
		}

		public PipeEventArgs(byte[] data, int len)
		{
			this.Data = data;
			this.Len = len;
		}
	}
}