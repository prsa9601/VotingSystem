using VotingLibrary.Core.Common;
using VotingLibrary.Core.Services.Services.DTOs;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Core.Services.Interfaces
{
    public interface IElectionService
    {
        Task<OperationResult<Guid>> Create(DateTime endTime, DateTime startTime, string title);
        Task<OperationResult> Edit(List<Guid> candidatesId, Guid electionId, DateTime endTime, DateTime starTime, string title);
        Task<OperationResult> AddVote(Guid electionId, Guid voteId);
        Task<OperationResult> Delete(Guid electionId);
        Task<OperationResult> AddCandidate(Guid electionId, Guid candidateId);
        Task<OperationResult> AddUser(Guid electionId, Guid userId);
        Task<OperationResult> ChangeIsActiveVisibility(Guid electionId);
        Task<Election> GetId(Guid electionId);
        Task<ElectionFilterResult> GetFilter(ElectionFilterParam param);

    }
}
