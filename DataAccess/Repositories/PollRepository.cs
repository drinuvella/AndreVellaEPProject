using DataAccess.DataContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollRepository
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
    }
}
