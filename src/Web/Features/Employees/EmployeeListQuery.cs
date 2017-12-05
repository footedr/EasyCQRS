using System;
using MediatR;

namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeeListQuery : IRequest<EmployeeListQueryResponse>
	{
		public DateTime HireDateStart { get; set; }

		public DateTime HireDateEnd { get; set; }

		public bool IncludeTerminated { get; set; }
	}
}