namespace Employees.Services.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IEmployeeService
    {
        /// <summary>
        /// Adds a new Employee to the database and returns EmployeeId.
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddEmployee<TEntity>(TEntity entity);

        /// <summary>
        /// 
        /// Sets the birthday of the employee to the given date.
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="employeeId"></param>
        /// <param name="birthday"></param>
        /// <returns></returns>
        TEntity SetBirthday<TEntity>(int employeeId, DateTime birthday);

        /// <summary>
        /// 
        /// Sets the address of the employee to the given address.
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="employeeId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        TEntity SetAddress<TEntity>(int employeeId, string address);

        /// <summary>
        /// 
        /// Gets employee's info.
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        TEntity GetEmployeeInfo<TEntity>(int employeeId);

        /// <summary>
        /// 
        /// Sets a manager to the employee.
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="employeeId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        TEntity SetManager<TEntity>(int employeeId, int managerId);

        IEnumerable<TEntity> ListEmployeesOlderThan<TEntity>(int years);
    }
}
