using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class TerminateEmployeeCommand : IRequest
	{
		public int Id { get; set; }
	}

	public class TerminateEmployeeCommandHandler : IRequestHandler<TerminateEmployeeCommand>
	{
		private readonly IEmployeeDomain _employeeDomain;

		public TerminateEmployeeCommandHandler(IEmployeeDomain employeeDomain)
		{
			_employeeDomain = employeeDomain;
		}
		
		public async Task Handle(TerminateEmployeeCommand command, CancellationToken cancellationToken)
		{
			await _employeeDomain.TerminateEmployeeAsync(command.Id);		
		}
	}
}