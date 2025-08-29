using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using voting.Util;
using voting.ViewModel.CandidateModel;
using voting.ViewModel.UserModel;
using voting.ViewModel.VoteModel;
using VotingLibrary.Core.Common.Attribute;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Data.Entities;
using VotingLibrary.Data.Entities.Enums;

namespace voting.Pages.Admin.Election
{
    public class AddModel : PageModel
    {
        private readonly ICandidateService _candidateService;
        private readonly IVoteService _voteService;
        private readonly IElectionService _electionService;
        private readonly IUserService _userService;

        public AddModel(ICandidateService candidateService,
            IVoteService voteService, IElectionService electionService, IUserService userService)
        {
            _candidateService = candidateService;
            _voteService = voteService;
            _electionService = electionService;
            _userService = userService;
        }
        [BindProperty]
        public string ElectionTitle { get; set; }
        [BindProperty]
        public ElectionType ElectionType { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "تاریخ شروع الزامی است")]
        [RegularExpression(@"^[\u06F0-\u06F9/]{10}$", ErrorMessage = "فرمت تاریخ نامعتبر است")]
        public string StartDate { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "تاریخ پایان الزامی است")]
        [RegularExpression(@"^[\u06F0-\u06F9/]{10}$", ErrorMessage = "فرمت تاریخ نامعتبر است")]
        public string EndDate { get; set; }
        //[BindProperty]
        //public string CandidateFullName { get; set; }
        ////[BindProperty]
        ////[IranianPhoneNumber]
        //public string PhoneNumber { get; set; }

        [BindProperty]
        public List<UserViewModel>? Voters { get; set; } = new();
        [BindProperty]
        public List<VoteViewModel>? Votes { get; set; } = new();
        [BindProperty]
        public List<CandidateViewModel>? Candidates { get; set; } = new();
        //public int VoteNumber { get; set; }

        //[IranianPhoneNumber]
        //public string VoterPhoneNumber { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var start = DateConverter.ConvertPersianToMiladi(StartDate);
            var end = DateConverter.ConvertPersianToMiladi(EndDate);
            //var date = DateTime.Now.AddDays(1);
            var createElectionResult = await _electionService.Create(
                end, start
                , ElectionTitle, ElectionType);
            foreach (var item in Candidates!)
            {
                var createCandidateResult = await _candidateService.Create
                    (item.PhoneNumber, item.FullName);
                await _candidateService.AddElection(createCandidateResult.Data, createElectionResult.Data);

                await _electionService.AddCandidate
                    (createElectionResult.Data, createCandidateResult.Data);

                //var voteResult = await _voteService.AddCandidateId(createCandidateResult.Data);
            }
            foreach (var item in Voters!)
            {
                var userResult = await _userService.Create(
                     item.PhoneNumber, item.VoteAccessNumber, item.FullName);
                await _electionService.AddUser(createElectionResult.Data, userResult.Data);
            }

            TempData["AlertType"] = "success";
            TempData["AlertMessage"] = "انتخابات با موفقیت ایجاد شد";
            return RedirectToPage("Index");
        }
    }
}
//return new JsonResult(new
//{
//    Success = true,
//    Message = "انتخابات با موفقیت ایجاد شد",
//    ElectionId = createElectionResult.Data
//});
