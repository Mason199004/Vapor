using System;
using System.Collections.Generic;
using System.Text;

namespace VaporCore
{
	class CUser : IUser
	{
		
		void SendMessage(string message)
		{
			//TODO implement later
		}

		public static implicit operator CUser(PUser v)
		{
			CUser h = new CUser
			{
				UserName = v.UserName,
				NickName = v.NickName,
				Status = v.Status,
				IP = v.IP,
				UUID = v.UUID,
			};

			return h;
		}

		
	}
}
