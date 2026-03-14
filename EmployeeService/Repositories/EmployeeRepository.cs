using EmployeeService.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeService.Repositories
{
    public class EmployeeRepository
    {
        /// <summary>
        /// Returns all employees from the database.
        /// </summary>
        public Dictionary<int, Employee> GetAll()
        {
            var dt = DbHelper.ExecuteQuery("SELECT Id, Name, ManagerId FROM Employee");
            var result = new Dictionary<int, Employee>();

            foreach (DataRow row in dt.Rows)
            {
                var emp = new Employee
                {
                    ID = (int)row["Id"],
                    Name = (string)row["Name"],
                    ManagerID = row["ManagerId"] as int?
                };
                result[emp.ID] = emp;
            }

            return result;
        }

        /// <summary>
        /// Returns the current Enable value for a given employee.
        /// Returns null if the employee does not exist.
        /// </summary>
        public bool? GetEnableStatus(int id)
        {
            var dt = DbHelper.ExecuteQuery(
                "SELECT Enable FROM Employee WHERE Id = @id",
                new SqlParameter("@id", id));

            if (dt.Rows.Count == 0)
                return null;

            return (bool)dt.Rows[0]["Enable"];
        }

        /// <summary>
        /// Updates the Enable flag for a given employee.
        /// Returns the number of rows affected.
        /// </summary>
        public int UpdateEnableStatus(int id, int enable)
        {
            return DbHelper.ExecuteNonQuery(
                "UPDATE Employee SET Enable = @enable WHERE Id = @id",
                new SqlParameter("@id", id),
                new SqlParameter("@enable", enable));
        }
    }
}