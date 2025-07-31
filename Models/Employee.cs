using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Example.Models;

[Table("Employee")]
public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PK_EmployeeId")]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    [Column("Salary")]
    public int Salary { get; set; }
}
