using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CepNot.Models;

namespace CepNot.Controllers
{
    public class LogsController : Controller
    {
        private CepNotEntities db = new CepNotEntities();

        // GET: Logs
        public ActionResult Index()
        {
            return View(db.Logs.ToList());
        }

        // GET: Logs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logs logs = db.Logs.Find(id);
            if (logs == null)
            {
                return HttpNotFound();
            }
            return View(logs);
        }

        // GET: Logs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogID,MethodName,ExMessage,Message,CreatedDate,CreatedUser")] Logs logs)
        {
            if (ModelState.IsValid)
            {
                db.Logs.Add(logs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logs);
        }

        // GET: Logs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logs logs = db.Logs.Find(id);
            if (logs == null)
            {
                return HttpNotFound();
            }
            return View(logs);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogID,MethodName,ExMessage,Message,CreatedDate,CreatedUser")] Logs logs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logs);
        }

        // GET: Logs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logs logs = db.Logs.Find(id);
            if (logs == null)
            {
                return HttpNotFound();
            }
            return View(logs);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logs logs = db.Logs.Find(id);
            db.Logs.Remove(logs);
            db.SaveChanges();
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
