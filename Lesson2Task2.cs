using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            string info = "A - add new account, D - delete account, T - transaction , U - undo";
            while(true)
            {
                bank.Show();
                Console.WriteLine(info);
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                if(key.Key == ConsoleKey.A)
                {
                    Console.WriteLine("You chose add new account, pls input his start amount");
                    bank.NewAccount(int.Parse(Console.ReadLine()));
                }
                if(key.Key == ConsoleKey.D)
                {
                    Console.WriteLine("You chose delete account, pls input his Id");
                    bank.DeleteAccount(int.Parse(Console.ReadLine()));
                }
                if(key.Key == ConsoleKey.T)
                {
                    Console.WriteLine("You chose transaction , pls input Id from , Id to and sum");
                    bank.Transaction(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
                }
                if(key.Key == ConsoleKey.U)
                {
                    if(bank.Commands.Count>0) 
                        (bank.Commands.Pop()).Execute(bank);
                }
            }
            Console.ReadLine();
        }
    }
    public class Bank
    {
        public List<Account> AllAccounts = new List<Account>();
        public Stack<Command> Commands = new Stack<Command>();

        public void NewAccount(int sum)
        {
            AllAccounts.Add(new Account(sum));
            Commands.Push(new Command { type = "Add", fromIdUndo = AllAccounts[AllAccounts.Count - 1].id });
        }

        public void DeleteAccount(int id)
        {
            Account removedAccount = AllAccounts.Find(item => item.id ==id);
            if(removedAccount==null)
            {
                Console.WriteLine("Invalid account's id");
                return;
            }
            AllAccounts.Remove(removedAccount);
            Commands.Push(new Command { type = "Delete" ,deletedAc = removedAccount});
        }
        public void Transaction(int fromId,int toId,int sum)
        {
            Account fromAccount = AllAccounts.Find(item => item.id == fromId);
            Account toAccount = AllAccounts.Find(item => item.id == toId);
            if (fromAccount == null || toAccount == null)
            {
                Console.WriteLine("Invalid account's id");
                return;
            }
            if (fromAccount.sum < sum)
            {
                Console.WriteLine("Not enough money");
                return;
            }
            fromAccount.ChangeSum(-sum);
            toAccount.ChangeSum(sum);
            Commands.Push(new Command { type = "Transaction", fromIdUndo = fromId, toIdUndo = toId, sumUndo = sum});
        }

        public void Show()
        {
            Console.Clear();
            foreach(var v in AllAccounts)
            {
                Console.WriteLine("id: {0} , sum: {1}",v.id,v.sum);
            }
        }
    }
    public class Command
    {
        public string type { get; set; }
        public Account deletedAc;
        public int fromIdUndo, toIdUndo, sumUndo;

        public void Execute(Bank bank)
        {
            switch(type)
            {
                case "Add":
                    bank.AllAccounts.Remove(bank.AllAccounts.Find(item => item.id == fromIdUndo));
                break;
                case "Delete":
                    bank.AllAccounts.Add(deletedAc);
                break;
                case  "Transaction":
                    bank.AllAccounts.Find(item => item.id == fromIdUndo).ChangeSum(sumUndo);
                    bank.AllAccounts.Find(item => item.id == toIdUndo).ChangeSum(-sumUndo);
                break;
            }
        }
    }
    public class Account
    {
        private static int lastid = 0;
        public int id { get; private set; }
        public int sum { get; private set; }

        public Account(int newSum)
        {
            id = lastid++;
            sum = newSum;
        }       

        public void ChangeSum(int delta)
        {
            sum += delta;
        }
    }

}