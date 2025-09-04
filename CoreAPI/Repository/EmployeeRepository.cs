using CoreAPI.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using WebAPIDotNetCore.Entities;

namespace WebAPIDotNetCore.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly APIDBContext _appDBContext;
        public EmployeeRepository(APIDBContext context)
        {
            _appDBContext = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<EmployeeEntity>> GetEmployees()
        {
            var emp = new List<EmployeeEntity>();
                emp = await _appDBContext.Employee.ToListAsync();
            var dep = new List<DepartmentEntity>();
                dep = await _appDBContext.Department.ToListAsync();

            var result = from employee in emp
                         join dept in dep on employee.Department equals Convert.ToString(dept.DepartmentId)
                         select new EmployeeEntity {
                             EmployeeName = employee.EmployeeName,
                             EmployeeID = employee.EmployeeID,
                             EmailId = employee.EmailId,
                             Department = dept.DepartmentName,
                             DOJ = employee.DOJ,
            };
            return result.ToList();
        }
        public async Task<EmployeeEntity> GetEmployeeByID(int ID)
        {
            return await _appDBContext.Employee.FindAsync(ID);
        }
        public async Task<EmployeeEntity> InsertEmployee(EmployeeEntity objEmployee)
        {
            var department = await _appDBContext.Department.FirstOrDefaultAsync(d => d.DepartmentName == objEmployee.Department);

            if (department != null)
            {
                var emp = new EmployeeEntity()
                {
                    EmployeeName = objEmployee.EmployeeName,
                    Department = Convert.ToString(department.DepartmentId), // save actual ID
                    EmailId = objEmployee.EmailId,
                    DOJ = objEmployee.DOJ
                };
                _appDBContext.Employee.Add(emp);
                await _appDBContext.SaveChangesAsync();

                return emp;
            }
            return null;
        }
        public async Task<EmployeeEntity> UpdateEmployee(EmployeeEntity objEmployee)
        {
            var department = await _appDBContext.Department.FirstOrDefaultAsync(d => d.DepartmentName == objEmployee.Department);
            if(department != null)
            {
                objEmployee.Department = Convert.ToString(department.DepartmentId);
            }
            _appDBContext.Entry(objEmployee).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objEmployee;
        }
        public bool DeleteEmployee(int ID)
        {
            bool result = false;
            var department = _appDBContext.Employee.Find(ID);
            if (department != null)
            {
                _appDBContext.Entry(department).State = EntityState.Deleted;
                _appDBContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }        
    }
}
