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

			var editEmployee = new EditEmployeeCommand
			{
				Name = employee.Name,
				Id = employee.Id,
				Salary = employee.Salary
			};

			return View(editEmployee);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditEmployeeCommand command)
		{
			try
			{
				await _mediator.Send(command);
				return RedirectToAction("List");
			}
			catch (Exception exception)
			{
				ModelState.AddModelError(string.Empty, exception.Message);
			}

			return View(command);
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

		// For code demo:
		// Todo: Create update employee command (id, name, salary)
		// Todo: Create update employee command handler (use _employeeDomain.UpdateEmployeeAsync)
		// Todo: Create update GET and POST controller actions (copy and paste the edit ones, change types)
		// Todo: Create update view (copy and rename Edit.cshtml, change types)
		// Todo: Ensure update view posts to update controller action
		// Todo: Update List.cshtml to point to the Update view (vs. Edit)
		// Todo: Add DI bindings to Startup.cs for update command -> command handler
		// Todo: Add update employee validation behavior (copy & paste from edit, change names)
		// Todo: Add DI binding for update validation behavior -> IPipelineBehavior
	}
}