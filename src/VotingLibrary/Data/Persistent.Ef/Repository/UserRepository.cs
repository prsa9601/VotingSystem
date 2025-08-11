using VotingLibrary.Data.Entities.Repository;

namespace VotingLibrary.Data.Persistent.Ef.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }
    }
}
