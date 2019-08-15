using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLiteNetExtensions.Attributes;

namespace LittleAgenda.Entity
{
	[Table("Contact")]
	[Serializable]
	public class Contact
	{
		[Key]
		[Column("ContactId")]
		[Display(Name="Internal Code")]
		public string ContactId { get; set; } = Guid.NewGuid().ToString();

		[Required]
		[MaxLength(100, ErrorMessage = "Contact name must be 100 characters or less"), MinLength(3)]
		[Column("Name")]
		[Display(Name = "Name")]
		[Index(IsUnique = true)]
		public string Name { get; set; }

		[Column("Observation")]
		[Display(Name = "Observation")]
		public string Observation { get; set; }

		//[Index("FavoritesContacts")]
		[Display(Name = "Favorite Contact")]
		public bool FavoriteTag { get; set; }

		[Display(Name = "Addresses")]
		[OneToMany]
		public virtual ICollection<Address> Addresses { get; set; }

		[Display(Name = "Telephones")]
		[OneToMany]
		public virtual ICollection<Telephone> Telephones { get; set; }

		[Display(Name = "Emails")]
		[OneToMany]
		public virtual ICollection<Email> Emails { get; set; }

		public Contact()
		{
			this.ContactId = Guid.NewGuid().ToString();
			this.Addresses = new List<Address>();
			this.Emails = new List<Email>();
			this.Telephones = new List<Telephone>();
		}

	}
}