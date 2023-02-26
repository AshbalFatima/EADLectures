using BSCSF19MVCCore.Models;
using BSCSMVCCore.Models;
using BSCSMVCCore.Models.DbServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BSCSMVCCore.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoTask _todoTaskService;
        private readonly IWebHostEnvironment _env;

        public TodoController(ITodoTask todoTaskService, IWebHostEnvironment env)
        {
            _todoTaskService = todoTaskService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var list =await _todoTaskService.GetTasks();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var temp = new List<TodoTaskStatus>() {
                new TodoTaskStatus(){ID=1,StatusText="Completed" },
                new TodoTaskStatus(){ID=2,StatusText="Canceled" },
                new TodoTaskStatus(){ID=3,StatusText="UnderProcess" },
            };
           var lst  = temp.Select(t =>
            new SelectListItem(t.StatusText, t.ID.ToString())).ToList();
            ViewBag.Statuses = lst;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ToDoTask task,IFormFile? _Path)
        {
            if (ModelState.IsValid)
            {
                if (_Path != null)
                {

                    var helper = new FileHelper(_env);
                    var tempPath = helper.ImageUpload(_Path);
                    if (tempPath == null)
                    {
                        task.Path = tempPath;
                        _todoTaskService.Insert(task);
                        return RedirectToAction(nameof(Index));
                    }
                    else {
                        ModelState.AddModelError("Path", "Invalid File Type");

                    }

                }
                else {
                    ModelState.AddModelError("Path", "Please select an Image");
                }
            }
            var temp = new List<TodoTaskStatus>() {
                new TodoTaskStatus(){ID=1,StatusText="Completed" },
                new TodoTaskStatus(){ID=2,StatusText="Canceled" },
                new TodoTaskStatus(){ID=3,StatusText="UnderProcess" },
            };
            var lst = temp.Select(t =>
             new SelectListItem(t.StatusText, t.ID.ToString())).ToList();
            ViewBag.Statuses = lst;
            return View();  
        }
        public IActionResult Delete(int id)
        {
            _todoTaskService.Delete(id);
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ToDoTask task = _todoTaskService.GetTask(id);
            return View(task);
        }
        [HttpPost]
        public IActionResult Edit(ToDoTask task)
        {
            _todoTaskService.Update(task);
            return View();
        }

    }   
}
