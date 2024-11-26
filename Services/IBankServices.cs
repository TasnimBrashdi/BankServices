using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLab1.Services
{
    public interface IBankServices
    {

        void Deposit(int accountId, decimal amount);
        void Withdraw(int accountId, decimal amount);
        void Transfer(int ac1, int ac2, decimal amount);
    }
}
