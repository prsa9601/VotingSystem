using Microsoft.EntityFrameworkCore;
using VotingLibrary.Core.Common;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services.DTOs;
using VotingLibrary.Data.Entities;
using VotingLibrary.Data.Entities.Repository;
using VotingLibrary.Data.Persistent.Ef;

namespace VotingLibrary.Core.Services.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly Context _context;

        public CandidateService(ICandidateRepository repository, Context context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<OperationResult> AddElection(Guid candidateId, Guid electionId)
        {
            var candidate = await _repository.GetTracking(candidateId);
            if (candidate == null)
            {
                return OperationResult.NotFound();
            }
            candidate.AddElection(electionId);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult> AddVote(Guid candidateId, Guid voteId)
        {
            var candidate = await _repository.GetTracking(candidateId);
            if (candidate == null)
            {
                return OperationResult.NotFound();
            }
            candidate.AddVote(voteId);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult> ChangeIsWinVisibility(Guid candidateId)
        {
            var candidate = await _repository.GetTracking(candidateId);
            if (candidate == null)
            {
                return OperationResult.NotFound();
            }
            candidate.ChangeIsWinVisibility();
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult<Guid>> Create(string phoneNumber, string fullName)
        {
            if (_context.Candidates.Any(i => i.PhoneNumber.Equals(phoneNumber)))
            {
                var candide = await _repository.GetByFilterAsync(
                    i => i.PhoneNumber.Equals(phoneNumber));
                return OperationResult<Guid>.Success(candide.Id);
            }
            var candidate = new Candidate(phoneNumber);
            candidate.FullName = fullName;
            await _repository.AddAsync(candidate);
            await _repository.Save();
            return OperationResult<Guid>.Success(candidate.Id);
        }

        public async Task<OperationResult> EditPhoneNumber(Guid candidateId, string phoneNumber)
        {
            var candidate = await _repository.GetTracking(candidateId);
            if (candidate == null)
            {
                return OperationResult.NotFound();
            }
            candidate.SetPhoneNumber(phoneNumber);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult> Edit(Guid candidateId, string fullName, string phoneNumber)
        {
            var candidate = await _repository.GetTracking(candidateId);
            if (candidate == null)
            {
                return OperationResult.NotFound();
            }
            var candidates = _context.Candidates;

            candidates.Remove(candidate);
            if (candidates.Any(i => i.PhoneNumber.Equals(phoneNumber)))
            {
                OperationResult.Error("کاندیدی با این مشحصات ثبت شده.");
            }
            candidate.Edit(fullName, phoneNumber);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<Candidate?> GetId(Guid candidateId)
        {
            var candidate = await _repository.GetTracking(candidateId);
            if (candidate == null)
            {
                return null;
            }
            await _repository.Save();
            return candidate;
        }

        public async Task<List<Candidate>?> GetList(Guid electionId)
        {
            var result = await _context.Candidates.
                Where(i => i.ElectionsId.Any(x => x == electionId)).ToListAsync();
            return result;
        }
        public async Task<CandidateFilterResult> GetFilter(CandidateFilterParam param)
        {

            List<Candidate> candidate = new();
            List<Vote> vote = new();
            var candidates = _context.Candidates.AsQueryable();
            var votes = _context.Votes.OrderByDescending(i => i.Id).AsQueryable();

            if (param.electionId != null)
            {
                var election = await _context.Elections.FirstOrDefaultAsync(i => i.Id.Equals(param.electionId));
                foreach (var item in election.CandidateId)
                {
                    var tempCandidate = candidates.FirstOrDefault(i => i.Id.Equals(item));
                    var count = _context.Votes.Where(i => i.CandidateId.Equals(item)
                    && i.ElectionId.Equals(param.electionId));
                    tempCandidate.VotesId = count.Select(i => i.Id).ToList();
                    candidate.Add(tempCandidate);
                }

            }

            //if (param.PhoneNumber != null)
            //{
            //    result = _context.Users.Where(i => i.PhoneNumber.Equals(param.PhoneNumber));
            //}
            var skip = (param.PageId - 1) * param.Take;
            var data = new CandidateFilterResult()
            {
                Data = candidate.AsQueryable().Skip(skip).Take(param.Take).ToList()
                //.Select(n => new VoteFilterResult
                //{

                //}).ToListAsync()
            };
            data.GeneratePaging(candidate.AsQueryable(), param.Take, param.PageId);
            return data;
        }

        public async Task<OperationResult> RemoveVote(Guid candidateId, Guid userId, Guid electionId)
        {
            var candidate = await _repository.GetTracking(candidateId);
            if (candidate == null)
            {
                return OperationResult.NotFound();
            }

            var votes = _context.Votes.Where(i => i.UserId.Equals(userId)
            && i.ElectionId.Equals(electionId) && i.CandidateId.Equals(candidateId));

            candidate.ClearVote(votes.Select(i => i.Id).ToList());
            _context.SaveChanges();
            return OperationResult.Success();
        }
        public long GetVoteNumberOneElection(Guid candidateId, Guid electionId)
        {
            var candidate = _context.Votes.Where
                (i => i.ElectionId.Equals(electionId) && i.CandidateId.Equals(candidateId));
            if (candidate == null)
            {
                return 0;
            }
            return candidate.Count();
        }
    }
}
