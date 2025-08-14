
namespace VotingLibrary.Core.Services.NewFolder
{
    public class ElectionModelForResultPage
    {
        public string Title { get; set; }
        public int AllVotes { get; set; }
        public List<CandidateModelForResultPage> Candidates { get; set; } = new();
        public void AddCandidate(string fullName, int Index, int votesCount)
        {
            Candidates.Add(new CandidateModelForResultPage
            {
                FullName = fullName,
                Index = Index,
                Votes = votesCount
            });
        }
        public void TopCandidates()
        {
            var candidates = Candidates.OrderByDescending(i => i.Votes).ToList();
            var result = candidates.Take(5);
            Candidates.Clear();
            int index1 = 0;
            foreach (var item in result)
            {
                index1++;
                item.Index = index1;
                Candidates.Add(item);
            }
        }
    }
}
