using VotingLibrary.Core.Common.Repository;
using VotingLibrary.Data.Entities;
using VotingLibrary.Data.Persistent.Ef;

namespace VotingLibrary.Data.Entities.Repository
{
    public class ICandidateRepository : BaseRepository<Candidate>
    {
        public ICandidateRepository(Context context) : base(context)
        {
        }
    }
}
