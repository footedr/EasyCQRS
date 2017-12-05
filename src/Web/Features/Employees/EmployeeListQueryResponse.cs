using System.Collections.Generic;

namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeeListQueryResponse
	{
		private EmployeeListQueryResponse(IEnumerable<EmployeeListItem> employees)
		{
			Employees = employees;
		}

		public IEnumerable<EmployeeListItem> Employees { get; }

		public static EmployeeListQueryResponse Create(IEnumerable<EmployeeListItem> employees)
		{
			return new EmployeeListQueryResponse(employees);
		}
	}
}