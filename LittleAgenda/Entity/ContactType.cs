using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleAgenda.Entity
{
	[Serializable]
	public enum ContactType
	{
		Default = 0,
		Home = 1,
		HomeWork = 2,
		Work = 3,
		Messaging = 4
	}
}