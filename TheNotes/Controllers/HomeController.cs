using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TheNotes.Models;

namespace TheNotes.Controllers
{
      
    public class HomeController : Controller
    {
        

        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]

        public ActionResult Login(Table model)
        {
            using (var context = new TheNotesEntities1())
            {
                var data = context.Tables.Where(x => x.mobile == model.mobile && x.pass == model.pass).SingleOrDefault();
                if (data == null)
                {


                    return View("Login", model);
                }
                else
                {
                    Session["name"] = data.name;
                    Session["uid"] = data.uid;

                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Rejister()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Rejister(Table model)
        {
            using (var context = new TheNotesEntities1())
            {
                var data = context.Tables.Where(x => x.mobile == model.mobile && x.pass == model.pass).SingleOrDefault();
                if(data != null)
                {
                    return RedirectToAction("Rejister");

                }
                context.Tables.Add(model);
                context.SaveChanges();
                
                Session["name"] = model.name;
                Session["uid"] = model.uid;
                return RedirectToAction("Index");

            }
        }
        public ActionResult Index()
        {
            using (var context = new TheNotesEntities1())
            {
                var data = context.Notes.Where(x => x.uid == 1 ).ToList();

                return View(data);
            }
        }
        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Create(Note model)
        {
            using (var context = new TheNotesEntities1())
            {
                model.uid = 1;
                context.Notes.Add(model);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult About() { 
        
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Edit(int Id)
        {
            using (var context = new TheNotesEntities1())
            {
                var data = context.Notes.Where(x => x.nid == Id).SingleOrDefault();
                if (data == null)
                {
                    return View("Error");
                }
                else
                {
                    return View(data);
                }
            }

        }

        [HttpPost]
        public ActionResult Edit(int nid, Note model)
        {
            using (var context = new TheNotesEntities1())
            {
                var data = context.Notes.Where(x => x.nid == nid).SingleOrDefault();

                data.tittle = model.tittle;
                data.desc = model.desc;
                data.uid = 1;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(int id)
        {
            using (var context = new TheNotesEntities1())
            {
                var data = context.Notes.Where(x => x.nid == id).SingleOrDefault();

                context.Notes.Remove(data);

                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult LogOut()
        {
            Session.Clear();
           return RedirectToAction("Login");
            
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}