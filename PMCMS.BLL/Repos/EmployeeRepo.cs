using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMCMS.BLL.Repos
{
    public class EmployeeRepo
    {
        private static readonly PocketMenuDBEntities db = DBTool.GetInstance();
        public Employee InsertEmployee(Employee newEmployee)
        {
            db.Employees.Add(newEmployee);
            if (db.SaveChanges() > 0)
            {
                return newEmployee;
            }
            return null;
        }
    }
}
