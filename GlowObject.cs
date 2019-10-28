using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2_Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Store Pyaterochka = new Store();
            while (true)
            {
                Console.Clear();
                Pyaterochka.Show();
                Console.WriteLine("commands : A - add , D - delivery, P - pick up , M - make/change discount");
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.A)
                {
                    Console.WriteLine("write new product type");
                    string producttype = Console.ReadLine();
                    switch (producttype.ToLower())
                    {
                        case "clothes":
                            Pyaterochka.Storage.Add(new Clothes());
                            break;
                        case "shoes":
                            Pyaterochka.Storage.Add(new Shoes());
                            break;
                        case "notebook":
                            Pyaterochka.Storage.Add(new Notebook());
                            break;
                        case "phone":
                            Pyaterochka.Storage.Add(new Phone());
                            break;
                    }
                    Console.WriteLine("write price for this product");
                    Pyaterochka.Storage.Last<Product>().ChangePrice(int.Parse(Console.ReadLine()));
                }
                else
                {
                    Console.WriteLine("write index of product:");
                    int storageIndex = int.Parse(Console.ReadLine());
                    if (key.Key == ConsoleKey.M)
                    {
                        Console.WriteLine("how much discount you want?");
                        float newDiscount = int.Parse(Console.ReadLine());
                        Pyaterochka.Storage[storageIndex].discount = newDiscount;
                    }
                    if (key.Key == ConsoleKey.D)
                    {
                        Pyaterochka.Storage[storageIndex].BuyDelivery();
                    }
                    if (key.Key == ConsoleKey.P)
                    {
                        Pyaterochka.Storage[storageIndex].BuyPickup();
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Ant key for next command ...");
                Console.ReadKey();
            }
        }
    }
    public class Store
    {
        public List<Product> Storage = new List<Product>();
        public void Show()
        {
            Console.WriteLine("Storage have {0} products:", Storage.Count);
            for (int i = 0; i < Storage.Count; i++)
            {
                var v = Storage[i];
                Console.WriteLine("Id:{0}, Type:{1}, IsSale:{2} ,Discount:{3} , Base Price:{4}, Total Price: {5}", i, v.GetType().ToString().Substring(16), v.isSale, v.discount, v.GetPrice(), v.GetTotalPrice());
            }
            Console.WriteLine();
        }
    }

    public abstract class Product
    {
        public bool isSale = false;

        private float _discount;
        public float discount
        {
            get
            {
                return _discount;
            }
            set
            {
                if (value >= 0)
                {
                    isSale = true;
                    _discount = value;
                }
                else throw new ArgumentException("Discount less then zero", "value");
            }
        }

        public abstract float GetTotalPrice();
        public abstract float GetPrice();
        public abstract void ChangePrice(float newPrice);
        public void BuyDelivery()
        {
            if (isSale == false)
                Console.WriteLine($"{this.GetType().ToString().Substring(16)} was deliveried, you spend {GetTotalPrice()} dollars");
            else
                Console.WriteLine($"This product {this.GetType().ToString().Substring(16)} have a discount, so you can't deliveried this");
        }
        public void BuyPickup()
        {
            Console.WriteLine($"{this.GetType().ToString().Substring(16)} was pickuped, you spend {GetTotalPrice()} dollars");
        }
    }
    public class Clothes : Product
    {
        public static float price;
        public override float GetTotalPrice() => isSale ? price - price * discount / 100.0f : price;
        public override float GetPrice() => price;
        public override void ChangePrice(float newPrice) => price = newPrice;

    }
    public class Shoes : Product
    {
        public static float price;
        public override float GetTotalPrice() => isSale ? price - price * discount / 100.0f : price;
        public override float GetPrice() => price;
        public override void ChangePrice(float newPrice) => price = newPrice;
    }
    public class Phone : Product
    {
        public static float price;
        public override float GetTotalPrice() => isSale ? price - price * discount / 100.0f : price;
        public override float GetPrice() => price;
        public override void ChangePrice(float newPrice) => price = newPrice;
    }
    public class Notebook : Product
    {
        public static float price;
        public override float GetTotalPrice() => isSale ? price - price * discount / 100.0f : price;
        public override float GetPrice() => price;
        public override void ChangePrice(float newPrice) => price = newPrice;
    }
}










