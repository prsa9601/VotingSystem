using Microsoft.EntityFrameworkCore;
using VotingLibrary.Core.Common;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services.DTOs;
using VotingLibrary.Data.Entities;
using VotingLibrary.Data.Persistent.Ef;
using VotingLibrary.Data.Persistent.Ef.Entities.Repository;
using static System.Collections.Specialized.BitVector32;

namespace VotingLibrary.Core.Services.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _repository;
        private readonly Context _context;
        public VoteService(IVoteRepository repository, Context context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<OperationResult?> AddCandidateId(Guid voteId, Guid candidateId)
        {
            var vote = await _repository.GetTracking(voteId);
            if (vote == null)
                return OperationResult.NotFound();

            vote.AddCandidate(candidateId);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult?> AddElectionId(Guid voteId, Guid electionId)
        {
            var vote = await _repository.GetTracking(voteId);
            if (vote == null)
                return OperationResult.NotFound();

            vote.AddCandidate(electionId);
            await _repository.Save();
            return OperationResult.Success();
        }

        public bool CheckVote(Guid userId, Guid electionId)
        {
            return _context.Votes.Any(i => i.UserId.Equals(userId) && i.ElectionId.Equals(electionId));
        }

        public async Task<OperationResult<Guid>> Create(int voteNumber, Guid userId, Guid candidateId, bool isAdminVote, Guid election)
        {
            var vote = new Vote(voteNumber, userId, candidateId, isAdminVote, election);
            await _repository.AddAsync(vote);
            await _repository.Save();
            return OperationResult<Guid>.Success(vote.Id);
        }

        public async Task<OperationResult> Edit(Guid voteId, int voteNumber, Guid userId, Guid candidateId, bool isAdminVote, Guid election)
        {
            var vote = await _repository.GetTracking(voteId);
            if (vote == null)
                return OperationResult.NotFound();

            vote.Edit(voteNumber, userId, candidateId, isAdminVote, election);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<Vote?> GetByCandidateIdAndElectionId(Guid candidateId, Guid electionId)
        {
            var vote = await _repository.GetByFilterAsync(
                i => i.CandidateId.Equals(candidateId) && i.ElectionId.Equals(electionId));

            if (vote == null)
                return null;
            return vote;
        }

        public async Task<VoteFilterResult> GetFilter(VoteFilterParam param)
        {
            var result = _context.Votes.AsQueryable();
            if (param.electionId != null && param.candidateId != null)
            {
                result = _context.Votes.Where(i => i.ElectionId.Equals(param.electionId)
                && i.CandidateId.Equals(param.candidateId));

            }

            var skip = (param.PageId - 1) * param.Take;
            var data = new VoteFilterResult()
            {
                Data = await result.Skip(skip).Take(param.Take).ToListAsync()
                //.Select(n => new VoteFilterResult
                //{

                //}).ToListAsync()
            };
            data.GeneratePaging(result, param.Take, param.PageId);
            return data;
        }

        public async Task<Vote?> GetId(Guid voteId)
        {
            var vote = await _repository.GetTracking(voteId);
            if (vote == null)
                return null;
            return vote;
        }

        public async Task<OperationResult?> RemoveVotes(Guid candidateId, Guid userId, Guid electionId)
        {
            var votes = _context.Votes.Where(i => i.UserId.Equals(userId)
             && i.ElectionId.Equals(electionId) && i.CandidateId.Equals(candidateId));

            _context.Votes.RemoveRange(votes);
            _context.SaveChanges();
            return OperationResult.Success();
        }
    }
}
