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
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None),SQLite.PrimaryKey]
		[Display(Name = "Internal Code")]
		public string EmailId { get; set; }

		[SQLiteNetExtensions.Attributes.ForeignKey(typeof(Contact))]
		[Display(Name = "Contact")]
		public string ContactId { get; set; }

		[Display(Name = "E-Mail Account")]		
		public string EmailContent { get; set; }

		[Column("ContactType")]
		[Display(Name = "Contact Type")]
		public ContactType? ContactType { get; set; }
		
		[ManyToOne]
		[Display(Name = "Contact"), System.ComponentModel.DataAnnotations.Schema.ForeignKey("ContactId")]
		public Contact Contact { get; set; }

		public Email()
		{
			this.EmailId = Guid.NewGuid().ToString();
			this.EmailContent = string.Empty;
			this.ContactType = ContactType.GetValueOrDefault();
			this.ContactId = string.Empty;			
		}
	}

}