using System;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace VaporTestClientCLI
{
	class Program
	{
		static TcpClient clientSocket = new TcpClient();
		static NetworkStream serverStream = default(NetworkStream);
		static void Main(string[] args)
		{
			clientSocket.Connect("10.0.0.49", 8888);
			serverStream = clientSocket.GetStream();
			string bruh = "reqregMason\u0003";
			var h = SHA256.Create();
			bruh += Encoding.UTF8.GetString(h.ComputeHash(Encoding.UTF8.GetBytes("somebodyoncetoldme")));
			
			
			byte[] outStream = System.Text.Encoding.UTF8.GetBytes(bruh);
			serverStream.Write(outStream, 0, outStream.Length);
			serverStream.Flush();
			Console.ReadLine();
		}
	}
}
