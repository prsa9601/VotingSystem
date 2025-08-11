using VotingLibrary.Core.Common;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Core.Services.Interfaces
{
    public interface IElectionVoteService
    {
        Task<OperationResult> Create(Guid candidateId, Guid electionId);
        //OperationResult Edit();
        Task<ElectionCandidate> GetId();
        Task<List<ElectionCandidate>> GetFilter();

    }
}
