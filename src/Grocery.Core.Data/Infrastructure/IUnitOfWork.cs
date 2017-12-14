namespace Grocery.Core.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
