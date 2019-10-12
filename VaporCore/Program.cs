using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace VaporCore
{
	
	class Program
	{
		public static Func<byte[], string> GetString => Encoding.UTF8.GetString;
		static Dictionary<Guid, PUser> UserList = new Dictionary<Guid, PUser>();
		public static byte[] bruh;
		public static string bruhstring;
		static void Main(string[] args)
		{
			TcpListener serverSocket = new TcpListener(8888);
			TcpClient clientSocket = default;
			serverSocket.Start();
			byte[] bytesFrom = new byte[65536];
			
			string FromClient;
			while (true)
			{
				clientSocket = serverSocket.AcceptTcpClient();
				NetworkStream NetStream = clientSocket.GetStream();
				NetStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
				FromClient = GetString(bytesFrom);
				FromClient = FromClient.Substring(0, FromClient.IndexOf('\0'));
				if (FromClient[0..3] == "req") //requesting server action
				{
					if (FromClient[3..6] == "reg") //register account
					{
						
						var user = new PUser
						{
							UserName = FromClient.Substring(6, FromClient.IndexOf("\u0003") - 6),
							NickName = FromClient.Substring(6, FromClient.IndexOf("\u0003") - 6),
							Status = Status.Online,
							IP = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString(),
							UUID = Guid.NewGuid(),
							PassKey = FromClient.Substring(FromClient.IndexOf("\u0003") + 1)
						};
						UserList.Add(user.UUID, user);
						CUser h = user;
						File.WriteAllText("dict.json", JsonConvert.SerializeObject(UserList));
					}
					
				}
			}
		}
	}
}
