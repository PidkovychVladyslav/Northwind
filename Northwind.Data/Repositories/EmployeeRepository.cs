namespace Northwind.Data.Repositories
{
    using Northwind.Core.Models;
    using Northwind.Data.Infrastructure;

    public interface IEmployeeRepository : IRepository<Employee>
    {
    }

    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(NorthwindDbContext context)
            : base(context)
        {
        }
    }
}
