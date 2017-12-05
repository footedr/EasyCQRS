using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class EditEmployeeValidationBehavior : IPipelineBehavior<EditEmployeeCommand, Unit>
	{
		public Task<Unit> Handle(EditEmployeeCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
		{
			if (command == null)
				throw new Exception("Command cannot be null.");
			if (string.IsNullOrWhiteSpace(command.Name))
				throw new Exception("Employee name is required.");
			if (command.Salary <= 0)
				throw new Exception("Salary must be greater than zero.");
			return next();
		}
	}
}