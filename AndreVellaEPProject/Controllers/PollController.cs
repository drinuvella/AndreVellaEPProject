using DataAccess.Repositories;
using Domain.Models;
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Poll poll)
        {
            if (!ModelState.IsValid)
                return View(poll);

            poll.id = Guid.NewGuid();
            poll.DateCreated = DateTime.Now;
            _pollRepository.CreatePoll(poll);
            return RedirectToAction("Index");
        }
    }
}
