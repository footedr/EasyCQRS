using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCQRS.Web.Data;
using EasyCQRS.Web.Features.Shared;
using Microsoft.EntityFrameworkCore;

namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeeDomain : IEmployeeDomain
	{
		private readonly EasyCqrsContext _context;

		public EmployeeDomain(EasyCqrsContext context)
		{
			_context = context;
		}

		public async Task<int> HireEmployeeAsync(string employeeName, DateTime dateOfBirth, decimal salary)
		{
			// Todo: Refactor DateTime.Now to DateTimeService for easy mocking
			// Todo: Add AutoMapper and map to employee class
			var employee = Employee.Create(employeeName, salary, dateOfBirth, DateTime.Now, true);
			_context.Employees.Add(employee);
			await _context.SaveChangesAsync();
			return employee.Id;
		}

		public async Task<IEnumerable<Employee>> GetEmployeesAsync(DateRange hireDateRange, bool includeTerminated)
		{
			var employeesQueryable = _context.Employees.Where(e => e.HireDate >= hireDateRange.Start && e.HireDate <= hireDateRange.End);
			if (!includeTerminated)
				employeesQueryable = employeesQueryable.Where(e => e.IsActive);
			return await employeesQueryable.ToListAsync();
		}

		public async Task TerminateEmployeeAsync(int id)
		{
			var employee = await _context.Employees.FindAsync(id);
			employee.TerminationDate = DateTime.Now;
			employee.IsActive = false;
			await _context.SaveChangesAsync();
		}

		public async Task UpdateEmployeeAsync(int id, string name, decimal salary)
		{
			var employee = await _context.Employees.FindAsync(id);
			employee.Name = name;
			employee.Salary = salary;
			await _context.SaveChangesAsync();
		}

		public Task<Employee> FindAsync(int id)
		{
			return _context.Employees.FindAsync(id);
		}
	}
}