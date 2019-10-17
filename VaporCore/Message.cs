using System;
using System.Collections.Generic;
using System.Text;

namespace VaporCore
{
	public class Message
	{
		public DateTime TimeStamp { get; set; }
		public string Content { get; set; }
		
	}
	public class SentMessage : Message 
	{
		public Guid Recipient { get; set; }
	}
}
