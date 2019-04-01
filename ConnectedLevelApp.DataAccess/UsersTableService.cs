using ConnectedLevelApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConnectedLevelApp.DataAccess
{
    public class UsersTableService
    {

        private readonly string _connectionString = "";

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
                    command.CommandText = $"insert into Users values('{user.Login}', '{user.Password}')";
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows < 1)
                    {
                        throw new Exception("Вставка не удалась");
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
        }
        public void DeleteUserById(int id)
        {

        }
        public void UpdateUser(User user)
        {

        }
    }
}
