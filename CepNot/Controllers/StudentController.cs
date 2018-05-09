using CepNot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CepNot.Controllers
{
    public class StudentController : Controller
    {
        public AllList mylist = new AllList();
        int defaultPageSize = 5;

        // GET: Student
        public ActionResult Index(int? p,string filter)
        {
            if (p == null)
                p = 1;
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                int userid = Convert.ToInt32(Session["UserId"]); 

                //Görevleri alıyoruz
                using (var db = new CepNotEntities())
                {
                    //Filter
                    IQueryable<Tasks> query = null;
                    if (string.IsNullOrEmpty(filter))
                    {
                        query = db.Tasks.Where(x => 1 == 1 && x.SId == userid);
                    }
                    else
                    {
                        query = db.Tasks.Where(x => x.Title.Contains(filter) && x.SId == userid);
                    }

                    mylist.CurrentPage = p.Value;
                    mylist.TotalCount = query.Count();
                    if ((mylist.TotalCount % defaultPageSize) == 0)
                    {
                        mylist.TotalPage = mylist.TotalCount / defaultPageSize;
                    }
                    else
                    {
                        mylist.TotalPage = (mylist.TotalCount / defaultPageSize) + 1;
                    }
                    mylist.Tasks = query.OrderByDescending(x => x.TaskID).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
                }
            }
            return View(mylist);
        }

        public ActionResult Calendar()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Notes()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult UserProfileO(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Users userdetail = null;

                using (var db = new CepNotEntities())
                {
                    userdetail = db.Users.Where(d => d.UserID == id).First();
                }
                return View(userdetail);
            }
        }
        public ActionResult UserEditO(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Users useredit = null;

                using (var db = new CepNotEntities())
                {
                    useredit = db.Users.Where(d => d.UserID == id).First();
                }
                return View(useredit);
            }
        }

        public ActionResult UserUpdateO(Users user)
        {
            Users userupdate = null;

            using (var db = new CepNotEntities())
            {
                userupdate = db.Users.Where(d => d.UserID == user.UserID).First();
                userupdate.Name = user.Name;
                userupdate.Age = user.Age;
                userupdate.Gender = user.Gender;
                userupdate.Mail = user.Mail;
                userupdate.Password = user.Password;

                db.SaveChanges();
            }
            return RedirectToAction("UserProfileO", new { id = user.UserID });

        }

        public ActionResult TaskDetailO(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Tasks taskdetail = null;

                using (var db = new CepNotEntities())
                {
                    taskdetail = db.Tasks.Where(d => d.TaskID == id).First();
                }
                return View(taskdetail);

            }           
        }       

    }
}