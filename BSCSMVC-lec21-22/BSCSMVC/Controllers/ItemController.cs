using BSCSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSCSMVC.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {

            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    db.Categories.Add(new Category()
            //    {
            //        CategoryText = "Electronics"
            //    });
            //    db.Categories.Add(new Category()
            //    {
            //        CategoryText = "Machanics"
            //    });
            //    db.SaveChanges();
            //}
            List<Item> items = new List<Item>();
            using (ApplicationDbContext db=new ApplicationDbContext())
            {
                items = db.Items.ToList();

              
            }
            return View(items);


        }
        public ActionResult Create()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            { 
             var temp = db.Categories.Select(t=>
             new SelectListItem() { Value=t.Id.ToString() , Text=t.CategoryText}
             ).ToList();
                ViewBag.Categories = temp;
            }
            return View();

        }
        [HttpPost]
        public ActionResult Create(Item model)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Items.Add(model);
                    db.SaveChanges();
                }
                return Redirect(nameof(Index));

            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext()) { 
                 var item = db.Items.Where(t=>t.Id==id).FirstOrDefault();
                if (item != null)
                {
                    var temp = db.Categories.Select(t =>
                       new SelectListItem() { Value = t.Id.ToString(), Text = t.CategoryText }
                       ).ToList();
                                ViewBag.Categories = temp;

                    return View(item);
                }else
                {
                    return Redirect(nameof(Create));
                }
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, Item model)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db=new ApplicationDbContext())
                {
                    var item = db.Items.Where(t => t.Id == id).First();
                    item.Name = model.Name; 
                    item.CategoryId = model.CategoryId;
                    //db.I(item);
                    db.SaveChanges();
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id, Object model)
        {
            return View();
        }


        //public ActionResult GetData(int id,DateTime from, DateTime to)
        //{
        //    return View();
        //}
    }
}