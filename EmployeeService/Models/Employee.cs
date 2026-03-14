using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EmployeeService.Models
{
    [DataContract]
    public class Employee
    {
        [DataMember(Order = 0)]
        public int ID { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public int? ManagerID { get; set; }

        [DataMember(Order = 3)]
        public List<Employee> Employees { get; set; }

        public Employee()
        {
            Employees = new List<Employee>();
        }
    }
}