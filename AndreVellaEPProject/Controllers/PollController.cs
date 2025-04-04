using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
