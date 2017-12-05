using System;
using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class HireEmployeeCommand : IRequest<HireEmployeeCommandResponse>
	{
		public string Name { get; set; }

		public DateTime DateOfBirth { get; set; }

		public decimal WeeklySalary { get; set; }
	}
}