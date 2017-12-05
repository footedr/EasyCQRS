using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class EditEmployeeCommand : IRequest
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Salary { get; set; }
	}
}