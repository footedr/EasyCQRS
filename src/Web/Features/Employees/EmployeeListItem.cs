using System;

namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeeListItem
	{
		private EmployeeListItem(int id, DateTime hireDate, DateTime? terminationDate, string name)
		{
			Id = id;
			HireDate = hireDate;
			TerminationDate = terminationDate;
			Name = name;
		}

		public int Id { get; }

		public DateTime HireDate { get; }

		public DateTime? TerminationDate { get; }

		public string Name { get; }

		public static EmployeeListItem Create(int id, DateTime hireDate, DateTime? terminationDate, string name)
		{
			return new EmployeeListItem(id, hireDate, terminationDate, name);
		}
	}
}