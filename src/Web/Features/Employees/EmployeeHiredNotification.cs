using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeeHiredNotification : INotification
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}
}