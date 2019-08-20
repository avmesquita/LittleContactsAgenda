using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LittleAgenda.Context;
using LittleAgenda.Entity;
using System.Data.Entity.Validation;

namespace LittleAgenda.Controllers
{
    public class ContactsController : Controller
    {
        private readonly AgendaContext db = new AgendaContext();

        // GET: Contacts
        public async Task<ActionResult> Index()
        {
			try
			{
				var data = await db.Contatos
					.Include(m => m.Addresses)
					.Include(m => m.Telephones)
					.Include(m => m.Emails)
					.ToListAsync();

				return View(data);
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }

        // GET: Contacts/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contatos
				    .Include(m => m.Addresses)
					.Include(m => m.Telephones)
					.Include(m => m.Emails)
					.Where(m => m.ContactId == id)
					.FirstOrDefaultAsync();

            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContactId,Name,Observation,FavoriteTag")] Contact contact)
        {
            if (ModelState.IsValid)
            {
				try
				{
					contact.ContactId = Guid.NewGuid().ToString();
					db.Contatos.Add(contact);
					await db.SaveChangesAsync();
					return RedirectToAction("Index");
				}
				catch (DbEntityValidationException exdb)
				{
					throw exdb;
				}
				catch (Exception ex)
				{
					throw ex;
				}
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contatos.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContactId,Name,Observation,FavoriteTag")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contatos.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Contact contact = await db.Contatos.FindAsync(id);
            db.Contatos.Remove(contact);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

		public async Task<ActionResult> ViewEmails(string ContactId)
		{
			try
			{
				var data = await db.Emails.Include(m => m.Contact).Where(x => x.ContactId == ContactId).ToListAsync();

				return View(data);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<ActionResult> ViewAddresses(string ContactId)
		{
			try
			{
				var data = await db.Enderecos.Include(m => m.Contact).Where(x => x.Contact.ContactId == ContactId).ToListAsync();

				return PartialView("GetAddress",data);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<ActionResult> ViewTelephones(string ContactId)
		{
			try
			{
				var data = await db.Telefones.Include(m => m.Contact).Where(x => x.Contact.ContactId == ContactId).ToListAsync();

				return PartialView("GetTelephones",data);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}





		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
