namespace voting.ViewModel.Election
{
    public class ElectionViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsActive { get; set; } = true;
        public List<Guid>? CandidateId { get; set; }
        public List<Guid>? VotesId { get; set; }
    }
}
