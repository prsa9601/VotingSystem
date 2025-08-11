using VotingLibrary.Core.Common.Repository;
using VotingLibrary.Data.Entities;
using VotingLibrary.Data.Persistent.Ef;

namespace VotingLibrary.Data.Persistent.Ef.Entities.Repository
{
    public class IVoteRepository : BaseRepository<Vote>
    {
        public IVoteRepository(Context context) : base(context)
        {
        }
    }
}
