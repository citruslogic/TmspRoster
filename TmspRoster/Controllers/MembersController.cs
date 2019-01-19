using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TmspRoster.App_Start;
using TmspRoster.Models.Enums;
using TmspRoster.Models.Member;

namespace TmspRoster.Controllers
{
    public class MembersController : Controller
    {
        private SqlDbContext db = new SqlDbContext();

        private List<SelectListItem> SetViewBagDdlLocation()
        {

            IEnumerable<EnumLocation> locations = Enum.GetValues(typeof(EnumLocation)).Cast<EnumLocation>();

            return (from location in locations
                    select new SelectListItem
                    {
                        Text = location.ToString(),
                        Value = ((int)location).ToString()
                    }).ToList();
        }

        // GET: Members
        [Authorize]
        public ActionResult Index(string locationFilter)
        {
            ViewBag.Location = from item in SetViewBagDdlLocation()
                               select item.Text;

            ViewBag.IsPrivileged = User.IsInRole("Admin") || User.IsInRole("Supervisor");

            var members = from m in db.Members
                          select m;

            Enum.TryParse(locationFilter, out EnumLocation selectedLocation);

            if (selectedLocation != EnumLocation.ALL)
            {
                members = members.Where(l => l.Territory == selectedLocation);
            }
            

            return View(members.ToList());
        }

        // GET: Members/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        [Authorize(Roles = "Admin, Supervisor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,LastName,FirstMidName,City,Territory,JoinDate,Status,PlateNumber")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize(Roles = "Admin, Supervisor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,LastName,FirstMidName,City,Territory,JoinDate,Status,PlateNumber")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        [Authorize(Roles = "Admin, Supervisor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [Authorize(Roles = "Admin, Supervisor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
