using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLab1.Repositories
{
    public class TranscationRepository
    {
        private readonly ApplicationDbContext _context;

        public TranscationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
