using LittleAgenda.Context;
using LittleAgenda.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleAgenda.Repository.Interface
{
	public interface IAgendaRepository : IDisposable
	{
		Contact GetContact(string ContactId);

		IList<Contact> GetContacts();

		Contact InsertContact(Contact contato);

		Contact UpdateContact(Contact contato);

		Contact DeleteContact(Contact contato);
	}
}