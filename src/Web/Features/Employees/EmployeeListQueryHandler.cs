using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyCQRS.Web.Features.Shared;
using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeeListQueryHandler : IRequestHandler<EmployeeListQuery, EmployeeListQueryResponse>
	{
		private readonly IEmployeeDomain _employeeDomain;

		public EmployeeListQueryHandler(IEmployeeDomain employeeDomain)
		{
			_employeeDomain = employeeDomain;
		}

		public async Task<EmployeeListQueryResponse> Handle(EmployeeListQuery query, CancellationToken cancellationToken)
		{
			var employees = await _employeeDomain.GetEmployeesAsync(DateRange.Create(query.HireDateStart, query.HireDateEnd),
				query.IncludeTerminated);

			return EmployeeListQueryResponse.Create(employees.Select(e => EmployeeListItem.Create(e.Id, e.HireDate, e.TerminationDate, e.Name))
				.OrderByDescending(emp => emp.HireDate));
		}
	}
}