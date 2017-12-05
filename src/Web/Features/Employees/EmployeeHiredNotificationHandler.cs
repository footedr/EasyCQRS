using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeeHiredNotificationHandler
		: AsyncNotificationHandler<EmployeeHiredNotification>
	{
		private readonly ILogger<EmployeeDomain> _logger;

		public EmployeeHiredNotificationHandler(ILogger<EmployeeDomain> logger)
		{
			_logger = logger;
		}

		protected override Task HandleCore(EmployeeHiredNotification notification)
		{
			_logger.LogInformation($"Employee #{notification.Id} was hired successfully.");
			return Unit.Task;
		}
	}
}