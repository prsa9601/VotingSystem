using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using voting.Util;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Data.Entities;

namespace voting.Pages
{
    public class VotingFormModel : PageModel
    {
        private readonly ILogger<VotingFormModel> _logger;
        private readonly IUserService _service;
        private readonly IVoteService _voteService;
        private readonly ICandidateService _candidateService;
        private readonly IElectionService _electionService;

        public VotingFormModel(IUserService service, IVoteService voteService, ILogger<VotingFormModel> logger, IElectionService electionService, ICandidateService candidateService)
        {
            _service = service;
            _voteService = voteService;
            _logger = logger;
            _electionService = electionService;
            _candidateService = candidateService;
        }

        public List<Candidate>? Candidates { get; set; }
        public Election Election { get; set; }
        public async Task<IActionResult> OnGet(Guid electionId)
        {
            Election = await _electionService.GetId(electionId);
            Candidates = await _candidateService.GetList(electionId);
            return Page();
        }
        public async Task<IActionResult> OnPost(string phoneNumber, string fullName, Guid electionId, List<Guid> candidates)
        {
            var election = await _electionService.GetId(electionId);
            if (election == null)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "خطایی رخ داده لطفا صفحه رو رفرش کنید."
                });
            }
            if (election.EndTime <= DateTime.Now)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "زمان رای گیری تمام شده."
                });
            }
            var user = await _service.GetPhoneNumber
                (phoneNumber.StartsWith("0") ? phoneNumber : $"0{phoneNumber}", electionId);

            if (user == null)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "شما حق رای ندارید."
                });
            }
            if (candidates.Count == 0)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "عملیات غیر مجاز."
                });
            }
            bool checkVote = VoteChecker.CheckVote(user.VoteAccessNumber, candidates);
            if (!checkVote)
            {
                return new JsonResult(new
                {
                    message = "عملیات غیر مجاز",
                    success = false,
                    description = $"شما حق  {user.VoteAccessNumber * 3} و {user.VoteAccessNumber}  رای تکراری دارید"

                });
            }
            var isExist = _voteService.CheckVote(user.Id, electionId);
            if (isExist == true)
            {
                return new JsonResult(new
                {
                    message = "عملیات غیر مجاز",
                    success = false,
                    description = "شما یکبار رای داده اید",
                });
            }
            await _service.SetFullName(user.Id, fullName);
            foreach (var item in candidates)
            {
                var voteResult = await _voteService.Create(user.VoteAccessNumber, user.Id, item, false, electionId);
                await _candidateService.AddVote(item, voteResult.Data);
                await _electionService.AddVote(electionId, voteResult.Data);
                await _electionService.AddUser(electionId, user.Id);
                await _service.SetVote(user.Id, voteResult.Data);
            }

            return new JsonResult(new
            {
                success = true,
                message = "رای شما با موفقیت ثبت شد."
            });
        }
    }
}
