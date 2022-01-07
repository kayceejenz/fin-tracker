using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenzFinance.DAL.Entity
{
    public class SavingRecord
    {
        public int Id { get; set; }
        public int? SavingCatID { get; set; }
        public string Month { get; set; }
        public decimal Amount { get; set; }
        public bool HasPaid { get; set; }
        public DateTime? DatePaid { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("SavingCatID")]
        public SavingCategory SavingCategory { get; set; }
    }
}
