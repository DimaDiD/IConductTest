using EmployeeService.Models;
using EmployeeService.Repositories;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    public class Service1 : IEmployeeService
    {
        private readonly EmployeeRepository _repository = new EmployeeRepository();

        public Employee GetEmployeeById(int id)
        {
            AppLogger.Info($"GetEmployeeById called with id={id}");

            if (id <= 0)
                throw ServiceException.BadRequest("ID must be a positive number.");

            var all = _repository.GetAll();

            if (all.Count == 0)
                throw ServiceException.NotFound("No employees found in the database.");

            if (!all.ContainsKey(id))
                throw ServiceException.NotFound($"Employee with ID {id} not found.");

            foreach (var emp in all.Values)
            {
                if (emp.ManagerID.HasValue && all.ContainsKey(emp.ManagerID.Value))
                {
                    all[emp.ManagerID.Value].Employees.Add(emp);
                }
            }

            AppLogger.Info($"GetEmployeeById returning employee '{all[id].Name}' with {all[id].Employees.Count} direct reports.");
            return all[id];
        }

        public void EnableEmployee(int id, int enable)
        {
            AppLogger.Info($"EnableEmployee called with id={id}, enable={enable}");

            if (id <= 0)
                throw ServiceException.BadRequest("ID must be a positive number.");

            if (enable != 0 && enable != 1)
                throw ServiceException.BadRequest("Enable must be 0 or 1.");

            var currentStatus = _repository.GetEnableStatus(id);

            if (currentStatus == null)
                throw ServiceException.NotFound($"Employee with ID {id} not found.");

            if (currentStatus.Value == (enable == 1))
                throw ServiceException.BadRequest(
                    $"Employee is already {(currentStatus.Value ? "enabled" : "disabled")}.");

            _repository.UpdateEnableStatus(id, enable);

            AppLogger.Info($"Employee {id} has been {(enable == 1 ? "enabled" : "disabled")}.");
        }
    }
}