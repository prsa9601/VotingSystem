namespace VotingLibrary.Data.Entities
{
    public class Vote : BaseClass
    {
        public Vote(int voteNumber, Guid userId, Guid candidateId, bool isAdminVote, Guid election)
        {
            VoteNumber = voteNumber;
            UserId = userId;
            CandidateId = candidateId;
            VoteTime = DateTime.Now;
            IsAdminVote = isAdminVote;
            ElectionId = election;
        }
        public void Edit(int voteNumber, Guid userId, Guid candidateId, bool isAdminVote, Guid election)
        {
            VoteNumber = voteNumber;
            UserId = userId;
            CandidateId = candidateId;
            VoteTime = DateTime.Now;
            IsAdminVote = isAdminVote;
            ElectionId = election;
        }
        private Vote()
        {
            
        }
        public void AddCandidate(Guid candidateId)
        {
            CandidateId = candidateId;
        }
        public void AddElection(Guid electionId)
        {
            ElectionId = electionId;
        }
        //public Guid Id { get; private set; }
        public int VoteNumber { get; private set; }
        public Guid UserId { get; private set; }
        public Guid CandidateId { get; private set; }
        public DateTime VoteTime { get; private set; }
        public bool IsAdminVote { get; set; }
        public Guid ElectionId { get; set; }

    }
}
