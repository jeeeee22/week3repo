using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EntityModels.Models;

namespace Week3EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new IndustryConnectWeek2Context())
            {
                context.Database.EnsureCreated();
                var customersWithoutSales = context.Customers
                    .Where(c => !c.Sales.Any())
                    .ToList();

                Console.WriteLine("Customers without sales:");
                foreach (var customer in customersWithoutSales)
                {
                    Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}");
                }
                var newCustomer = new Customer
                {
                    FirstName = "New",
                    LastName = "Customer",
                    DateOfBirth = new DateTime(1985, 5, 23)
                };
                context.Customers.Add(newCustomer);
                context.SaveChanges();

                var newProduct = context.Products.FirstOrDefault();
                var newStore = context.Stores.FirstOrDefault();

                if (newProduct != null && newStore != null)
                {
                    var newSale = new Sale
                    {
                        CustomerId = newCustomer.Id,
                        ProductId = newProduct.Id,
                        StoreId = newStore.Id,
                        DateSold = DateTime.Now
                    };
                    context.Sales.Add(newSale);
                    context.SaveChanges();
                }

                Console.WriteLine($"Added new customer: {newCustomer.FirstName} {newCustomer.LastName} with a sale record.");

                var anotherStore = new Store
                {
                    Name = "New Store"
                };
                context.Stores.Add(anotherStore);
                context.SaveChanges();

                Console.WriteLine($"Added new store: {anotherStore.Name}");

                var storesWithSales = context.Stores
                    .Where(s => s.Sales.Any())
                    .ToList();

                Console.WriteLine("Stores with sales:");
                foreach (var store in storesWithSales)
                {
                    Console.WriteLine($"Store: {store.Name}");
                }

                Console.WriteLine("Operations completed successfully.");
            }
        }
    }
}
