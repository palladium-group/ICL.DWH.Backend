using ICL.DWH.Backend.Core.Entities;

namespace ICL.DWH.Backend.Core.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
