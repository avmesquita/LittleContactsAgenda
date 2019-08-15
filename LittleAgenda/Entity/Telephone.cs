using System;
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
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Display(Name = "Internal Code")]
		public string TelephoneId { get; set; }

		[SQLiteNetExtensions.Attributes.ForeignKey(typeof(Contact))]
		public string ContactId { get; set; }

		[Display(Name = "Telephone")]
		public string TelephoneContent { get;set; }

		[Column("ContactType")]
		[Display(Name = "Contact Type")]
		public ContactType ContactType { get; set; }

		[System.ComponentModel.DataAnnotations.Schema.ForeignKey("ContactId")]
		[ManyToOne]
		public Contact Contact { get; set; }

		public Telephone()
		{
			this.TelephoneId = Guid.NewGuid().ToString();
		}
	}
}