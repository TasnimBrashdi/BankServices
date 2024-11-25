using Microsoft.EntityFrameworkCore;
using ServicesLab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLab1.Repositories
{
    public class TranscationRepository:ITranscationRepository
    {
        private readonly ApplicationDbContext _context;

        public TranscationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions.ToList();
        }

        public Transaction GetTranscationById(int TId)
        {
            return _context.Transactions.Include(t => t.BankAccounts).FirstOrDefault(t =>t.TId == TId);
        }

        public void AddTranscation(Transaction transcation)
        {
            _context.Transactions.Add(transcation);
            _context.SaveChanges();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }

        public void DeleteTransaction(int Id)
        {
            var transaction = _context.Transactions.Find(Id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
        }
    }
}
