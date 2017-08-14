namespace Northwind.Data.Infrastructure
{
    using Northwind.Data.Repositories;
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }

        Task CommitAsync();

        void Commit();
    }
}
