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
	[Table("Address")]
	public class Address
	{
		[Key,DatabaseGenerated(DatabaseGeneratedOption.None),SQLite.PrimaryKey]		
		[Display(Name = "Internal Code")]
		public string AddressId { get; set; }
				
		[SQLiteNetExtensions.Attributes.ForeignKey(typeof(Contact))]
		[Display(Name = "Contact")]
		public string ContactId { get; set; }

		[Display(Name ="Address")]
		public string AddressContent { get; set; }

		[Column("ContactType")]
		[Display(Name = "Contact Type")]
		public ContactType? ContactType { get; set; }
		
		[ManyToOne, System.ComponentModel.DataAnnotations.Schema.ForeignKey("ContactId")]
		public virtual Contact Contact { get; set; }

		public Address()
		{
			this.AddressId = Guid.NewGuid().ToString();
			this.ContactType = ContactType.GetValueOrDefault();
			this.AddressContent = string.Empty;
			this.ContactId = string.Empty;
		}

	}
}