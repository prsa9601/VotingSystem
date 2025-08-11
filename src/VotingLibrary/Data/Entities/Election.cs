namespace VotingLibrary.Data.Entities
{
    public class Election : BaseClass
    {
        //public Guid Id { get; private set; }
        public string Title { get; set; }
        public DateTime EndTime { get; private set; }
        public DateTime StartTime { get; private set; }
        public bool IsActive { get; set; } = true;
        public List<Guid> CandidateId { get; set; }
        public List<Guid> UsersId { get; set; }
        public List<Guid> VotesId { get; set; }
        public string? StarTimeHangfireId { get; set; }
        public string? EndTimeHangfireId { get; set; }

        private Election()
        {

        }
        public Election(DateTime endTime, DateTime startTime, string title)
        {
            //Id = Guid.NewGuid();
            VotesId = new List<Guid>();
            CandidateId = new List<Guid>();
            UsersId = new List<Guid>();
            Title = title;
            EndTime = endTime;
            StartTime = startTime;
        }
        public void ChangeIsActiveVisibility() => IsActive = true ? IsActive = false : IsActive = true;

        public void Edit(DateTime endTime, DateTime startTime, string title)
        {
            Title = title;
            EndTime = endTime;
            StartTime = startTime;
        }
        public void AddVotes(List<Guid> votesId)
        {
            VotesId.AddRange(votesId);
        }
        public void AddVotes(Guid voteId)
        {
            VotesId.Add(voteId);
        }
        public void AddUsers(List<Guid> Ids)
        {
            UsersId.AddRange(Ids);
        }
        public void AddUsers(Guid Id)
        {
            UsersId.Add(Id);
        }
        public void ClearUsers(List<Guid> id)
        {
            foreach (var item in id)
            {
                UsersId.Remove(item);
            }
        }
        public void ClearVotes(List<Guid> id)
        {
            foreach (var item in id)
            {
                VotesId.Remove(item);
            }
        }
        public void AddCandidate(List<Guid> candidatesId)
        {
            foreach (var item in candidatesId)
            {
                if (!candidatesId.Any(i => i.Equals(item)))
                {
                    CandidateId.Add(item);
                }

            }
        }
        public void AddCandidate(Guid candidateId)
        {
            CandidateId.Add(candidateId);
        }
        public void ClearCandidate(List<Guid> id)
        {
            foreach (var item in id)
            {
                CandidateId.Remove(item);
            }
        }
        public List<Guid> GetCandidatesForEdit(List<Guid> id)
        {
            var candidate = CandidateId;
            foreach (var item in id)
            {
                candidate.Remove(item);
            }
            return candidate;
        }
        public List<Guid> GetVotesForEdit(List<Guid> id)
        {
            var votes = VotesId;
            foreach (var item in id)
            {
                votes.Remove(item);
            }
            return votes;
        }

    }
}
