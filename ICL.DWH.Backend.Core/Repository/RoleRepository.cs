using ICL.DWH.Backend.Core.Entities;

namespace ICL.DWH.Backend.Core.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
