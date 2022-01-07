using JenzFinance.Areas.Admin.Helpers;
using JenzFinance.Areas.Admin.Interfaces;
using JenzFinance.Areas.Admin.ViewModels;
using JenzFinance.DAL.DataConnection;
using JenzFinance.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace JenzFinance.Areas.Admin.Services
{
    public class SavingService : ISavingService
    {
        /* Instancation of the database context model
    * and injecting some buisness layer services
    */
        #region Instanciation
        private readonly DatabaseEntities _db;
        public SavingService()
        {
            _db = new DatabaseEntities();
        }
        public SavingService(DatabaseEntities db)
        {
            _db = db;
        }
        #endregion

        public string[] Months = { "JANUARY", "FEBRARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMEBER", "DECEMBER" };

        /* ********************************************************************************************************** */
        // Saving category


        // Fetching saving categories
        public List<SavingCategoryVM> GetSavingCategories()
        {
            var model = _db.SavingCategories.Where(x => x.IsDeleted == false && x.ParentID == null).Select(b => new SavingCategoryVM()
            {
                Id = b.Id,
                Description = b.Description,
            }).ToList();
            return model;
        }

        // Creating saving category
        public bool CreateSavingCategory(SavingCategoryVM Vmodel)
        {
            bool hasSaved = false;
            SavingCategory model = new SavingCategory()
            {
                Description = Vmodel.Description,
                DateCreated = DateTime.Now,
                IsDeleted = false,
            };
            _db.SavingCategories.Add(model);
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }

        // Getting saving category
        public SavingCategoryVM GetSavingCategory(int ID)
        {
            var model = _db.SavingCategories.Where(x => x.Id == ID).Select(b => new SavingCategoryVM()
            {
                Id = b.Id,
                Description = b.Description,
            }).FirstOrDefault();
            return model;
        }

        // Editting and updating saving category
        public bool EditSavingCategory(SavingCategoryVM Vmodel)
        {
            bool hasSaved = false;
            SavingCategory model = _db.SavingCategories.FirstOrDefault(x => x.Id == Vmodel.Id);
            model.Description = Vmodel.Description;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }

        // Deleting saving category
        public bool DeleteSavingCategory(int ID)
        {
            bool hasSaved = false;
            var model = _db.SavingCategories.FirstOrDefault(x => x.Id == ID);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }

        /* ************************************************************************************************* */
        // Sub categories
        public List<SavingCategoryVM> GetSubCategories()
        {


            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            var model = _db.SavingCategories.Where(x => x.IsDeleted == false && x.ParentID != null).Select(b => new SavingCategoryVM()
            {
                Id = b.Id,
                Description = b.Description,
                AmountPerMonth = b.AmountPerMonth,
                NumberOfMonths = b.NumberOfMonths,
                ParentID = b.ParentID,
                IsActivated = b.IsActivated
            }).ToList();
            foreach (var each in model)
            {
                each.AmountPerMonthString = "₦" + each.AmountPerMonth.ToString("N", nfi);
            }
            return model;
        }

        // Creating sub content category
        public bool CreateSubCategory(SavingCategoryVM Vmodel)
        {
            bool hasSaved = false;
            SavingCategory model = new SavingCategory()
            {
                Description = Vmodel.Description,
                DateCreated = DateTime.Now,
                IsDeleted = false,
                AmountPerMonth = CustomSerializer.UnMaskString(Vmodel.AmountPerMonthString),
                ParentID = Vmodel.ParentID,
                NumberOfMonths = Vmodel.NumberOfMonths
            };
            _db.SavingCategories.Add(model);
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }

        // Getting sub content category
        public SavingCategoryVM GetSubCategory(int ID)
        {
            var model = _db.SavingCategories.Where(x => x.Id == ID).Select(b => new SavingCategoryVM()
            {
                Id = b.Id,
                Description = b.Description,
                AmountPerMonth = b.AmountPerMonth,
                NumberOfMonths = b.NumberOfMonths,
                ParentID = b.ParentID
            }).FirstOrDefault();
            return model;
        }

        // Editting and updating sub content category
        public bool EditSubCategory(SavingCategoryVM Vmodel)
        {
            bool hasSaved = false;
            SavingCategory model = _db.SavingCategories.FirstOrDefault(x => x.Id == Vmodel.Id);
            model.Description = Vmodel.Description;
            model.AmountPerMonth = Vmodel.AmountPerMonth;
            model.ParentID = Vmodel.ParentID;

            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }

        // Deleting sub content category
        public bool DeleteSubCategory(int ID)
        {
            bool hasSaved = false;
            var model = _db.SavingCategories.FirstOrDefault(x => x.Id == ID);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }
        public bool ActivateSubCategory(int ID)
        {
            bool hasSaved = false;
            var model = _db.SavingCategories.FirstOrDefault(x => x.Id == ID);
            model.IsActivated = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            PrepareSavingRecord(ID);

            hasSaved = true;
            return hasSaved;
        }


        // Prepare Saving Record
        public void PrepareSavingRecord(int SavingID)
        {
            try
            {
                var savingCategory = _db.SavingCategories.FirstOrDefault(x => x.Id == SavingID);

                for (int i = 0; i < savingCategory.NumberOfMonths; i++)
                {
                    var model = new SavingRecord()
                    {
                        SavingCatID = SavingID,
                        Amount = savingCategory.AmountPerMonth,
                        Month = Months[i],
                        IsDeleted = false,
                        DateCreated = DateTime.Now,
                    };
                    _db.SavingRecords.Add(model);
                }
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public SavingRecordVM GetSavingRecord(int SavingID)
        {
            SavingRecordVM model = new SavingRecordVM();
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            var records = _db.SavingRecords.Where(x => x.IsDeleted == false && x.SavingCatID == SavingID).Select(b => new SavingRecordVM()
            {
                Id = b.Id,
                SavingCatID = b.SavingCatID,
                SavingDescription = b.SavingCategory.Description,
                Month = b.Month,
                Amount = b.Amount,
                HasPaid = b.HasPaid,
                DatePaid = b.DatePaid,
            }).ToList();
            foreach (var each in records)
            {
                each.AmountString = "₦" + each.Amount.ToString("N", nfi);
            }
            model.TableData = records;
            model.TotalAmountToSave = "₦" + records.Select(b => b.Amount).Sum().ToString("N", nfi);
            model.TotalAmountSaved = "₦" + records.Where(x => x.HasPaid == true).Select(b => b.Amount).Sum().ToString("N", nfi);
            model.SavingCat = _db.SavingCategories.Where(x => x.Id == SavingID).FirstOrDefault();
            return model;
        }

        public bool UpdateSavingRecord(SavingRecordVM vmodel)
        {
            try
            {
                var model = _db.SavingRecords.FirstOrDefault(x => x.Id == vmodel.Id);
                model.HasPaid = vmodel.HasPaid;
                model.DatePaid = vmodel.DatePaid;

                _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}