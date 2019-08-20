using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using LittleAgenda.Entity;

namespace LittleAgenda.Infrastructure
{
	/// <summary>
	/// Not used
	/// </summary>
	public class SQLiteDatabase
	{
		/// <summary>
		/// Objeto Connection Real
		/// </summary>
		private SQLiteConnection _connection;

		/// <summary>
		/// Retorna o objeto Connection somente leitura
		/// </summary>
		public SQLiteConnection Connection
		{
			get
			{
				return _connection;
			}
		}

		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		/// <param name="connectionstring"></param>
		public SQLiteDatabase(string connectionstring = "Data Source =| DataDirectory | LittleAgenda.s3db; Version=3;New=True;")
		{
			try
			{
				_connection = new SQLiteConnection(connectionstring);
				_connection.Open();
			}
			catch
			{
				//GenerateTables();
			}
		}

		#region CLIENTE
		/// <summary>
		/// Retorna os dados de UM CLIENTE
		/// </summary>
		/// <param name="cliente"></param>
		/// <returns></returns>
		public IList<Contact> GetContacts(string contactId)
		{
			var list = new List<Contact>();


			SQLiteDataAdapter DB;
			DataSet DS = new DataSet();
			DataTable dt = new DataTable();

			string sql = "SELECT ContactId,Name,Observation,FavoriteTag FROM Contact";

			if (!string.IsNullOrEmpty(contactId))
				sql = string.Format("SELECT ContactId,Name,Observation,FavoriteTag FROM Contact WHERE ContactId = {0}", contactId);

			// Specify command below
			DB = new SQLiteDataAdapter(sql, _connection);
			DS.Reset();
			DB.Fill(DS);
			dt = DS.Tables[0];

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow drow = dt.Rows[i];

				var item = new Contact();
				// Only row that have not been deleted
				if (drow.RowState != DataRowState.Deleted)
				{
					item.ContactId = drow["ContactId"].ToString();
					item.Name = drow["Name"].ToString();
					item.Observation = drow["Observation"].ToString();
					item.FavoriteTag = Convert.ToBoolean(drow["FavoriteTag"]);
					list.Add(item);
				}
			}

			return list;
		}


		public void UpdateContact(Contact contact)
		{
			new NotImplementedException("Ops!");
		}

		public void DeleteContact(Contact contact)
		{
			new NotImplementedException("Ops!"); 
		}
		#endregion

		public void InsertContact(Contact contact)
		{
			try
			{
				_connection.Open();

				string cmdstring = string.Format("INSERT INTO Contact (ContactId, Name, Observation) VALUES ('{0}','{1}','{2}')",
												 contact.ContactId,
												 contact.Name,
												 contact.Observation
												 );

				SQLiteCommand mycommand = new SQLiteCommand(cmdstring, _connection);

				mycommand.ExecuteNonQuery();

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}