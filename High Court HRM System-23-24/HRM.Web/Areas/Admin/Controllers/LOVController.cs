using HRM.Repositories.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LOVController : Controller
    {
        // GET: LOVController
        private readonly ILOVsService _LOVsService;

        public LOVController(ILOVsService lOVsService)
        {
            _LOVsService = lOVsService;
        }

        public ActionResult Index()
        {
            var temp = _LOVsService.GetAll();
            return View(temp);
        }

        // GET: LOVController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LOVController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LOVController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LOVController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LOVController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LOVController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LOVController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
