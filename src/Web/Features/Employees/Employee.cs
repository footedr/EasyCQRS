using System;

namespace EasyCQRS.Web.Features.Employees
{
	public class Employee
	{		
		private Employee(string name, decimal salary, DateTime dateOfBirth, DateTime hireDate, bool isActive)
		{			
			Name = name;
			Salary = salary;
			DateOfBirth = dateOfBirth;
			HireDate = hireDate;
			IsActive = isActive;
		}

		public Employee() { }

		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime DateOfBirth { get; set; }

		public bool IsActive { get; set; }

		public decimal Salary { get; set; }

		public DateTime HireDate { get; set; }

		public DateTime? TerminationDate { get; set; }

		public static Employee Create(string name, decimal salary, DateTime dateOfBirth, DateTime hireDate, bool isActive)
		{
			return new Employee(name, salary, dateOfBirth, hireDate, isActive);
		}
	}
}