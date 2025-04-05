using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;

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
            if(!File.Exists(_filename))
                return new List<Poll>().AsQueryable();

            string contents = File.ReadAllText(_filename);
            
            var polls = JsonConvert.DeserializeObject<List<Poll>>(contents);

            return polls?.AsQueryable() ?? Enumerable.Empty<Poll>().AsQueryable();
        }

        public void CreatePoll(Poll poll)
        {
            var polls = GetPolls().ToList();

            polls.Add(poll);

            string contents = JsonConvert.SerializeObject(polls);

            File.WriteAllText(_filename, contents);
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

            File.WriteAllText(_filename, contents);
        }
    }
}
