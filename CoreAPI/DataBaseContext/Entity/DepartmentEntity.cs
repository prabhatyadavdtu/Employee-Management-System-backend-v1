using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIDotNetCore.Entities
{
    [Table("Department")]
    public class DepartmentEntity
    {
        [Key]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
