using VotingLibrary.Core.Common.Repository;
using VotingLibrary.Data.Entities;
using VotingLibrary.Data.Persistent.Ef;

namespace VotingLibrary.Data.Entities.Repository
{
    public class IElectionRepository : BaseRepository<Election>
    {
        public IElectionRepository(Context context) : base(context)
        {
        }
    }
}
