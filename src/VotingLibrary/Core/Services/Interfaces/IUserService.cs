using VotingLibrary.Core.Common;
using VotingLibrary.Core.Services.Services.DTOs;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult> Login(string fullName, string phoneNumber);
        Task<OperationResult> Register(string fullName, string phoneNumber);
        Task<OperationResult<Guid>> Create(string phoneNumber, int voteAccessNumber);
        Task<OperationResult> SetVote(Guid userId, Guid voteId);
        Task<OperationResult> RemoveVote(Guid userId, Guid candidateId, Guid electionId);
        Task<OperationResult> SetFullName(Guid userId, string fullName);
        Task<OperationResult> ChangeAccessVoteVisibility(Guid userId);
        Task<OperationResult> SetRole(Guid userId, Role role);
        Task<OperationResult> Edit(Guid userId, string phoneNumber, int voteAccessNumber);
        Task<User> GetId(Guid userId);
        Task<User?> GetPhoneNumber(string phoneNumber, Guid electionId);
        Task<UserFilterResult> GetFilterForAdmin(UserFilterParam param);
        Task<UserFilterResult> GetFilterForCandidateAdmin(UserFilterForCandidateParam param);

    }
}
