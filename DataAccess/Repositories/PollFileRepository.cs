using Microsoft.Extensions.Configuration;
using Castle.Core.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using DataAccess.DataContext;
using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollFileRepository:IPollRepository
    {
        private string _filename;
        
        public PollFileRepository(IConfiguration configuration)
        {
            _filename = configuration["PollsFileName"];
        }

        public IQueryable<Poll> GetPolls()
        {
            if(!System.IO.File.Exists(_filename))
                return new List<Poll>().AsQueryable();

            string contents = System.IO.File.ReadAllText(_filename);
            
            var polls = JsonConvert.DeserializeObject<List<Poll>>(contents);

            return polls?.AsQueryable() ?? Enumerable.Empty<Poll>().AsQueryable();
        }

        public void CreatePoll(Poll poll)
        {
            var polls = GetPolls().ToList();

            polls.Add(poll);

            string contents = JsonConvert.SerializeObject(polls);

            System.IO.File.WriteAllText(_filename, contents);
        }

        public void Vote(Guid id, string selectedOption)
        {
            var polls = GetPolls().ToList();
            Poll poll = polls.FirstOrDefault(p => p.id == id);

            polls.Remove(poll);

            if (selectedOption.Equals(poll.Option1Text))
                poll.Option1VotesCount++;

            if (selectedOption.Equals(poll.Option2Text))
                poll.Option2VotesCount++;

            if (selectedOption.Equals(poll.Option3Text))
                poll.Option3VotesCount++;

            polls.Add(poll);

            string contents = JsonConvert.SerializeObject(polls);

            System.IO.File.WriteAllText(_filename, contents);
        }
    }
}
