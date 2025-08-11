using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services.DTOs;

namespace voting.Pages.Admin.User
{
    public class ListModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IUserService _electionService;
        private readonly IUserService _voteService;

        public ListModel(IUserService userService, IUserService electionService, IUserService voteService)
        {
            _userService = userService;
            _electionService = electionService;
            _voteService = voteService;
        }
        public UserFilterParam filterParam { get; set; } = new UserFilterParam { PageId = 1, Take=10 };
        public UserFilterResult? Result { get; set; }
        public async Task OnGet()
        {
            Result = await _userService.GetFilterForAdmin(new UserFilterParam
            {
                PageId = filterParam.PageId,
                Take = filterParam.Take
            });
        }
        public void OnPost()
        {
        }
        public void OnPostDelete()
        {
        }
    }
}
