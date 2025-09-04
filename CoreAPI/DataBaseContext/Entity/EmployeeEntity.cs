using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPIDotNetCore.Entities
{
    [Table("Employee")]
    public class EmployeeEntity
    {
        [Key]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string EmailId { get; set; }
        public DateTime DOJ { get; set; }
        
    }
}
