using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SUNController : Controller
    {
        private OTTestContext db = new OTTestContext();

        // GET: /SUN/
        public ActionResult Index()
        {

            var cctransactions = db.CCTransactions;
            return View(cctransactions.ToList());
        }

        private IEnumerable<int> RiskIdsInReferences(IEnumerable<RiskReference> x)
        {
            foreach(var y in x)
                yield return y.Risk.RiskId;
        }

        // GET: /SUN/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CCTransaction cctransaction = db.CCTransactions.Find(id);
            if (cctransaction == null)
            {
                return HttpNotFound();
            }
            return View(cctransaction);
        }

        // GET: /SUN/Create
        public ActionResult Create()
        {
            ViewBag.CCTransactionId = new SelectList(db.Risks, "RiskId", "Description");
            return View();
        }

        // POST: /SUN/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CCTransactionId,RiskId,Reference,Description")] CCTransaction cctransaction)
        {
            if (ModelState.IsValid)
            {
                db.CCTransactions.Add(cctransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CCTransactionId = new SelectList(db.Risks, "RiskId", "Description", cctransaction.CCTransactionId);
            return View(cctransaction);
        }

        // GET: /SUN/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CCTransaction cctransaction = db.CCTransactions.Find(id);
            if (cctransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.CCTransactionId = new SelectList(db.Risks, "RiskId", "Description", cctransaction.CCTransactionId);
            return View(cctransaction);
        }

        // POST: /SUN/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CCTransactionId,RiskId,Reference,Description")] CCTransaction cctransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cctransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CCTransactionId = new SelectList(db.Risks, "RiskId", "Description", cctransaction.CCTransactionId);
            return View(cctransaction);
        }

        // GET: /SUN/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CCTransaction cctransaction = db.CCTransactions.Find(id);
            if (cctransaction == null)
            {
                return HttpNotFound();
            }
            return View(cctransaction);
        }

        // POST: /SUN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CCTransaction cctransaction = db.CCTransactions.Find(id);
            db.CCTransactions.Remove(cctransaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string txtboxSearchReference)
        {
            
            var x = db.RiskReferences.Where(r => r.Reference.Equals(txtboxSearchReference, StringComparison.CurrentCultureIgnoreCase));
            var cctransactions = db.CCTransactions.Include(c => c.Risk).Where(cc => x.Any(y=>y.RiskId == cc.RiskId));
            return View(cctransactions.ToList());
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
