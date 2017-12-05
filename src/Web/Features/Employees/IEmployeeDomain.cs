using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyCQRS.Web.Features.Shared;

namespace EasyCQRS.Web.Features.Employees
{
	public interface IEmployeeDomain
	{
		Task<int> HireEmployeeAsync(string employeeName,
			DateTime dateOfBirth,
			decimal salary);

		Task<IEnumerable<Employee>> GetEmployeesAsync(DateRange hireDateRange, bool includeTerminated);

		Task TerminateEmployeeAsync(int id);

		Task UpdateEmployeeAsync(int id, string name, decimal salary);

		Task<Employee> FindAsync(int id);
	}
}