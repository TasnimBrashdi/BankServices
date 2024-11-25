using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLab1.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public decimal Balance { get; set; }

        //navigation property
        [ForeignKey(nameof(Owner))]
        public int UserId { get; set; }
        public virtual User Owner { get; set; }
    }
}
