using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using voting.ViewModel.Election;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Data.Entities;

namespace voting.Pages.Admin.User
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
        [BindProperty]
        public int VoteAccessNumber { get; set; }
        [BindProperty]
        public string FullName { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<VotingLibrary.Data.Entities.Candidate>? Candidates { get; set; }
        [BindProperty(SupportsGet = true)]
        public ElectionViewModel Election { get; set; }
        public async Task<IActionResult> OnGet(Guid electionId)
        {
            var result = await _electionService.GetId(electionId);
            Election.CandidateId = result.CandidateId;
            Election.Title = result.Title;
            Election.StartTime = result.StartTime;
            Election.EndTime = result.EndTime;
            Election.Id = result.Id;
            Election.IsActive = result.IsActive;
            Election.VotesId = result.VotesId;
            Candidates = await _candidateService.GetList(electionId);
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _userService.GetPhoneNumber(PhoneNumber, Election.Id);
            foreach (var item in Candidates)
            {
                var voteReault = await _voteService.Create(VoteAccessNumber, user.Id, item.Id, true, Election.Id);
                //await _userService.SetVote(user.Id, voteReault.Data);
                await _electionService.AddVote(Election.Id, voteReault.Data);
                await _candidateService.AddVote(item.Id, voteReault.Data);
            }
            TempData["AlertType"] = "success";
            TempData["AlertMessage"] = $"رای شما برای شماره {user.PhoneNumber} با موفقیت ثبت شد.";
            return Page();
        }
    }
}
