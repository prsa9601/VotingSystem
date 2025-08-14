using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.NewFolder;
using VotingLibrary.Core.Services.Services.DTOs;

namespace voting.Pages
{
    public class ResultModel : PageModel
    {
        private readonly IElectionService _service;

        public ResultModel(IElectionService service)
        {
            _service = service;
        }

        public ElectionModelForResultPage Result { get; set; } = new();
        public async Task<IActionResult> OnGet(Guid electionId)
        {
            Result = await _service.GetIdForResultPage(electionId);
            return Page();
        }
        // در کلاس مدل یا ViewModel
        public string GetVotePercentage(int candidateIndex)
        {
            var candidate = Result.Candidates.FirstOrDefault(i => i.Index == candidateIndex);
            if (candidate == null || Result.AllVotes == 0)
                return "0%";

            double percentage = (100.0 * candidate.Votes) / Result.AllVotes;
            return $"{percentage:N1}%";
        }
    }
}
