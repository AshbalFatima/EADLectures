using BSCSMVCCore.Models.DbServices;
using Microsoft.AspNetCore.Mvc;

namespace BSCSMVCCore.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoTask _todoTaskService;

        public TodoController(ITodoTask todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }

        public async Task<IActionResult> Index()
        {
            var list =await _todoTaskService.GetTasks();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
    }   
}
