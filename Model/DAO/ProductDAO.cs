using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PagedList;

namespace Model.DAO
{
    public class ProductDAO
    {
        static ProductDAO instance;
        static object key = new object();
        static MobileShopDbContext db;

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new ProductDAO();
                        db = new MobileShopDbContext();
                    }
                }
                return instance;
            }
        }

        ProductDAO()
        { }

        public IPagedList<Product> GetListAll(string sortOrder, string searchString, int? page)
        {
            var products = from p in db.Products select p;
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString) || x.ProductCategory.Name == searchString);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(x => x.Name);
                    break;
                case "Date":
                    products = products.OrderBy(x => x.CreatedDate);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(x => x.CreatedDate);
                    break;
                default:
                    products = products.OrderBy(x => x.Name);
                    break;
            }

            return products.ToPagedList(page ?? 1, 10);
        }

        public bool Create(Product product)
        {
            try
            {
                product.CreatedDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Product GetDetail(int id)
        {
            return db.Products.Find(id);
        }

        public bool CheckNameIsExist(string name)
        {
            return db.Products.Count(x => x.Name == name.Trim()) > 0;
        }

        public bool Update(Product product)
        {
            try
            {
                db.Set<Product>().AddOrUpdate(product);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(string id)
        {
            try
            {
                var product = db.Products.Find(int.Parse(id));
                if (product != null)
                {
                    db.Products.Remove(product);
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

        public string ChangeImage(int id, string image)
        {
            var product = GetDetail(id);
            if (product == null)
                return "Thông tin sản phẩm không tồn tại!";
            product.Image = image;
            db.SaveChanges();
            return "";
        }

    }
}
