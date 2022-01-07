using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JenzFinance.Areas.Admin.ViewModels
{
    public class SavingCategoryVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }
        public int? NumberOfMonths { get; set; }
        public decimal AmountPerMonth { get; set; }
        public string AmountPerMonthString { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string ParentDescription { get; set; }
        public bool IsActivated { get; set; }
    }
}