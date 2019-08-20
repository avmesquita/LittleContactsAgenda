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
    public class AddressesController : Controller
    {
        private readonly AgendaContext db = new AgendaContext();

        // GET: Addresses
        public async Task<ActionResult> Index()
        {
            var enderecos = db.Enderecos.Include(a => a.Contact);
            return View(await enderecos.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Enderecos.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {            
            return View();
        }

		public ActionResult Create(string contactId)
		{
			if (contactId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var data = new Address()
			{
				ContactId = contactId,
				ContactType = ContactType.Default,
				AddressId = string.Empty,
				AddressContent = string.Empty
			};
			return View(data);
		}


		// POST: Addresses/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AddressId,ContactId,AddressContent,ContactType")] Address address)
        {
            if (ModelState.IsValid)
            {
				address.AddressId = Guid.NewGuid().ToString();

				var contact = db.Contatos.Find(address.ContactId);
				address.Contact = contact;
				db.Enderecos.Add(address);

                await db.SaveChangesAsync();

				return new HttpStatusCodeResult(HttpStatusCode.OK);
			}            
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Enderecos.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }            
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AddressId,ContactId,AddressContent,ContactType")] Address address)
        {
            if (ModelState.IsValid)
            {
				var contact = db.Contatos.Find(address.ContactId);
				address.Contact = contact;

				db.Entry(address).State = EntityState.Modified;

				await db.SaveChangesAsync();

				return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
            ViewBag.ContactId = address.ContactId;
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Enderecos.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Address address = await db.Enderecos.FindAsync(id);

			var contact = db.Contatos.Find(address.ContactId);
			address.Contact = contact;

			db.Enderecos.Remove(address);
            await db.SaveChangesAsync();
			
			return new HttpStatusCodeResult(HttpStatusCode.OK);
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
