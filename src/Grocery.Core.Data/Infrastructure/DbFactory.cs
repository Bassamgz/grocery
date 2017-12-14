namespace Grocery.Core.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private eFruitEntities dbContext;

        public eFruitEntities Init()
        {
            return this.dbContext ?? (this.dbContext = new eFruitEntities());
        }

        protected override void DisposeCore()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
            }
        }
    }
}
