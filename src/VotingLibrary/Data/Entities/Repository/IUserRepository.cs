using VotingLibrary.Core.Common.Repository;
using VotingLibrary.Data.Entities;
using VotingLibrary.Data.Persistent.Ef;

namespace VotingLibrary.Data.Entities.Repository
{
    public class IUserRepository : BaseRepository<User>
    {
        public IUserRepository(Context context) : base(context)
        {
        }
    }
}
