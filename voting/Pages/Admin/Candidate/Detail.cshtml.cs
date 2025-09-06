using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using voting.ViewModel.CandidateModel;
using voting.ViewModel.Election;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services.DTOs;

namespace voting.Pages.Admin.Candidate
{
    public class DetailModel : PageModel
    {
        private readonly ICandidateService _candidateService;
        private readonly IElectionService _electionService;
        private readonly IVoteService _voteService;
        private readonly IUserService _userService;

        public DetailModel(ICandidateService candidateService, IUserService userService, IElectionService electionService, IVoteService voteService)
        {
            _candidateService = candidateService;
            _userService = userService;
            _electionService = electionService;
            _voteService = voteService;
        }
        [BindProperty]
        public Guid ElectionId { get; set; }
        public VoteFilterParam VoteFilterParam { get; set; } = new VoteFilterParam { Take = 10, PageId = 1 };

        [BindProperty]
        public UserFilterForCandidateParam FilterParam { get; set; } = new UserFilterForCandidateParam
        {
            PageId = 1,
            Take = 10
        };
        [BindProperty(SupportsGet = true)]
        public UserFilterResult FilterResult { get; set; }
        public long VotesNumber { get; set; }

        public VoteFilterResult VoteFilterResult { get; set; }

        [BindProperty]
        public CandidateViewModel Candidate { get; set; }
        [BindProperty]
        public ElectionViewModel Election { get; set; }
        public async Task<IActionResult> OnGet(Guid candidateId, Guid electionId, int PageId= 1, int Take = 10)
        {
            var candidate = await _candidateService.GetId(candidateId);
            var election = await _electionService.GetId(electionId);
            Election = new ElectionViewModel
            {
                Id = election.Id,
                CandidateId = election.CandidateId,
                EndTime = election.EndTime,
                StartTime = election.StartTime,
                IsActive = election.IsActive,
                Title=election.Title
            };
            Candidate = new CandidateViewModel
            {
                PhoneNumber = candidate.PhoneNumber,
                FullName=candidate.FullName
            };

            VoteFilterResult = await _voteService.GetFilter(new VoteFilterParam
            {
                candidateId = candidateId,
                electionId = electionId,
                PageId = 1,
                Take = 100000

            });
            FilterResult = await _userService.GetFilterForCandidateAdmin(new UserFilterForCandidateParam
            {
                PageId = PageId,
                Take = Take,
                VoteIds = VoteFilterResult.Data.Select(i => i.Id).ToList(),
            });
            VotesNumber = _candidateService.GetVoteNumberOneElection(candidateId, electionId);
            ElectionId = electionId;
            return Page();
        }
    }
}
