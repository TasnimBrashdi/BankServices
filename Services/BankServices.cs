using Microsoft.Identity.Client;
using ServicesLab1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ServicesLab1.Services
{
    public class BankServices 
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ITranscationRepository _transactionRepository;
        public BankServices(IBankAccountRepository bankAccountRepository, ITranscationRepository transactionRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _transactionRepository = transactionRepository;
        }

        public void Deposit(int accountId, decimal amount)
        {
            var account = _bankAccountRepository.GetAccountById(accountId);
            if (account != null)
            {
                account.Balance += amount;
                _bankAccountRepository.UpdateAccount(account);
          

            }
            else
            {
                throw new Exception("Account not found.");
            }



        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = _bankAccountRepository.GetAccountById(accountId);
            if (account != null)
            {
                if (account.Balance >= amount)
                {
                    account.Balance -= amount;
                    _bankAccountRepository.UpdateAccount(account);
                }
                else
                {
                    throw new Exception("Insufficient funds.");
                }
            }
            else
            {
                throw new Exception("Account not found.");
            }
        }
        public void Transfer(int ac1, int ac2,decimal amount)
        {

            Withdraw(ac1, amount);
            Deposit(ac2, amount);



        }
    }
}
