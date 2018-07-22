namespace Employees.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeesContext context;
        private readonly IMapper mapper;

        public EmployeeService(EmployeesContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int AddEmployee<TEntity>(TEntity entity)
        {
            var employee = this.mapper.Map<Employee>(entity);

            this.context.Employees.Add(employee);
            this.context.SaveChanges();

            return employee.Id;
        }

        private Employee GetEmployeeById(int employeeId)
        {
            var employee = this.context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new InvalidOperationException($"Employee with id {employeeId} was not found!");
            }

            return employee;
        }

        public TEntity SetBirthday<TEntity>(int employeeId, DateTime birthday)
        {
            var employee = GetEmployeeById(employeeId);

            employee.Birthday = birthday;

            context.SaveChanges();

            var employeeDto = this.mapper.Map<TEntity>(employee);

            return employeeDto;
        }

        public TEntity SetAddress<TEntity>(int employeeId, string address)
        {
            var employee = GetEmployeeById(employeeId);

            employee.Address = address;

            context.SaveChanges();

            var employeeDto = this.mapper.Map<TEntity>(employee);

            return employeeDto;
        }

        public TEntity SetManager<TEntity>(int employeeId, int managerId)
        {
            var employee = GetEmployeeById(employeeId);
            var manager = GetEmployeeById(managerId);

            employee.Manager = manager;

            this.context.SaveChanges();

            var managerDto = this.mapper.Map<TEntity>(manager);
            return managerDto;
        }

        public TEntity GetEmployeeInfo<TEntity>(int employeeId)
        {
            var dto = context.Employees
                .Where(e => e.Id == employeeId)
                .ProjectTo<TEntity>(mapper.ConfigurationProvider)
                .SingleOrDefault();

            if (dto == null)
            {
                throw new InvalidOperationException($"Employee with id {employeeId} was not found!");
            }

            return dto;
        }

        public IEnumerable<TEntity> ListEmployeesOlderThan<TEntity>(int years)
        {
            var dtos = this.context.Employees
                .Where(e => EF.Functions.DateDiffYear(e.Birthday, DateTime.Today) > years)
                .OrderByDescending(e => e.Salary)
                .ProjectTo<TEntity>(mapper.ConfigurationProvider)
                .ToList();

            return dtos;
        }
    }
}
