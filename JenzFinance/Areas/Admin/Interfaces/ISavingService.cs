using JenzFinance.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenzFinance.Areas.Admin.Interfaces
{
    public interface ISavingService
    {
        List<SavingCategoryVM> GetSavingCategories();
        bool CreateSavingCategory(SavingCategoryVM Vmodel);
        SavingCategoryVM GetSavingCategory(int ID);
        bool EditSavingCategory(SavingCategoryVM Vmodel);
        bool DeleteSavingCategory(int ID);
        List<SavingCategoryVM> GetSubCategories();
        bool CreateSubCategory(SavingCategoryVM Vmodel);
        SavingCategoryVM GetSubCategory(int ID);
        bool EditSubCategory(SavingCategoryVM Vmodel);
        bool DeleteSubCategory(int ID);

        bool ActivateSubCategory(int ID);
        SavingRecordVM GetSavingRecord(int SavingID);
        bool UpdateSavingRecord(SavingRecordVM vmodel);
    }
}
