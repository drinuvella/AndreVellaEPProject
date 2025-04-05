using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPollRepository
    {
        IQueryable<Poll> GetPolls();
        void CreatePoll(Poll poll);
        void Vote(Guid id, string selectedOption);
    }
}
