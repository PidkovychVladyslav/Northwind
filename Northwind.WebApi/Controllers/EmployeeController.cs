namespace Northwind.WebApi.Controllers
{
    using Northwind.Core.Models;
    using Northwind.Data.Infrastructure;
    using Northwind.WebApi.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        public IEnumerable<EmployeeTemplate> Get()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Employees.GetAll().Select(o => new EmployeeTemplate { FirstName = o.FirstName, LastName = o.LastName, Title = o.Title, Orders = o.Orders.Count }).ToList();
            }
        }
    }
}