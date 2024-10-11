using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        public DepartmentRepository(MvcAppG01DbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
