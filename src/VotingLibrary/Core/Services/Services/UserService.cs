using Microsoft.EntityFrameworkCore;
using VotingLibrary.Core.Common;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services.DTOs;
using VotingLibrary.Data.Entities;
using VotingLibrary.Data.Entities.Repository;
using VotingLibrary.Data.Persistent.Ef;

namespace VotingLibrary.Core.Services.Comands
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly Context _context;

        public UserService(IUserRepository repository, Context context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<OperationResult> ChangeAccessVoteVisibility(Guid userId)
        {
            var user = await _repository.GetTracking(userId);
            if (user == null)
            {
                return OperationResult.NotFound();
            }
            user.ChangeAccessVoteVisibility();
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult<Guid>> Create(string phoneNumber, int voteAccessNumber, string fullName)
        {
            //    if (_context.Users.Any(i => i.PhoneNumber.Equals(phoneNumber)))
            //    {
            //        var userExist = await _repository.GetByFilterAsync(i => i.PhoneNumber.Equals(phoneNumber));
            //        return OperationResult<Guid>.Success(userExist!.Id);
            //    }

            var user = new User(phoneNumber, voteAccessNumber);
            user.SetFullName(fullName);
            await _repository.AddAsync(user);
            await _repository.Save();
            return OperationResult<Guid>.Success(user.Id);
        }

        public async Task<OperationResult> Edit(Guid userId, string phoneNumber, int voteAccessNumber)
        {
            var users = _context.Users;
            var user = await _repository.GetTracking(userId);
            //if (user == null)
            //{
            //    return OperationResult.NotFound();
            //}
            users.Remove(user);
            if (users.Any(i => i.PhoneNumber.Equals(phoneNumber)))
            {
                return OperationResult.Error("کاربری با این مشحصات ثبت شده.");
            }

            user.SetVoteAccessNumber(voteAccessNumber);
            user.SetPhoneNumber(phoneNumber);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<UserFilterResult> GetFilterForAdmin(UserFilterParam param)
        {
            var result = _context.Users.AsQueryable();
            if (param.FullName != null)
            {
                result = _context.Users.Where(i => i.FullName.Equals(param.FullName));

            }
            if (param.PhoneNumber != null)
            {
                result = _context.Users.Where(i => i.PhoneNumber.Equals(param.PhoneNumber));
            }

            var skip = (param.PageId - 1) * param.Take;
            var data = new UserFilterResult()
            {
                Data = await result.Skip(skip).Take(param.Take).ToListAsync()
                //.Select(n => new VoteFilterResult
                //{

                //}).ToListAsync()
            };
            data.GeneratePaging(result, param.Take, param.PageId);
            return data;
        }

        public async Task<User?> GetId(Guid userId)
        {
            var user = await _repository.GetTracking(userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User?> GetPhoneNumber(string phoneNumber, Guid electionId, string fullName)
        {
            var user = _context.Users.Where(i => i.PhoneNumber == phoneNumber.Trim()&& i.FullName.Equals(fullName)).ToList();
            var election = await _context.Elections.FirstOrDefaultAsync(i => i.Id.Equals(electionId));
            User result = new User();
            foreach (var item in election.UsersId)
            {
                result = user.FirstOrDefault(i => i.Id.Equals(item));
                if (result != null)
                {
                    break;
                }
            }
            if (user == null)
            {
                return null;
            }
            return result;
        }

        public Task<OperationResult> Login(string fullName, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Register(string fullName, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> SetRole(Guid userId, Role role)
        {
            var user = await _repository.GetTracking(userId);
            if (user == null)
            {
                return OperationResult.NotFound();
            }
            user.SetRole(role);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult> SetVote(Guid userId, Guid voteId)
        {
            var user = await _repository.GetTracking(userId);
            if (user == null)
            {
                return OperationResult.NotFound();
            }
            user.AddVote(voteId);
            await _repository.Save();
            return OperationResult.Success();
        }
        public async Task<OperationResult> SetFullName(Guid userId, string fullName)
        {
            var user = await _repository.GetTracking(userId);
            if (user == null)
            {
                return OperationResult.NotFound();
            }
            user.SetFullName(fullName);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<UserFilterResult> GetFilterForCandidateAdmin(UserFilterForCandidateParam param)
        {
            var result = _context.Users.AsQueryable();
            var user = new List<User>();
            //var election = _context.Elections.FirstOrDefaultAsync(i => i.Id.Equals(param.ElectionId));

            //var vote = _context.Votes.Where(i => i.ElectionId.Equals(param.ElectionId)
            //&& i.CandidateId.Equals(param.CandidateId));


            if (param.VoteIds != null || param.VoteIds.Count > 0)
            {
                foreach (var item in param.VoteIds)
                {
                    user.AddRange(_context.Users.Where(i => i.VotesId.Any(x => x.Equals(item))));
                }

            }
            user.ForEach(i => i.VoteAccessNumber = 0);
            List<User> res = new();
            foreach (var item in user)
            {
                if (res.Any(i => i.Id.Equals(item.Id)))
                {
                    var q = res.FirstOrDefault(i => i.Id.Equals(item.Id));
                    q.VoteAccessNumber++;
                    //res(q);
                }
                else
                {
                    item.VoteAccessNumber++;
                    res.Add(item);
                }
            }

            var skip = (param.PageId - 1) * param.Take;
            var data = new UserFilterResult()
            {
                Data = res.AsQueryable().Skip(skip).Take(param.Take).ToList()
                //.Select(n => new VoteFilterResult
                //{

                //}).ToListAsync()
            };
            data.GeneratePaging(res.AsQueryable(), param.Take, param.PageId);
            return data;
        }

        public async Task<OperationResult> RemoveVote(Guid userId, Guid candidateId, Guid electionId)
        {
            var user = await _repository.GetTracking(userId);
            if (user == null)
            {
                return OperationResult.NotFound();
            }

            var votes = _context.Votes.Where(i => i.UserId.Equals(userId)
            && i.ElectionId.Equals(electionId) && i.CandidateId.Equals(candidateId));

            user.ClearVote(votes.Select(i => i.Id).ToList());
            _context.SaveChanges();
            return OperationResult.Success();
        }
    }
}
