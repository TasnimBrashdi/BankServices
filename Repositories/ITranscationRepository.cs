using ServicesLab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLab1.Repositories
{
    public interface ITranscationRepository
    {
        IEnumerable<Transaction> GetAll();
        Transaction GetTranscationById(int Id);
        void AddTranscation(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(int tId);

    }
}
