using Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using ServicesLab1.Models;
using ServicesLab1.Repositories;
using ServicesLab1.Services;
using System;
using System.Text;

namespace ServicesLab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            using var scope = serviceProvider.CreateScope();


            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var bankAccountService = scope.ServiceProvider.GetRequiredService<IBankAccountService>();
            var TranscationService = scope.ServiceProvider.GetRequiredService<ITranscationService>();
            var BankService = scope.ServiceProvider.GetRequiredService<IBankServices>();


            Console.WriteLine("Welcome to the Bank System");
            while (true)
            {
                Console.WriteLine("\n1. Add New user and Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateUser(userService);
                        AddBankAccount(userService, bankAccountService);
                        break;

                    case "2":
                        DepositTransaction(bankAccountService, BankService, TranscationService);
                        break;

                    case "3":
                        WithdrowTransaction(bankAccountService, BankService, TranscationService);
                        break;

                    case "5":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }



        }


        public static void CreateUser(IUserService userService)
        {


            Console.WriteLine("Enter User Name:");
            string Name = Console.ReadLine();

            Console.WriteLine("Enter User Email:");
            string Email = Console.ReadLine();



            var user = new User
            {
                Name = Name,

                Email = Email
            };
            userService.AddUser(user);
            Console.WriteLine($"User created successfully with ID: {user.Id}");



        }


        public static string GenerateAccount(int length = 10)
        {
            var random = new Random();
            var accountNumber = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                accountNumber.Append(random.Next(0, 10));
            }

            return accountNumber.ToString();
        }




        public static void AddBankAccount(IUserService userService, IBankAccountService bankAccountService)
        {
            Console.WriteLine("Enter User ID ");
            if (int.TryParse(Console.ReadLine(), out var userId))
            {
                var user = userService.GetUserById(userId);
                if (user != null)
                {
                    Console.WriteLine($"User Found: {user.Name}, Email: {user.Email}");


                    var accountNumber = GenerateAccount();
                    Console.WriteLine($"Generated Account Number: {accountNumber}");

                    Console.WriteLine("Enter Initial Balance:");
                    if (decimal.TryParse(Console.ReadLine(), out var initialBalance))
                    {
                        var bankAccount = new BankAccount
                        {
                            AccountNumber = accountNumber,
                            Balance = initialBalance,
                            UserId = user.Id
                        };

                        bankAccountService.AddAccount(bankAccount);
                        Console.WriteLine($"Bank account created successfully for User ID: {user.Id} with Account Number: {accountNumber}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid balance amount.");
                    }
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid User ID.");
            }
        }


        public static void DepositTransaction(IBankAccountService bankAccountService, IBankServices bankServices, ITranscationService transactionService)
        {

            Console.Write("Enter Account ID: ");
            int accountID = int.Parse(Console.ReadLine());

            Console.Write("Enter Amount: ");
            if (decimal.TryParse(Console.ReadLine(), out var amount))
            {
                var account = bankAccountService.GetAccountById(accountID);
                bankServices.Deposit(accountID, amount);
                Console.WriteLine($"Deposit successful. New Balance: {account.Balance}");
                bankAccountService.UpdateAccount(account);
                transactionService.AddTranscation(new Transaction
                {
                    sourceAccNumber = account.AccountNumber,
                    amount = amount,
                    operation = "Deposit",
                    AId = account.Id
                });

            }
            else
            {
                Console.WriteLine("INVAILD");


            }
        }


        public static void WithdrowTransaction(IBankAccountService bankAccountService, IBankServices bankServices, ITranscationService transactionService)
        {
            Console.Write("Enter Account ID: ");
            int accountID = int.Parse(Console.ReadLine());

            Console.Write("Enter Amount: ");
            if (decimal.TryParse(Console.ReadLine(), out var amount))
            {
                var account = bankAccountService.GetAccountById(accountID);
                bankServices.Withdraw(accountID, amount);
                Console.WriteLine($"Withdrawal successful. New Balance: {account.Balance}");

                        bankAccountService.UpdateAccount(account);
                        transactionService.AddTranscation(new Transaction
                        {
                            sourceAccNumber = account.AccountNumber,
                            amount = amount,
                            operation = "Withdrawal",
                            AId = account.Id
                        });

                    }
                    else
                    {
                        Console.WriteLine("INVAILD");

                    }
                

            
        }


        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ITranscationRepository, TranscationRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<ITranscationService, TranscationService>();


            return services.BuildServiceProvider();
        }


    }

}