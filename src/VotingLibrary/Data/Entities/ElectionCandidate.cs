namespace VotingLibrary.Data.Entities
{
    public class ElectionCandidate : BaseClass
    {
        public ElectionCandidate(Guid candidateId, Guid electionId)
        {
            //Id = Guid.NewGuid();
            CandidateId = candidateId;
            ElectionId = electionId;
        }

        //public Guid Id { get; set; }
        public Guid CandidateId { get; private set; }
        public Guid ElectionId { get; set; }
    }
}