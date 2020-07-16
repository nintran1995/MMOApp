using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace HostClientCommunication
{
	public class ServerCommunication
	{
		private readonly Stream _Stream;

		private readonly UnicodeEncoding _StreamEncoding;

		public ServerCommunication(Stream stream)
		{
			this._Stream = stream;
			this._StreamEncoding = new UnicodeEncoding();
		}

		public string ReadMessage()
		{
			int length = this._Stream.ReadByte() * 256;
			length += this._Stream.ReadByte();
			byte[] buffer = new byte[length];
			this._Stream.Read(buffer, 0, length);
			return this._StreamEncoding.GetString(buffer);
		}

		public JObject ReadMessageAsJObject()
		{
			int length = this._Stream.ReadByte() * 256;
			length += this._Stream.ReadByte();
			byte[] buffer = new byte[length];
			this._Stream.Read(buffer, 0, length);
			return JObject.Parse(this._StreamEncoding.GetString(buffer));
		}

		public int SendMessage(string outString)
		{
			byte[] buffer = this._StreamEncoding.GetBytes(outString);
			int length = (int)buffer.Length;
			if (length > 65535)
			{
				length = 65535;
			}
			this._Stream.WriteByte((byte)(length / 256));
			this._Stream.WriteByte((byte)(length & 255));
			this._Stream.Write(buffer, 0, length);
			this._Stream.Flush();
			return (int)buffer.Length + 2;
		}
	}
}