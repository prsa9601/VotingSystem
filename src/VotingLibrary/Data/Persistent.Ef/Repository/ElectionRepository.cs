using VotingLibrary.Data.Entities.Repository;

namespace VotingLibrary.Data.Persistent.Ef.Repository
{
    public class ElectionRepository : IElectionRepository
    {
        public ElectionRepository(Context context) : base(context)
        {
        }
    }
}
