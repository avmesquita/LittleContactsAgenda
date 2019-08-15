using LittleAgenda.Context;
using LittleAgenda.Entity;
using LittleAgenda.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleAgenda.Repository
{
	public class AgendaRepository : IAgendaRepository
	{
		private readonly AgendaContext _context;

		public AgendaRepository(AgendaContext context)
		{
			_context = context;
		}

		public Contact GetContact(string ContactId)
		{
			var contato = (from a in _context.Contatos
						  where a.ContactId == ContactId
						  select a).FirstOrDefault();

			return contato;
		}

		public IList<Contact> GetContacts()
		{
			var contatos = (from a in _context.Contatos
						   select a).ToList();

			return contatos;
		}

		public Contact InsertContact(Contact contato)
		{
			_context.Contatos.Add(contato);
			_context.SaveChanges();

			return contato;
		}

		public Contact UpdateContact(Contact contato)
		{
			return InsertContact(contato);
		}

		public Contact DeleteContact(Contact contato)
		{
			return _context.Contatos.Remove(contato);			
		}

		public void Dispose()
		{
			_context.Dispose();
			GC.WaitForPendingFinalizers();
		}




	}
}