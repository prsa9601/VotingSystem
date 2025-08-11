using VotingLibrary.Data.Entities.Repository;

namespace VotingLibrary.Data.Persistent.Ef.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        public CandidateRepository(Context context) : base(context)
        {
        }
    }
}
