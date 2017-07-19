using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Model.DAO
{
    public class ProductCategoryDAO
    {
        static ProductCategoryDAO instance;
        static object key = new object();
        static MobileShopDbContext db;

        public static ProductCategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new ProductCategoryDAO();
                        db = new MobileShopDbContext();
                    }
                }
                return instance;
            }
        }

        ProductCategoryDAO(){ }

        public List<ProductCategory> GetListAll()
        {
            return db.ProductCategories.OrderBy(x => x.DisplayOrder).ToList();
        }

        public bool Create(ProductCategory category)
        {
            try
            {
                category.CreatedDate = DateTime.Now;
                db.ProductCategories.Add(category);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public ProductCategory GetDetail(int id)
        {
            return db.ProductCategories.Find(id);
        }

        public bool CheckNameIsExist(string name)
        {
            return db.ProductCategories.Count(x => x.Name == name.Trim()) > 0;
        }

        public bool Update(ProductCategory category)
        {
            try
            {
                var temp = GetDetail(category.Id);
                temp.Name = category.Name;
                temp.ParentID = category.ParentID;
                temp.DisplayOrder = category.DisplayOrder;
                temp.MetaTitle = category.MetaTitle;
                temp.MetaKeywords = category.MetaKeywords;
                temp.MetaDescriptions = category.MetaDescriptions;
                temp.Status = category.Status;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(string id)
        {
            try
            {
                var ProductCategory = GetDetail(int.Parse(id));
                if (ProductCategory != null)
                {
                    db.ProductCategories.Remove(ProductCategory);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
