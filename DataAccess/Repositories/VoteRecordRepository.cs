using DataAccess.DataContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class VoteRecordRepository
    {
        private PollDbContext _PollDbContext;

        public VoteRecordRepository(PollDbContext pollDbContext) { 
            _PollDbContext = pollDbContext;
        }

        public IQueryable<VoteRecord> GetVoteRecords()
        {
            return _PollDbContext.VoteRecords.AsQueryable();
        }

        public void CreateLog(VoteRecord log)
        {
            _PollDbContext.VoteRecords.Add(log);
            _PollDbContext.SaveChanges();
        }
    }
}
