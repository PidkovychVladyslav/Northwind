namespace Northwind.Data.Infrastructure
{
    using Northwind.Data.Repositories;
    using System;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private NorthwindDbContext dataContext;

        private IEmployeeRepository employees;
        private bool isDisposed = false;

        public UnitOfWork()
        {
            this.dataContext = new NorthwindDbContext();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public IEmployeeRepository Employees
        {
            get
            {
                return employees ?? (employees = new EmployeeRepository(dataContext));
            }
        }

        public void Commit()
        {
            dataContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                dataContext.Dispose();
            }
            isDisposed = true;
        }
    }
}
