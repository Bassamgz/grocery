namespace Grocery.Core.Data.Repositories
{
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;

    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
