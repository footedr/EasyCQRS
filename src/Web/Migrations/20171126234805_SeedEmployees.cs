using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EasyCQRS.Web.Migrations
{
    public partial class SeedEmployees : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2017-01-01', 1, 'John Doe', 500)
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2016-01-01', 1, 'Jane Doe', 1500)
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2015-01-01', 1, 'Joe Schmoe', 1000)
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2015-06-01', 1, 'Schoe Jmoe', 700)
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2014-01-01', 1, 'Babs McGee', 400)
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2014-01-01', 1, 'Schmabs Fligee', 890)
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2013-01-01', 1, 'God Shammgod', 2000)
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2013-06-01', 1, 'Shod Gammgod', 1350)
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2012-01-01', 1, 'Runnin McOuttanames', 300)
				INSERT INTO [dbo].[Employees] (DateOfBirth, HireDate, IsActive, Name, Salary)
					VALUES ('1980-01-01', '2011-01-01', 1, 'This Isenough', 1175)
			");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
				DELETE FROM [dbo].[Employees]
			");
		}
    }
}
