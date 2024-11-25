using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using ServicesLab1.Models;
using ServicesLab1.Repositories;
using ServicesLab1.Services;
using System;

namespace ServicesLab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            using var scope = serviceProvider.CreateScope();


            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var bankAccountService = scope.ServiceProvider.GetRequiredService<IBankAccountService>();

            // Example operations

            User client = new User();

            Console.WriteLine("Enter User Name");
            client.Name = Console.ReadLine();

            Console.WriteLine("Enter User Email");
            client.Email = Console.ReadLine();

            userService.AddUser(client); // return ID after adding?


            Console.WriteLine("Enter User ID to Search");
            int ID = int.Parse(Console.ReadLine()); 

            var user = userService.GetUserById(ID);
            Console.WriteLine($"User: {user.Name}, Email: {user.Email}");


            BankAccount BA = new BankAccount(); 
            bankAccountService.AddAccount(new BankAccount { AccountNumber = "123456789", Balance = 1000, UserId = user.Id });








           


            Console.WriteLine($"Balance after deposit: {bankAccountService.BalanceInquiry(1)}");







            bankAccountService.Withdraw(1, 300);
            Console.WriteLine($"Balance after withdrawal: {bankAccountService.BalanceInquiry(1)}");

            //bankAccountService.DeleteAccount(1);
            //userService.DeleteUser(user.Id);
        }

        //public string Withdraw()
        //{
        //    //Console.WriteLine("enter account number");
        //    //int accountNumber = int.Parse(Console.ReadLine());
        //    //Console.WriteLine("Enter amount to withdraw");
        //    //decimal amount = decimal.Parse(Console.ReadLine());

        //    //return bankAccountService.Deposit(accountNumber, amount);

        //}

        public void BorrowBook()
        {


        }
        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBankAccountService, BankAccountService>();

            return services.BuildServiceProvider();
        }
    }
    
}
