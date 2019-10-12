using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
namespace VaporCore
{
	enum Status
	{
		Online,
		Away,
		Dnd,
		Offline
	}
	class IUser
	{
		public virtual string UserName { get; set; }
		public virtual string NickName { get; set; }
		public virtual Status Status { get; set; }
		public virtual string IP { get; set; }
		public virtual Guid UUID { get; set; }
		
	}
}
