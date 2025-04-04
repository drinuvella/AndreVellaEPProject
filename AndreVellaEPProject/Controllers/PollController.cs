using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private PollRepository _pollRepository;
        public PollController(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }
        public IActionResult Index()
        {
            var list = _pollRepository.GetPolls();
            return View(list);
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
