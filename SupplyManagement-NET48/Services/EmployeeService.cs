using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyManagement_NET48.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> Get()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any()) return null;
            return employees;
        }

        public Employee Get(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null) return null;
            return employee;
        }

        public Employee Create(Employee employeeCreate)
        {
            var employee = new Employee
            {
                Guid = Guid.NewGuid(),
                FirstName = employeeCreate.FirstName,
                LastName = employeeCreate.LastName,
                Email = employeeCreate.Email,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdEmployee = _employeeRepository.Create(employee);

            return createdEmployee;
        }

        public int Update(Employee employeeUpdate)
        {
            var getEmployee = _employeeRepository.GetByGuid(employeeUpdate.Guid);
            if (getEmployee == null) return 0;

            getEmployee.FirstName = employeeUpdate.FirstName;
            getEmployee.LastName = employeeUpdate.LastName;
            getEmployee.Email = employeeUpdate.Email;
            getEmployee.ModifiedDate = DateTime.Now;

            var isUpdate = _employeeRepository.Update(getEmployee);

            return isUpdate ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee == null) return 0;
            var isDelete = _employeeRepository.Delete(employee);
            return isDelete ? 1 : 0;
        }
    }
}