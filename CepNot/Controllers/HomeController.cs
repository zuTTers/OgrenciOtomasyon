using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CepNot.Models;
using System.Collections;
using System.Globalization;
using CepNot.Helper;
using CepNot.ViewModel;


namespace CepNot.Controllers
{

    public class HomeController : Controller
    {
        private const string api = "ecba1371b83d3e680f03c2af4c6edb14";
        private const string baglanti = "http://api.openweathermap.org/data/2.5/weather?q=Duzce&mode=xml&units=metric&appid=" + api;

        public AllList mylist = new AllList();
        public int defaultPageSize = 5;

        public bool InternetKontrol()
        {
            try
            {
                System.Net.Sockets.TcpClient kontrol_client = new System.Net.Sockets.TcpClient("www.google.com.tr", 80);
                kontrol_client.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void HavaDurumuGetir()
        {
            //Hava durumu xml ile fetch ediyoruz
            bool ikontrol = InternetKontrol();
            XDocument hava = null;

            if (ikontrol)
            {
                hava = XDocument.Load(baglanti);
                var sicaklik = hava.Descendants("temperature").ElementAt(0).Attribute("value").Value;
                var durum = hava.Descendants("clouds").ElementAt(0).Attribute("name").Value;
                var yer = hava.Descendants("city").ElementAt(0).Attribute("name").Value;
                var nem = hava.Descendants("humidity").ElementAt(0).Attribute("value").Value;

                if (durum.Contains("clouds") == true)
                {
                    ViewBag.durum = "fa fa-cloud";
                }
                else if (durum.Contains("sun") == true)
                {
                    ViewBag.durum = "fa fa-sun-o";
                }
                else
                {
                    ViewBag.durum = "fa fa-globe";
                }
                ViewBag.sicaklik = sicaklik + "°";
                ViewBag.yer = yer;
                ViewBag.nem = "% " + nem;
            }
            else
            {
                ViewBag.sicaklik = "Bağlantınız";
                ViewBag.yer = "İnternet";
                ViewBag.nem = "Yok";
                ViewBag.durum = "fa fa-plug";
            }
        }


        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                HavaDurumuGetir();

                using (var db = new CepNotEntities())
                {
                    mylist.Tags = db.Tags.ToList();
                    mylist.Users = db.Users.Where(x => x.Role == 3).ToList();
                    mylist.Roles = db.Roles.ToList();
                }

                ViewBag.VBTagsList = new SelectList(mylist.Tags, "TagID", "Title");
                ViewBag.VBSList = new SelectList(mylist.Users, "UserID", "Name", mylist.Users.OrderByDescending(x => x.UserID).Where(y => y.IsDeleted == false).First().UserID);
                ViewBag.VBRolesList = new SelectList(mylist.Roles, "RoleID", "RoleName");

                return View();

            }
        }

        public ActionResult Users(int? p, string filter)
        {
            if (p == null)
                p = 1;

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //Kullanıcıları alıyoruz
                using (var db = new CepNotEntities())
                {
                    //Filter
                    IQueryable<Users> query = null;
                    if (string.IsNullOrEmpty(filter))
                    {
                        query = db.Users.Where(x => 1 == 1 && x.IsDeleted == false);
                    }
                    else
                    {
                        query = db.Users.Where(x => x.IsDeleted == false && (x.Name.Contains(filter)));
                    }

                    mylist.Users = query.OrderByDescending(x => x.UserID).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
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

                }
                return View(mylist);
            }

        }

        public ActionResult UserAdder(Users user)
        {
            Users useradd = new Users();
            using (var db = new CepNotEntities())
            {

                useradd.Name = user.Name;
                useradd.Age = null;
                useradd.Gender = user.Gender;
                useradd.Mail = user.Mail;
                useradd.Password = user.Name;
                useradd.IsDeleted = false;
                useradd.Role = user.Role;

                db.Users.Add(useradd);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserDetail(int id)
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

        public ActionResult UserEdit(int id)
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

        public ActionResult UserUpdate(Users user)
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
                userupdate.Role = user.Role;
                userupdate.IsDeleted = user.IsDeleted;

                db.SaveChanges();
            }
            return RedirectToAction("UserDetail", new { id = user.UserID });

        }

        public ActionResult UserDelete(Users user)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Users userdelete = null;

                using (var db = new CepNotEntities())
                {
                    userdelete = db.Users.Where(d => d.UserID == user.UserID).First();
                    userdelete.Name = user.Name;
                    userdelete.Age = user.Age;
                    userdelete.Gender = user.Gender;
                    userdelete.Mail = user.Mail;
                    userdelete.Password = user.Password;
                    userdelete.Role = user.Role;
                    userdelete.IsDeleted = true;

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }


        public ActionResult Tasks(int? p, string filter)
        {
            if (p == null)
                p = 1;

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                //Görevleri alıyoruz
                using (var db = new CepNotEntities())
                {
                    //Filter
                    IQueryable<Tasks> query = null;
                    if (string.IsNullOrEmpty(filter))
                    {
                        query = db.Tasks.Where(x => 1 == 1);
                    }
                    else
                    {
                        query = db.Tasks.Where(x => x.Title.Contains(filter));
                    }

                    mylist.Tasks = query.OrderByDescending(x => x.TaskID).Skip(defaultPageSize * (p.Value - 1)).Take(defaultPageSize).ToList();
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
                }
                return View(mylist);

            }


        }

        public ActionResult TaskAdder(Tasks task)
        {
            Tasks taskadd = new Tasks();
            using (var db = new CepNotEntities())
            {

                taskadd.Title = task.Title;
                taskadd.Description = task.Description;
                taskadd.Date = task.Date;
                taskadd.Type = task.Type;
                taskadd.CreatedDate = task.CreatedDate;
                taskadd.IsActive = true;
                taskadd.IsDeleted = false;
                taskadd.SId = task.SId;
                taskadd.AId = task.AId;

                db.Tasks.Add(taskadd);
                db.SaveChanges();
            }
            return RedirectToAction("Tasks", "Home");

        }

        public ActionResult TaskDetail(int id)
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

        public ActionResult TaskEdit(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Tasks taskedit = null;

                using (var db = new CepNotEntities())
                {
                    taskedit = db.Tasks.Where(d => d.TaskID == id).First();
                }
                return View(taskedit);
            }
        }

        public ActionResult TaskUpdate(Tasks task)
        {
            Tasks taskupdate = null;

            using (var db = new CepNotEntities())
            {
                taskupdate = db.Tasks.Where(d => d.TaskID == task.TaskID).First();
                taskupdate.Title = task.Title;
                taskupdate.Description = task.Description;
                taskupdate.Date = task.Date;
                taskupdate.Type = task.Type;
                taskupdate.CreatedDate = task.CreatedDate;
                taskupdate.IsActive = task.IsActive;
                taskupdate.IsDeleted = task.IsDeleted;
                taskupdate.SId = task.SId;
                taskupdate.AId = task.AId;

                db.SaveChanges();
            }
            return RedirectToAction("TaskDetail", new { id = task.TaskID });

        }

        public ActionResult TaskDelete(Tasks task)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Tasks taskdelete = null;

                using (var db = new CepNotEntities())
                {
                    taskdelete = db.Tasks.Where(d => d.TaskID == task.TaskID).First();
                    taskdelete.Title = task.Title;
                    taskdelete.Description = task.Description;
                    taskdelete.Date = task.Date;
                    taskdelete.Type = task.Type;
                    taskdelete.CreatedDate = task.CreatedDate;
                    taskdelete.SId = task.SId;
                    taskdelete.AId = task.AId;
                    taskdelete.IsActive = task.IsActive;
                    taskdelete.IsDeleted = true;

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
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

        public ActionResult TaskManager()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    using (var db = new CepNotEntities())
                    {
                        mylist.Tags = db.Tags.ToList();
                        mylist.Users = db.Users.Where(x => x.Role == 3).ToList();
                    }

                    ViewBag.VBTagsList = new SelectList(mylist.Tags, "TagID", "Title");
                    ViewBag.VBSList = new SelectList(mylist.Users, "UserID", "Name", mylist.Users.OrderByDescending(x => x.UserID).Where(y => y.IsDeleted == false).First().UserID);

                }
                catch (Exception ex)
                {

                    throw;
                }
                return View();

            }
        }

        public JsonResult TaskManSave(TaskManagerDetail taskManagerDetail)
        {
            ReturnValue ret = new ReturnValue();
            Tasks taskadd = new Tasks();

            if (Shared.CheckSession() == false)
            {
                ret.requiredLogin = true;
                ret.message = "Lütfen giriş yapınız.";
                ret.success = false;
                return Json(ret);
            }

            try
            {
                using (var db = new CepNotEntities())
                {

                    if (taskManagerDetail.TaskID != 0)
                    {
                        taskadd = db.Tasks.Where(x => x.TaskID == taskManagerDetail.TaskID).FirstOrDefault();
                    }

                    
                    taskadd.AId = taskManagerDetail.AId;
                    taskadd.SId = taskManagerDetail.SId;
                    taskadd.Title = taskManagerDetail.Title;
                    taskadd.Description = taskManagerDetail.Description;
                    taskadd.Date = taskManagerDetail.Date;
                    taskadd.Type = taskManagerDetail.Type;
                    taskadd.CreatedDate = taskManagerDetail.CreatedDate;
                    taskadd.CreatedUser = taskManagerDetail.CreatedUser;
                    taskadd.IsActive = taskManagerDetail.IsActive;
                    taskadd.IsDeleted = taskManagerDetail.IsDeleted;
                    

                    if (taskManagerDetail.TaskID == 0)
                    {
                        db.Tasks.Add(taskadd);
                    }

                    db.SaveChanges();
                    ret.retObject = taskManagerDetail;
                }
                ret.message = "Başarıyla kaydedildi.";
                ret.success = true;
            }
            catch (Exception ex)
            {
                ex.AddToDBLog("HomeController.AddTaskMan");
                ret.success = false;
                ret.message = ex.Message;
            }

            return Json(ret);
        }

        public JsonResult GetUserList()
        {
            ReturnValue ret = new ReturnValue();

            using (var db = new CepNotEntities())
            {
                var userdata = db.Users.Select(x => new { x.Name }).ToList();
                ret.retObject = userdata;
            }

            return Json(ret);
        } // kullanılmıyor şu an

        public JsonResult TaskManGet(DateTime dateId)
        {
            ReturnValue ret = new ReturnValue();
            TaskManagerDetail taskManDetail = new TaskManagerDetail();

            if (Shared.CheckSession() == false)
            {
                ret.requiredLogin = true;
                ret.message = "Lütfen giriş yapınız.";
                ret.success = false;
                return Json(ret);
            }

            try
            {
                using (var db = new CepNotEntities())
                {
                    var TaskObject = db.Tasks.Select(x => new
                    {
                        x.TaskID,
                        x.Title,
                        x.Description,
                        x.Date,
                        x.Type
                    }).Where(x=>x.Date == dateId).FirstOrDefault();

                    taskManDetail.TaskID = TaskObject.TaskID;
                    taskManDetail.Title = TaskObject.Title;
                    taskManDetail.Description = TaskObject.Description;
                    taskManDetail.Date = TaskObject.Date;
                    taskManDetail.Type = TaskObject.Type;
                }

                ret.retObject = taskManDetail;
                return Json(taskManDetail);

            }
            catch (Exception ex)
            {
                ex.AddToDBLog("HomeController.GetTaskMan");
                ret.success = false;
                ret.message = ex.Message;
            }

            ret.retObject = taskManDetail;
            return Json(ret);
        }
    }
}