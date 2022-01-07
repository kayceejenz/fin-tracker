using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenzFinance.DAL.Entity
{
   public  class SavingCategory
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }
        public int? NumberOfMonths { get; set; }
        public decimal AmountPerMonth { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActivated { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("ParentID")]
        public SavingCategory Parent { get; set; }
    }
}
