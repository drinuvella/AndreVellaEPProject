using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private IPollRepository _pollRepository;
        private IHttpContextAccessor _httpContextAccessorRepository;
        private VoteRecordRepository _voteRecordRepository;
        public PollController(IPollRepository pollRepository, IHttpContextAccessor httpContextAccessor, VoteRecordRepository voteRecordRepository)
        {
            _pollRepository = pollRepository;
            _httpContextAccessorRepository = httpContextAccessor;
            _voteRecordRepository = voteRecordRepository;
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
        [VoteRecordActionFilter]
        public IActionResult Vote(Guid id, string selectedOption)
        {
            if (selectedOption == null)
            {
                TempData["error"] = "Please vote";
                return RedirectToAction("Details", new { id = id });
            }

            Boolean hasVoted = false;
            var user = _httpContextAccessorRepository.HttpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
                hasVoted = _voteRecordRepository.GetVoteRecords().FirstOrDefault(
                    vr => vr.PollId == id && 
                    vr.Email.Equals(user.Identity.Name)) != null;
            
            else
            {
                String ipAddress = _httpContextAccessorRepository.HttpContext.Connection.RemoteIpAddress?.ToString();
                hasVoted = _voteRecordRepository.GetVoteRecords().FirstOrDefault(
                    vr => vr.PollId == id && 
                    vr.Email.Length < 1 && 
                    vr.IpAddress.Equals(ipAddress)) != null;
            }

            if (!hasVoted) { 
                _pollRepository.Vote(id, selectedOption);
            }

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
