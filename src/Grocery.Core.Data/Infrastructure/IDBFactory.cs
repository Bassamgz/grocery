namespace Grocery.Core.Data.Infrastructure
{
    using System;

    public interface IDbFactory : IDisposable
    {
        eFruitEntities Init();
    }
}
