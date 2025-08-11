using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using VotingLibrary.Core.Services.Interfaces;

namespace voting.Pages.Admin.Vote
{
    public class IndexModel : PageModel
    {
        private readonly IVoteService _voteService;
        private readonly IUserService _userService;
        private readonly IElectionService _electionService;
        private readonly ICandidateService _candidateService;

        public IndexModel(IVoteService voteService, IUserService userService, IElectionService electionService, ICandidateService candidateService)
        {
            _voteService = voteService;
            _userService = userService;
            _electionService = electionService;
            _candidateService = candidateService;
        }

        public async Task OnGet(string phoneNumber)
        {
            //var election = await _electionService.GetId(Guid.NewGuid());

        } 
        public void OnPost()
        {
        } 
    }
}
