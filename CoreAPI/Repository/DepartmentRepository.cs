using CoreAPI.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using WebAPIDotNetCore.Entities;

namespace WebAPIDotNetCore.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly APIDBContext _appDBContext;
        public DepartmentRepository(APIDBContext context)
        {
            _appDBContext = context;// ??
                //throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<DepartmentEntity>> GetDepartment()
        {
            return await _appDBContext.Department.ToListAsync();
        }
        public async Task<DepartmentEntity> GetDepartmentByID(int ID)
        {
            return await _appDBContext.Department.FindAsync(ID);
        }
        public async Task<bool> InsertDepartment(DepartmentEntity objDepartment)
        {
            bool isSave = false;
            try
            {
                DepartmentEntity obj=new DepartmentEntity();
                obj.DepartmentName = objDepartment.DepartmentName;
                _appDBContext.Department.Add(obj);
                await _appDBContext.SaveChangesAsync();
                isSave = true;
            }
            catch (Exception ex) { }
            
            return isSave;
        }
        public async Task<DepartmentEntity> UpdateDepartment(DepartmentEntity objDepartment)
        {
            try
            {
                _appDBContext.Entry(objDepartment).State = EntityState.Modified;
                await _appDBContext.SaveChangesAsync();
            }
            catch(Exception ex) { }
            
            return objDepartment;
        }
        public async Task<bool> DeleteDepartment(int ID)
        {
            bool result = false;
            var department = _appDBContext.Department.Find(ID);
            if (department != null)
            {
                _appDBContext.Entry(department).State = EntityState.Deleted;
                await _appDBContext.SaveChangesAsync();
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
