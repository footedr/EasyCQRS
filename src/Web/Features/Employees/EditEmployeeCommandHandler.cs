using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand>
	{
		private readonly IEmployeeDomain _employeeDomain;

		public EditEmployeeCommandHandler(IEmployeeDomain employeeDomain)
		{
			_employeeDomain = employeeDomain;
		}

		public Task Handle(EditEmployeeCommand command, CancellationToken cancellationToken)
		{
			return _employeeDomain.UpdateEmployeeAsync(command.Id, command.Name, command.Salary);
		}
	}
}