﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLiteNetExtensions.Attributes;

namespace LittleAgenda.Entity
{
	[Serializable]
	[Table("Telephone")]
	public class Telephone
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None),SQLite.PrimaryKey]
		[Display(Name = "Internal Code")]
		public string TelephoneId { get; set; }

		[SQLiteNetExtensions.Attributes.ForeignKey(typeof(Contact))]
		[Display(Name = "Contact")]
		public string ContactId { get; set; }

		[Display(Name = "Telephone")]
		public string TelephoneContent { get;set; }

		[Column("ContactType")]
		[Display(Name = "Contact Type")]
		public ContactType? ContactType { get; set; }

		[System.ComponentModel.DataAnnotations.Schema.ForeignKey("ContactId")]
		[ManyToOne]
		[Display(Name = "Contact")]
		public Contact Contact { get; set; }

		public Telephone()
		{
			this.TelephoneId = Guid.NewGuid().ToString();
			this.ContactType = ContactType.GetValueOrDefault();
			this.TelephoneContent = string.Empty;
			this.ContactId = string.Empty;			
		}
	}
}