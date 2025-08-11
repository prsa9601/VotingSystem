namespace voting.ViewModel.VoteModel
{
    public class VoteViewModel
    {
        public int VoteNumber { get; set; }
        public Guid UserId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime VoteTime { get; set; }
        public bool IsAdminVote { get; set; }
        public Guid ElectionId { get; set; }
    }
    public class EditVoteViewModel
    {
        public Guid Id { get; set; }
        public int VoteNumber { get; set; }
        public Guid UserId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime VoteTime { get; set; }
        public bool IsAdminVote { get; set; }
        public Guid ElectionId { get; set; }
    }
}
