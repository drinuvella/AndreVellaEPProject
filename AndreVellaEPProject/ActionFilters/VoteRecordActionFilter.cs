using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters
{
    public class VoteRecordActionFilter: ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            //Checks if it s a valid vote by checking if the vote was cast or not
            if (context.HttpContext.Request.Form.Keys.Count() == 0)
                return;

            VoteRecordRepository voteRecordRepository = context.HttpContext.RequestServices.GetService<VoteRecordRepository>();

            Guid pollId = new Guid(context.HttpContext.Request.Path.Value.Split('/')[3]);
            string ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            string email="";

            VoteRecord vote = null;

            //Check for logged in users
            if (context.HttpContext.User != null && context.HttpContext.User.Identity.IsAuthenticated)
            {
                email = context.HttpContext.User.Identity.Name;

                vote = voteRecordRepository.GetVoteRecords().FirstOrDefault(vr => 
                    vr.PollId == pollId && 
                    vr.Email.Equals(email));
            }
            //Check for anonymous users
            else
                vote = voteRecordRepository.GetVoteRecords().FirstOrDefault(vr => 
                    vr.PollId == pollId && 
                    vr.Email.Length<1 && 
                    vr.IpAddress.Equals(ipAddress));
            
            //If they have voted doesn t getting saved
            if(vote!=null)
                return;

            vote = new VoteRecord();
            vote.Email = email;
            vote.IpAddress = ipAddress;
            vote.PollId = pollId;

            voteRecordRepository.CreateLog(vote);
        }
    }
}
