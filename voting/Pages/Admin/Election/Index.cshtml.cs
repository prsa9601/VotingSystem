using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services.DTOs;

namespace voting.Pages.Admin.Election
{
    public class IndexModel : PageModel
    {
        private readonly IElectionService _electionService;
        private readonly ICandidateService _candidateService;
        private readonly IUserService _userService;
        private readonly IVoteService _voteService;

        public IndexModel(IElectionService electionService, ICandidateService candidateService,
            IUserService userService, IVoteService voteService)
        {
            _electionService = electionService;
            _candidateService = candidateService;
            _userService = userService;
            _voteService = voteService;
        }
        public ElectionFilterResult FilterResult { get; set; }
        public ElectionFilterParam FilterParam { get; set; } = new ElectionFilterParam
        {
            IsFinished = null,
            PageId = 1,
            Take = 10
        };
        public async Task<IActionResult> OnGet(int PageId = 1, int Take = 10)
        {
            FilterResult = await _electionService.GetFilter(new ElectionFilterParam
            {
                PageId = PageId,
                Take = Take
            });
            return Page();
        }
        public async Task<IActionResult> OnPostDelete(Guid electionId)
        {
            //FilterResult = await _electionService.GetFilter(FilterParam);

            var result = await _electionService.Delete(electionId);
            return new JsonResult(new { success = result });
        }
    }
}
