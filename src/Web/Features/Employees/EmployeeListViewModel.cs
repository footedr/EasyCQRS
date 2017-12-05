namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeeListViewModel
	{
		private EmployeeListViewModel(EmployeeListQuery query, EmployeeListQueryResponse employeeList)
		{
			Query = query;
			EmployeeList = employeeList;
		}

		public EmployeeListQuery Query { get; }

		public EmployeeListQueryResponse EmployeeList { get; }

		public static EmployeeListViewModel Create(EmployeeListQuery query, EmployeeListQueryResponse employeeList)
		{
			return new EmployeeListViewModel(query, employeeList);
		}
	}
}