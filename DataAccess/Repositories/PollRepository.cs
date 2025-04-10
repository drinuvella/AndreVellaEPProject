﻿using DataAccess.DataContext;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class PollRepository:IPollRepository
    {
        private PollDbContext _PollDbContext;
        public PollRepository(PollDbContext PollDbContext)
        {
            _PollDbContext = PollDbContext;
        }
        
        public IQueryable<Poll> GetPolls()
        {
            return _PollDbContext.Polls;
        }

        public void CreatePoll(Poll poll)
        {
            _PollDbContext.Polls.Add(poll);
            _PollDbContext.SaveChanges();
        }

        public void Vote(Guid id, string selectedOption)
        {
            Poll poll = _PollDbContext.Polls.FirstOrDefault(p => p.id == id);

            if (selectedOption.Equals(poll.Option1Text))
                poll.Option1VotesCount++;

            if (selectedOption.Equals(poll.Option2Text))
                poll.Option2VotesCount++;

            if (selectedOption.Equals(poll.Option3Text))
                poll.Option3VotesCount++;

            _PollDbContext.SaveChanges();
        }
    }
}
