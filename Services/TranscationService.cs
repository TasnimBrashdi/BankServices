using Microsoft.EntityFrameworkCore;
using ServicesLab1.Models;
using ServicesLab1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLab1.Services
{
    public class TranscationService: ITranscationService
    {

        private readonly ITranscationRepository _transcationRepository;

        public TranscationService(ITranscationRepository transcationRepository)
        {
            _transcationRepository= transcationRepository;
        }


        public IEnumerable<Transaction> GetAll()
        {
            return _transcationRepository.GetAll();
        }

        public Transaction GetTranscationById(int TId)
        {
            return _transcationRepository.GetTranscationById(TId);
        }

        public void AddTranscation(Transaction transcation)
        {
            _transcationRepository.AddTranscation(transcation); 
        }

        public void UpdateTransaction(Transaction transaction)
        {
            var existingTran = _transcationRepository.GetTranscationById(transaction.TId);
            if (existingTran != null)
            {

                existingTran.sourceAccNumber = transaction.sourceAccNumber;
                existingTran.amount = transaction.amount;
                existingTran.operation = transaction.operation;
                existingTran.aId = transaction.aId;

          
                _transcationRepository.UpdateTransaction(existingTran);
            }
            else
            {
                throw new Exception("ERROR TRANSCATION.");
            }
        }

        public void DeleteTransaction(int Id)
        {
            _transcationRepository.DeleteTransaction(Id);


        }
    }
}