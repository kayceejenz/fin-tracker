using JenzFinance.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JenzFinance.Areas.Admin.ViewModels
{
    public class SavingRecordVM
    {
        public int Id { get; set; }
        public int? SavingCatID { get; set; }
        public string Month { get; set; }
        public decimal Amount { get; set; }
        public string AmountString { get; set; }
        public bool HasPaid { get; set; }
        public DateTime? DatePaid { get; set; }
        public bool IsDeleted { get; set; }
        public string SavingDescription { get; set; }
        public string TotalAmountToSave { get; set; }
        public string TotalAmountSaved { get; set; }
        public List<SavingRecordVM> TableData { get; set; }
        public SavingCategory SavingCat { get; set; }
    }
}