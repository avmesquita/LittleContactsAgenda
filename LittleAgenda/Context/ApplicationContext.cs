using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

namespace LittleAgenda.Context
{
	public class ApplicationContext
	{
		//@"C:\Users\andre\Source\Repos\LittleAgenda\LittleAgenda\Database\LittleAgenda.db3"

		private const string DATABASE_PATH = "SQLite.Database";

		public string GetSQLiteDatabase()
		{
			try
			{				
				string _sqlite = ConfigurationManager.AppSettings[DATABASE_PATH].ToString();

				if (File.Exists(_sqlite))
				{
					return _sqlite;
				}
				else throw new Exception("Loading default database");
			}
			catch
			{
				string _path = Convert.ToString(HttpRuntime.AppDomainAppPath);
				var _sqlite = _path + @"\Database\LittleAgenda.db3";

				return _sqlite;
			}			
		}

	}
}