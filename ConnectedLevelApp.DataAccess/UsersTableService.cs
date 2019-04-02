using ConnectedLevelApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace ConnectedLevelApp.DataAccess
{
    public class UsersTableService
    {
        private readonly string _connectionString = "";
        private SqlTransaction transaction;

        public UsersTableService()
        {
            _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                AttachDbFilename=C:\Users\ШадикьянР\source\repos\ConnectedLevelApp\ConnectedLevelApp.DataAccess\Database.mdf;
                                Integrated Security=True";
        }

        public List<User> SelectUsers()
        {
            var data = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = "select * from Users";

                    var sqlDataReader = command.ExecuteReader();
                     
                    while (sqlDataReader.Read())
                    {
                        int id = (int)sqlDataReader["Id"];
                        string password = sqlDataReader["Login"].ToString();
                        string login = sqlDataReader["Password"].ToString();

                        data.Add(new User() {
                            Id = id,
                            Login = login,
                            Password = password
                        });
                    }
                }
                catch (SqlException exception)
                {
                    // обработать
                    throw;
                }
                catch (Exception exception)
                {
                    // обработать
                    throw;
                }
            }
            return data;
        }
        public void InsertUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    command.CommandText = "insert into Users values(@login, @password)";

                    var loginParameter = new SqlParameter();
                    loginParameter.ParameterName = "@login";
                    loginParameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                    loginParameter.SqlValue = user.Login;

                    var passwrodParameter = new SqlParameter();
                    passwrodParameter.ParameterName = "@password";
                    passwrodParameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                    passwrodParameter.SqlValue = user.Password;

                    command.Parameters.Add(passwrodParameter);
                    command.Parameters.Add(loginParameter);

                    command.Transaction = transaction;

                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows < 1)
                    {
                        throw new Exception("Вставка не удалась");
                    }

                    transaction.Commit();
                }
                catch (SqlException exception)
                {
                    // обработать
                    transaction?.Rollback();
                    throw;
                }
                catch (Exception exception)
                {
                    // обработать
                    transaction?.Rollback();
                    throw;
                }
            }
        }
        public void DeleteUserById(int id)
        {

        }
        public void UpdateUser(User user)
        {

        }
    }
}
