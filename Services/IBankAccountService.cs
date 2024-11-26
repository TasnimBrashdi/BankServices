using ServicesLab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLab1.Services
{
    public interface IBankAccountService
    {
        IEnumerable<BankAccount> GetAllAccounts();
        BankAccount GetAccountById(int accountId);
        void AddAccount(BankAccount account);
        void UpdateAccount(BankAccount account);
        void DeleteAccount(int accountId); 

        decimal BalanceInquiry(int accountId);

    }
}
