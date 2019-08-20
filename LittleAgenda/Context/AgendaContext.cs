using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LittleAgenda.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LittleAgenda.Context
{
	public class AgendaContext : DbContext
	{
		public AgendaContext() : base(new SQLiteConnection
		{
			ConnectionString = new SQLiteConnectionStringBuilder
			{
				DataSource = @"C:\Users\andre\Source\Repos\LittleAgenda\LittleAgenda\Database\LittleAgenda.db3",
				ForeignKeys = true,				
				FailIfMissing=false,
				Version=3				
			}.ConnectionString
		}, true)
		{
			Database.Log = Console.Write;
			//this.Configuration.LazyLoadingEnabled = false;			
		}

		public AgendaContext(string path)

			: base(new SQLiteConnection
			{
				ConnectionString = new SQLiteConnectionStringBuilder
				{
					DataSource = path,
					ForeignKeys = true,					
					FailIfMissing = false,
					Version = 3										
				}.ConnectionString
			}, true)
		{

			Database.Log = Console.Write;
			Database.ExecuteSqlCommand("PRAGMA foreign_keys = ON");
			//this.Configuration.LazyLoadingEnabled = false;
			//this.Configuration.ProxyCreationEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			Database.SetInitializer<AgendaContext>(null);

			/*
			try
			{
				Database.Connection.ConnectionString = @"Data Source=C:\Users\andre\Source\Repos\LittleAgenda\LittleAgenda\Database\LittleAgenda.db3;ForeignKeys=true;Version=3;FailIfMissing=False;Journal Mode=Off";

				Database.Connection.Open();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			*/
		}

		public DbSet<Contact> Contatos { get; set; }
		public DbSet<Address> Enderecos { get; set; }
		public DbSet<Telephone> Telefones { get; set; }
		public DbSet<Email> Emails { get; set; }
	}
}