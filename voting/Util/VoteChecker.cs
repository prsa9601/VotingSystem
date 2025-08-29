using VotingLibrary.Data.Entities.Enums;

namespace voting.Util
{
    public static class VoteChecker
    {
        public static bool CheckVote(int accessVoteNumber, List<Guid> candidatesId, ElectionType electionType)
        {
            //آی دی و تعداد رای هاش
            int voteNumber = electionType switch
            {
                ElectionType.یک_رای => 1,
                ElectionType.دو_رای => 2,
                ElectionType.سه_رای => 3,
                ElectionType.پنج_رای => 5,
                _ => 3
            };

            Dictionary<Guid, int> candidateVotes = new Dictionary<Guid, int>();
            if (accessVoteNumber * voteNumber < candidatesId.Count)
            {
                return false;
            }
            foreach (var item in candidatesId)
            {
                if (!candidateVotes.ContainsKey(item))
                {
                    candidateVotes[item] = 0;
                }
                candidateVotes[item] += 1;
            }
            foreach (var item in candidateVotes)
            {
                if (item.Value > accessVoteNumber)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
