using VotingLibrary.Core.Common;
using VotingLibrary.Core.Services.Services.DTOs;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Core.Services.Interfaces
{
    public interface IVoteService
    {
        Task<OperationResult<Guid>> Create(int voteNumber, Guid userId, Guid candidateId, bool isAdminVote, Guid election);
        Task<OperationResult> Edit(Guid voteId, int voteNumber, Guid userId, Guid candidateId, bool isAdminVote, Guid election);
        Task<OperationResult?> AddCandidateId(Guid voteId, Guid candidateId);
        Task<OperationResult?> AddElectionId(Guid voteId, Guid electionId);
        bool CheckVote(Guid userId, Guid electionId);
        Task<Vote?> GetId(Guid voteId);
        Task<Vote?> GetByCandidateIdAndElectionId(Guid candidateId, Guid electionId);
        Task<VoteFilterResult> GetFilter(VoteFilterParam param);

    }
}
