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
	[Table("Email")]
	public class Email
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Display(Name = "Internal Code")]
		public string EmailId { get; set; }

		[SQLiteNetExtensions.Attributes.ForeignKey(typeof(Contact))]
		public string ContactId { get; set; }

		[Display(Name = "E-Mail Account")]		
		public string EmailContent { get; set; }

		[Column("ContactType")]
		[Display(Name = "Contact Type")]
		public ContactType ContactType { get; set; }

		[System.ComponentModel.DataAnnotations.Schema.ForeignKey("ContactId")]
		[ManyToOne]
		public Contact Contact { get; set; }

		public Email()
		{
			this.EmailId = Guid.NewGuid().ToString();
		}
	}

}