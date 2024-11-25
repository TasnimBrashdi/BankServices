using ServicesLab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLab1.Services
{
    public interface ITranscationService
    {

        IEnumerable<Transaction> GetAll();
        Transaction GetTranscationById(int Id);
        void AddTranscation(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(int TId);
    }
}
