using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
namespace VaporCore
{
	class PUser : IUser // Privste server side user data
	{
		/*public override string UserName { get; set; }
		public override  string NickName { get; set; }
		public override  Status Status { get; set; }
		public override  IPAddress IP { get; set; }
		public override  Guid UUID { get; set; }*/
		public string PassKey { get; set; }
	}
}
