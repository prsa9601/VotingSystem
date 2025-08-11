using VotingLibrary.Core.Common;
using VotingLibrary.Core.Services.Services.DTOs;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Core.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<OperationResult<Guid>> Create(string phoneNumber, string fullName);
        Task<OperationResult> Edit(Guid candidateId, string fullName, string phoneNumber);
        Task<OperationResult> EditPhoneNumber(Guid candidateId, string phoneNumber);
        Task<OperationResult> AddVote(Guid candidateId , Guid voteId);
        Task<OperationResult> AddElection(Guid candidateId, Guid electionId);
        Task<OperationResult> ChangeIsWinVisibility(Guid candidateId);
        Task<Candidate?> GetId(Guid candidateId);
        Task<List<Candidate>?> GetList(Guid electionId);
        Task<CandidateFilterResult> GetFilter(CandidateFilterParam param);

    }
}
