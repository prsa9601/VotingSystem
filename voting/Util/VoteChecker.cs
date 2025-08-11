namespace voting.Util
{
    public static class VoteChecker 
    {
        public static bool CheckVote(int accessVoteNumber, List<Guid> candidatesId)
        {
            //آی دی و تعداد رای هاش
            Dictionary<Guid, int> candidateVotes = new Dictionary<Guid, int>(); 
            if (accessVoteNumber * 3 < candidatesId.Count)
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
