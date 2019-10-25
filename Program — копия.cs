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
            Wombat Kombat = new Wombat { Health = 1000, Armor = 10 };
            Human Sasha = new Human { Health = 100, Agility = 5 };
            while (Kombat.Health > 0 && Sasha.Health > 0)
            {
                Kombat.TakeDamage(50);
                Sasha.TakeDamage(50);
                Console.WriteLine("Kombat hp: {0}  human hp:{1}", Kombat.Health, Sasha.Health);
            }
            Console.ReadLine();
        }
    }
    public class Charachter
    {
        public int Health;
        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Console.WriteLine("Я умер");
            }
        }
    }
    class Wombat : Charachter
    {
        public int Armor;

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage - Armor);
        }
    }

    class Human : Charachter
    {
        public int Agility;

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage / Agility);
        }
    }
}
