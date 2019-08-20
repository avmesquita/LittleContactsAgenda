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
    public class TelephonesController : Controller
    {
        private readonly AgendaContext db = new AgendaContext();

        // GET: Telephones
        public async Task<ActionResult> Index()
        {
            var telefones = db.Telefones.Include(t => t.Contact);
            return View(await telefones.ToListAsync());
        }

        // GET: Telephones/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telephone telephone = await db.Telefones.FindAsync(id);
            if (telephone == null)
            {
                return HttpNotFound();
            }
            return View(telephone);
        }

        // GET: Telephones/Create
        public ActionResult Create()
        {
            ViewBag.ContactId = new SelectList(db.Contatos, "ContactId", "Name");
            return View();
        }

        // POST: Telephones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TelephoneId,ContactId,TelephoneContent,ContactType")] Telephone telephone)
        {
            if (ModelState.IsValid)
            {
				var contact = db.Contatos.Find(telephone.ContactId);
				telephone.Contact = contact;
				db.Telefones.Add(telephone);

				await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ContactId = new SelectList(db.Contatos, "ContactId", "Name", telephone.ContactId);
            return View(telephone);
        }

        // GET: Telephones/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telephone telephone = await db.Telefones.FindAsync(id);
            if (telephone == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contatos, "ContactId", "Name", telephone.ContactId);
            return View(telephone);
        }

        // POST: Telephones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TelephoneId,ContactId,TelephoneContent,ContactType")] Telephone telephone)
        {
            if (ModelState.IsValid)
            {
				var contact = db.Contatos.Find(telephone.ContactId);
				telephone.Contact = contact;

				db.Entry(telephone).State = EntityState.Modified;

				await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.Contatos, "ContactId", "Name", telephone.ContactId);
            return View(telephone);
        }

        // GET: Telephones/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telephone telephone = await db.Telefones.FindAsync(id);
            if (telephone == null)
            {
                return HttpNotFound();
            }
            return View(telephone);
        }

        // POST: Telephones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Telephone telephone = await db.Telefones.FindAsync(id);

			var contact = db.Contatos.Find(telephone.ContactId);
			telephone.Contact = contact;

			db.Telefones.Remove(telephone);
            await db.SaveChangesAsync();
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
