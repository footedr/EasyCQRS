using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class HireEmployeeCommandHandler : IRequestHandler<HireEmployeeCommand, HireEmployeeCommandResponse>
	{
		private readonly IEmployeeDomain _employeeDomain;

		public HireEmployeeCommandHandler(IEmployeeDomain employeeDomain)
		{
			_employeeDomain = employeeDomain;
		}
		
		public async Task<HireEmployeeCommandResponse> Handle(HireEmployeeCommand command, 
			CancellationToken cancellationToken)
		{
			// process the command
			var id = await _employeeDomain.HireEmployeeAsync(command.Name,
				command.DateOfBirth,
				command.WeeklySalary);

			return new HireEmployeeCommandResponse
			{
				Id = id,
				Name = command.Name
			};
		}
	}
}