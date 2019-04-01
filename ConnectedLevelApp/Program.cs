using ConnectedLevelApp.DataAccess;
using ConnectedLevelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectedLevelApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            UsersTableService usersTableService = new UsersTableService();
            var user = new User()
            {
                Login = "admin",
                Password = "root"
            };
            usersTableService.InsertUser(user);

            foreach (var item in usersTableService.SelectUsers())
            {
                System.Console.WriteLine($"{item.Login}, {item.Password}");
            }
            System.Console.ReadLine();
        }
    }
}
