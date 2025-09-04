using WebAPIDotNetCore.Entities;

namespace WebAPIDotNetCore.Repository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentEntity>> GetDepartment();
        Task<DepartmentEntity> GetDepartmentByID(int ID);
        Task<bool> InsertDepartment(DepartmentEntity objDepartment);
        Task<DepartmentEntity> UpdateDepartment(DepartmentEntity objDepartment);
        Task<bool> DeleteDepartment(int ID);
    }
}
