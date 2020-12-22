using System;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using System.Text;
using fNbt.Tags;
using System.IO;

namespace VaporTestClientCLI
{
	class Program
	{
		static TcpClient clientSocket = new TcpClient();
		static NetworkStream serverStream = default(NetworkStream);
		static void Main(string[] args)
		{
			clientSocket.Connect("localhost", 8888);
			serverStream = clientSocket.GetStream();

			string bruh = "";
			var h = SHA256.Create();
			bruh += Encoding.UTF8.GetString(h.ComputeHash(Encoding.UTF8.GetBytes("somebodyoncetoldme")));
			var root = new NbtCompound("root")
			{
				new NbtInt("Type", 0),
				new NbtCompound("Data")
				{
					new NbtString("UserName", "Mason"),
					new NbtString("PassKey", bruh)
				}
			};
			byte[] outStream = new byte[65536];
			new fNbt.NbtFile(root).SaveToBuffer(outStream, 0, fNbt.NbtCompression.GZip);
			
			serverStream.Write(outStream, 0, outStream.Length);
			byte[] uuid = new byte[16];
			serverStream.Read(uuid, 0, 16);
			Console.WriteLine(new Guid(uuid));
			Console.ReadLine();
		}
	}
}
