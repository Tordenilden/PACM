using PACM.Classes;
using System;

namespace PACM
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerRepository repo = new CustomerRepository();
            Customer customer = repo.retrieveDB(1);
            Console.WriteLine("Hello World!");
        }
    }
}
