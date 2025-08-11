using Hangfire;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VotingLibrary.Core.Common;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services.DTOs;
using VotingLibrary.Data.Entities;
using VotingLibrary.Data.Entities.Repository;
using VotingLibrary.Data.Persistent.Ef;

namespace VotingLibrary.Core.Services.Services
{
    public class ElectionService : IElectionService
    {
        private readonly IElectionRepository _repository;
        private readonly Context _context;

        public ElectionService(IElectionRepository repository, Context context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<OperationResult> AddCandidate(Guid electionId, Guid candidateId)
        {
            var election = await _repository.GetTracking(electionId);
            if (election == null)
            {
                return OperationResult.NotFound();
            }
            election.AddCandidate(candidateId);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult> AddVote(Guid electionId, Guid voteId)
        {
            var election = await _repository.GetTracking(electionId);
            if (election == null)
            {
                return OperationResult.NotFound();
            }
            election.AddVotes(voteId);
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult> ChangeIsActiveVisibility(Guid electionId)
        {
            var election = await _repository.GetTracking(electionId);
            if (election == null)
            {
                return OperationResult.NotFound();
            }
            election.ChangeIsActiveVisibility();
            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<OperationResult<Guid>> Create(DateTime endTime, DateTime startTime, string title)
        {
            if (_context.Elections.Any(i => i.Title == title))
            {
                return OperationResult<Guid>.Error("شما یکبار با این عنوان انتخابات ثبت کرده اید.");
            }
            var election = new Election(endTime, startTime, title);
            await _repository.AddAsync(election);
            if (startTime > DateTime.Now)
            {
                election.StarTimeHangfireId = BackgroundJob.Schedule(() => ActivateElection(election.Id), election.StartTime);
                election.IsActive = false;
            }
            else
            {
                election.IsActive = true;
            }
            if (endTime > DateTime.Now)
            {
                election.EndTimeHangfireId = BackgroundJob.Schedule(() => DeactivateElection(election.Id), election.EndTime);
            }
            else
            {
                election.IsActive = false;
            }

            await _repository.Save();
            return OperationResult<Guid>.Success(election.Id);
        }

        public async Task<OperationResult> Edit(List<Guid> candidatesId, Guid electionId, DateTime endTime, DateTime startTime, string title)
        {

            var election = await _repository.GetTracking(electionId);
            //_context.Candidates.RemoveRange(candidates);

            BackgroundJob.Delete(election.StarTimeHangfireId);
            BackgroundJob.Delete(election.EndTimeHangfireId);
            election.StarTimeHangfireId = BackgroundJob.Schedule(() => ActivateElection(election.Id), election.StartTime);
            election.EndTimeHangfireId = BackgroundJob.Schedule(() => DeactivateElection(election.Id), election.EndTime);

            await _repository.Save();
            return OperationResult.Success();
        }

        public async Task<ElectionFilterResult> GetFilter(ElectionFilterParam param)
        {

            var result = _context.Elections.AsQueryable();

            if (param.IsFinished == true)
            {
                result = _context.Elections.Where(i => i.IsActive == true);
            }
            else if (param.IsFinished == false)
            {
                result = _context.Elections.Where(i => i.IsActive == false);
            }

            var skip = (param.PageId - 1) * param.Take;
            var data = new ElectionFilterResult()
            {
                Data = await result.Skip(skip).Take(param.Take).ToListAsync()
                //.Select(n => new VoteFilterResult
                //{

                //}).ToListAsync()
            };
            data.GeneratePaging(result, param.Take, param.PageId);
            return data;
        }

        public async Task<Election> GetId(Guid electionId)
        {
            var election = await _repository.GetTracking(electionId);
            if (election == null)
            {
                return null;
            }
            return election;
        }
        public void ActivateElection(Guid electionId)
        {
            var election = _context.Elections.FirstOrDefault(i => i.Id.Equals(electionId));
            if (election != null)
            {
                election.IsActive = true;
                _context.SaveChanges();
            }
        }

        public void DeactivateElection(Guid electionId)
        {
            var election = _context.Elections.FirstOrDefault(i => i.Id.Equals(electionId));
            if (election != null)
            {
                election.IsActive = false;
                _context.SaveChanges();
            }
        }

        //public async Task<OperationResult> Delete(Guid electionId)
        //{
        //    var election = await _context.Elections.FirstOrDefaultAsync
        //        (i => i.Id.Equals(electionId));
        //    var candidates = _context.Candidates.Where(i => i.ElectionsId.Any(x => x.Equals(electionId)));
        //    var votes = _context.Votes.Where(i => i.ElectionId.Equals(electionId));

        //    _context.Elections.Remove(election);
        //    foreach (var item in candidates)
        //    {
        //        item.ClearElection(electionId);
        //    }
        //}
        public async Task<OperationResult> Delete(Guid electionId)
        {
            var election = await _context.Elections.FirstOrDefaultAsync
                (i => i.Id.Equals(electionId));
            var candidates = _context.Candidates.Where(i => i.ElectionsId.Any(x => x.Equals(electionId)));
            var votes = _context.Votes.Where(i => i.ElectionId.Equals(electionId));

            _context.Elections.Remove(election);
            foreach (var item in candidates)
            {
                item.ClearElection(electionId);    
            }
            
            _context.Votes.RemoveRange(votes);

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public async Task<OperationResult> AddUser(Guid electionId, Guid userId)
        {
            var election = await _repository.GetTracking(electionId);
            if (election == null)
            {
                return OperationResult.NotFound();
            }
            election.AddUsers(userId);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}

