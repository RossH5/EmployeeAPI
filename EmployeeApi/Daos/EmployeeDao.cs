using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EmployeeApi.Models;

namespace EmployeeApi.Daos
{
    public class EmployeeDao
    {
        private readonly DapperContext _context;

        public EmployeeDao(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var query = "SELECT * FROM Employee";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);

                return employees.ToList();
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var query = $"SELECT * FROM Employee WHERE Id = {id}";

            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QueryFirstOrDefaultAsync<Employee>(query);
                return employee;
            }
        }

        public async Task DeleteEmployeeById(int id)
        {
            var query = $"DELETE FROM Employee WHERE Id = {id}";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateEmployeeById(Employee updateRequest)
        {
            var query = "UPDATE Employee SET FirstName=@FirstName, LastName=@LastName, Email=@Email, " +
                        $"PhoneNumber=@PhoneNumber, HireDate=@HireDate, ProjectId=@ProjectId, " +
                        $"DepartmentId=@DepartmentId, Salary=@Salary WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", updateRequest.Id, DbType.Int32);
            parameters.Add("FirstName", updateRequest.FirstName, DbType.String);
            parameters.Add("LastName", updateRequest.LastName, DbType.String);
            parameters.Add("Email", updateRequest.Email, DbType.String);
            parameters.Add("PhoneNumber", updateRequest.PhoneNumber, DbType.String);
            parameters.Add("HireDate", updateRequest.HireDate, DbType.Date);
            parameters.Add("ProjectId", updateRequest.ProjectId, DbType.Int32);
            parameters.Add("DepartmentId", updateRequest.DepartmentId, DbType.Int32);
            parameters.Add("Salary", updateRequest.Salary, DbType.String);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task CreateEmployee(Employee updateRequest)
        {
            var query = "INSERT INTO Employee (FirstName, LastName, Email, PhoneNumber, HireDate, ProjectId, DepartmentId, Salary) VALUES (@FirstName, @LastName, @Email, " +
                        $"@PhoneNumber, @HireDate, @ProjectId, " +
                        $"@DepartmentId, @Salary)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", updateRequest.Id, DbType.Int32);
            parameters.Add("FirstName", updateRequest.FirstName, DbType.String);
            parameters.Add("LastName", updateRequest.LastName, DbType.String);
            parameters.Add("Email", updateRequest.Email, DbType.String);
            parameters.Add("PhoneNumber", updateRequest.PhoneNumber, DbType.String);
            parameters.Add("HireDate", updateRequest.HireDate, DbType.Date);
            parameters.Add("ProjectId", updateRequest.ProjectId, DbType.Int32);
            parameters.Add("DepartmentId", updateRequest.DepartmentId, DbType.Int32);
            parameters.Add("Salary", updateRequest.Salary, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
