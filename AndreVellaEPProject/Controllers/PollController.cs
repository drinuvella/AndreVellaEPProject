using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private IPollRepository _pollRepository;
        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _pollRepository.GetPolls().OrderByDescending(p=> p.DateCreated);
            return View(list);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            Poll poll = _pollRepository.GetPolls().FirstOrDefault(p => p.id == id);
            if(poll == null)
                return RedirectToAction("Index");

            return View(poll);
        }

        [HttpPost]
        public IActionResult Vote(Guid id, string selectedOption)
        {
            if (selectedOption == null)
            {
                TempData["error"] = "Please vote";
                return RedirectToAction("Details", new { id = id });
            }

            _pollRepository.Vote(id, selectedOption);

            return RedirectToAction("Index");
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
