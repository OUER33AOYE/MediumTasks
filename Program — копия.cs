using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForces
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManager Manager = new UserManager(new List<User>() { new User (300,"Roman"),new User (100,"Kostya"),new User (200,"Alex"),new User (1000,"Roman") });
            Console.WriteLine("Айди юзеров с именем Роман:");
            foreach (var v in Manager.GetUsersByName("Roman"))
            {
                Console.Write($"{v.id} {v.name} {v.salary} ; ");
            }

            Console.WriteLine("\nИмя юзера с айди == 0:");
            Console.WriteLine(Manager.GetUsersById(0).name);
            Console.WriteLine("Все юзеры:");
            foreach (var v in Manager.GetAllUsers())
            {
                Console.Write($"{v.id} {v.name} {v.salary} ; ");
            }
            Console.WriteLine("\nЮзеры с зп больше чем 100:");
            foreach (var v in Manager.GetUsersWithSalaryLargerThen(100))
            {
                Console.Write($"{v.id} {v.name} {v.salary} ; ");
            }
            Console.WriteLine("\nЮзеры с зп меньше чем 300");
            foreach (var v in Manager.GetUsersWithSalaryLessThen(300))
            {
                Console.Write($"{v.id} {v.name} {v.salary} ; ");
            }
            Console.WriteLine("\nЮзеры с зп между 100 и 300:");
            foreach (var v in Manager.GetUsersWithSalaryBetween(100,300))
            {
                Console.Write($"{v.id} {v.name} {v.salary} ; ");
            }
            Console.ReadLine();
        }
    }
    class User
    {
        private static int NewId = 0;
        public readonly int salary;
        public readonly int id;
        public readonly string name;
        public User(int Salary, string Name)
        {
            salary = Salary;
            name = Name;
            id = NewId++;
        }
    }
    class UserManager {
        List<User> Users = new List<User>();
        public UserManager(List<User> users) => Users = users;

        public List<User> GetUsersByName(string name) => Users.FindAll(item=>item.name == name);
        public User GetUsersById(int id) => Users.Find(item => item.id == id);
        public List<User> GetAllUsers() => Users;
        public List<User> GetUsersWithSalaryLargerThen(int N) => Users.FindAll(item => item.salary > N);
        public List<User> GetUsersWithSalaryLessThen(int N) => Users.FindAll(item => item.salary < N);
        public List<User> GetUsersWithSalaryBetween(int N1,int N2) => Users.FindAll(item => item.salary > N1 && item.salary < N2);
    }
}

/*

*/
