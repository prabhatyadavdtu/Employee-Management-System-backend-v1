using WebAPIDotNetCore.Entities;

namespace WebAPIDotNetCore.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetEmployees();
        Task<EmployeeEntity> GetEmployeeByID(int ID);
        Task<EmployeeEntity> InsertEmployee(EmployeeEntity objEmployee);
        Task<EmployeeEntity> UpdateEmployee(EmployeeEntity objEmployee);
        bool DeleteEmployee(int ID);
    }
}
