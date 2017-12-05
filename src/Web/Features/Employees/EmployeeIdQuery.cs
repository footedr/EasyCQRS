using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeeIdQuery : IRequest<EmployeeIdQueryResponse>
	{
		public int Id { get; set; }
	}

	public class EmployeeIdQueryResponse
	{
		private EmployeeIdQueryResponse(int id, string name, decimal salary)
		{
			Id = id;
			Name = name;
			Salary = salary;
		}

		public int Id { get; }

		public string Name { get; }

		public decimal Salary { get; }

		public static EmployeeIdQueryResponse Create(int id, string name, decimal salary)
		{
			return new EmployeeIdQueryResponse(id, name, salary);
		}
	}

	public class EmployeeIdQueryHandler : IRequestHandler<EmployeeIdQuery, EmployeeIdQueryResponse>
	{
		private readonly IEmployeeDomain _employeeDomain;

		public EmployeeIdQueryHandler(IEmployeeDomain employeeDomain)
		{
			_employeeDomain = employeeDomain;
		}

		public async Task<EmployeeIdQueryResponse> Handle(EmployeeIdQuery query, CancellationToken cancellationToken)
		{
			var employee = await _employeeDomain.FindAsync(query.Id);
			if (employee == null)
				return default(EmployeeIdQueryResponse);
			return EmployeeIdQueryResponse.Create(employee.Id, employee.Name, employee.Salary);
		}
	}
}