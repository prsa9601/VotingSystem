using VotingLibrary.Data.Persistent.Ef.Entities.Repository;

namespace VotingLibrary.Data.Persistent.Ef.Repository
{
    public class VoteRepository : IVoteRepository
    {
        public VoteRepository(Context context) : base(context)
        {
        }
    }
}
