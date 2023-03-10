using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockPractice.Domain
{
    public class BankAccount
    {
        [Key]
        public Guid Id { get; set; }
        public char[] AccountNo { get; set; } = new char[9];
        public decimal Balance { get; set; }
        public Guid CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
