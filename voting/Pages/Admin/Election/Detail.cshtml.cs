using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using voting.ViewModel.Election;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services.DTOs;
using VotingLibrary.Data.Entities;

namespace voting.Pages.Admin.Election
{
    public class DetailModel : PageModel
    {
        private readonly ICandidateService _candidateService;
        private readonly IElectionService _electionService;

        public DetailModel(ICandidateService candidateService, IElectionService electionService)
        {
            _candidateService = candidateService;
            _electionService = electionService;
        }
        [BindProperty(SupportsGet = true)]
        public CandidateFilterResult Result { get; set; }

        [BindProperty(SupportsGet = true)]
        public ElectionViewModel Election { get; set; }
        public async Task<IActionResult> OnGet(Guid electionId)
        {
            var result = await _electionService.GetId(electionId);
            Election.CandidateId = result.CandidateId;
            Election.Title = result.Title;
            Election.Id = result.Id;
            Election.StartTime = result.StartTime;
            Election.EndTime = result.EndTime;
            Election.IsActive = result.IsActive;
            Election.VotesId = result.VotesId;
            Result = await _candidateService.GetFilter(new VotingLibrary.Core.Services.Services.DTOs.CandidateFilterParam
            {
                electionId = electionId,
                PageId = 1,
                Take = 10000,

            });
            return Page();
        }
        public void OnPost()
        {
        }
        public void OnPostDelete()
        {
        }
    }
}
