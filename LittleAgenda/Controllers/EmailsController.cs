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

namespace LittleAgenda.Controllers
{
    public class EmailsController : Controller
    {
        private readonly AgendaContext db = new AgendaContext();

        // GET: Emails
        public async Task<ActionResult> Index()
        {
            var emails = db.Emails.Include(e => e.Contact);
            return View(await emails.ToListAsync());
        }

        // GET: Emails/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = await db.Emails.FindAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

		// GET: Emails/Create		
		public ActionResult Create(string contactId)
        {
			if (contactId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var data = new Email()
			{
				ContactId = contactId,
				ContactType = ContactType.Default,
				EmailId = string.Empty,
				EmailContent = string.Empty
			};
            return View(data);
        }

		public ActionResult InsertNewEmail(string contactId)
		{
			if (contactId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			return RedirectToAction("InsertNewEmail", "Contacts", contactId);
		}


		// POST: Emails/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmailId,ContactId,EmailContent,ContactType")] Email email)
        {
            if (ModelState.IsValid)
            {
				var contact = db.Contatos.Find(email.ContactId);
				email.Contact = contact;
				email.EmailId = Guid.NewGuid().ToString();

				db.Emails.Add(email);
                await db.SaveChangesAsync();                
            }
            ViewBag.ContactId = email.ContactId;
            return View(email);
        }


		// GET: Emails/Edit/5
		public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = await db.Emails.FindAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contatos, "ContactId", "Name", email.ContactId);
            return View(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmailId,ContactId,EmailContent,ContactType")] Email email)
        {
            if (ModelState.IsValid)
            {
                db.Entry(email).State = EntityState.Modified;

				var contact = db.Contatos.Find(email.ContactId);
				email.Contact = contact;

				await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.Contatos, "ContactId", "Name", email.ContactId);
            return View(email);
        }

        // GET: Emails/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = await db.Emails.FindAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
			try
			{

				Email email = await db.Emails.FindAsync(id);

				/*
				 TODO: HOUSTON, WE HAVE A PROBLEM HERE... MAYBE LATER 
				 */

				var contact = db.Contatos.Find(email.ContactId);
				email.Contact = contact;
				email.ContactId = contact.ContactId;

				db.Emails.Remove(email);
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw ex;
			}
            return RedirectToAction("Index");

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
