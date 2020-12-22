using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using fNbt.Tags;

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
				MemoryStream stream = new MemoryStream();
				stream.Write(bytesFrom, 0, bytesFrom.Length);
				var rr = new fNbt.NbtFile();
				rr.LoadFromBuffer(bytesFrom, 0, 65536, fNbt.NbtCompression.GZip);
				var root = rr.RootTag;
				switch ((ReqType)(root["Type"] as NbtInt).IntValue)
				{
					case ReqType.REGISTER:
						var uuid = Guid.NewGuid();
						UserList.Add(uuid, new PUser
						{
							UserName = root["Data"]["UserName"].StringValue,
							NickName = root["Data"]["UserName"].StringValue,
							UUID = uuid,
							PassKey = root["Data"]["PassKey"].StringValue
						});
						NetStream.Write(uuid.ToByteArray(), 0, uuid.ToByteArray().Length);
						break;
				}
				
			}
		}
	}
	enum ReqType : int
	{
		REGISTER = 0,
		SEND_MESSAGE = 1
	}
}
