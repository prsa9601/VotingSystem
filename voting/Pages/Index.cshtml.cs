using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services.DTOs;

namespace voting.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IElectionService _service;

        public IndexModel(IElectionService service)
        {
            _service = service;
        }
        [BindProperty(SupportsGet = true)]
        public ElectionFilterResult IsActiveElection { get; set; }

        [BindProperty(SupportsGet = true)]
        public ElectionFilterResult IsNotActiveElection { get; set; }

        public async Task<IActionResult> OnGet()
        {
            IsActiveElection = await _service.GetFilter(
                new VotingLibrary.Core.Services.Services.DTOs.ElectionFilterParam
                {
                    PageId = 1,
                    Take = 20,
                    IsFinished = true
                });

            IsNotActiveElection = await _service.GetFilter(
                new VotingLibrary.Core.Services.Services.DTOs.ElectionFilterParam
                {
                    PageId = 1,
                    Take = 10,
                    IsFinished = false
                });
            return Page();
        }
    }
}
