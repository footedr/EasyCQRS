using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyCQRS.Web.Features.Employees
{
	public class EmployeesController : Controller
	{
		private readonly IMediator _mediator;

		public EmployeesController(IMediator mediator, IEmployeeDomain employeeDomain)
		{
			_mediator = mediator;

		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Hire()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Hire(HireEmployeeCommand command)
		{
			// dispatch command to handler
			var hireEmployeeCommandResponse = await _mediator.Send(command);

			// create notification
			var notification = new EmployeeHiredNotification
			{
				Id = hireEmployeeCommandResponse.Id,
				Name = hireEmployeeCommandResponse.Name
			};

			// fire off notification
			await _mediator.Publish(notification);

			ViewBag.Message = $"Employee created successfully. Employee #{hireEmployeeCommandResponse.Id}.";

			return View();
		}

		public async Task<IActionResult> List(EmployeeListQuery query)
		{
			if (query.HireDateEnd == DateTime.MinValue)
				query.HireDateEnd = DateTime.MaxValue;

			var employeeResponse = await _mediator.Send(query);
			return View(EmployeeListViewModel.Create(query, employeeResponse));
		}

		public async Task<IActionResult> Edit([FromRoute] EmployeeIdQuery query)
		{
			var employee = await _mediator.Send(query);

			if (employee == null)
				return NotFound();

			// Todo: Construct command and return to view

			return View();
		}

		// Run in steps to show progress

		// Todo: Create edit employee command				
		// Todo: Create edit employee command handler
		// Todo: Create edit employee validation behavior
		// Todo: Modify Startup.cs for DI
		// Todo: Modify GET action to create and return command
		// Todo: Modify POST action to dispatch command and return command in event of error
		// Todo: Uncomment @model directive in Edit.cshtml
		// Todo: Demonstrate what happens in behavior when you dont call next()
		// Todo: Change behavior to await next() then do logging aftewards
		// Todo: If time, review the rest of the code (commands/queries/handlers)

		[HttpPost]
		public async Task<IActionResult> Edit()
		{
			try
			{
				// Todo: Add EditEmployeeCommand as parameter
				// Todo: Use Mediator to dispatch command

				return RedirectToAction("List");
			}
			catch (Exception exception)
			{
				ModelState.AddModelError(string.Empty, exception.Message);
			}

			return View();
		}

		public async Task<IActionResult> Terminate(int id)
		{
			var employee = await _mediator.Send(new EmployeeIdQuery { Id = id });

			return View(employee);
		}

		[HttpPost]
		public async Task<IActionResult> TerminateConfirm(TerminateEmployeeCommand command)
		{
			try
			{
				await _mediator.Send(new TerminateEmployeeCommand { Id = command.Id });
				return RedirectToAction("List");
			}
			catch (Exception exception)
			{
				ModelState.AddModelError(string.Empty, exception.Message);
			}

			var employee = await _mediator.Send(new EmployeeIdQuery { Id = command.Id });
			return View(employee);
		}
	}
}