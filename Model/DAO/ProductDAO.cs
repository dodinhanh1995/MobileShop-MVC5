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

        public IPagedList<Product> SearchList(string searchString, int? page)
        {
            var products = db.Products.Where(x => x.Name.Contains(searchString) || x.Code == searchString || x.ProductCategory.Name == searchString);
            return products.OrderByDescending(x=>x.ViewCount).ToPagedList(page ?? 1, 9);
        }

        /// <summary>
        /// Get new products by created date condition
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public List<Product> ListAll(int quantity = 4)
        {
            return db.Products.Where(x => x.Status && x.TopHot == null && x.Quantity > 0).OrderByDescending(x => x.CreatedDate).Take(quantity).ToList();
        }

        /// <summary>
        /// Get feature products by top hot condition
        /// </summary>
        /// <param name="topHot"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public List<Product> ListAll(bool topHot, int quantity = 4)
        {
            return db.Products.Where(x => x.Status && x.Quantity > 0 && x.TopHot != null).OrderByDescending(x => x.TopHot).Take(quantity).ToList();
        }

        public List<Product> GetProductByCategory(string categoryName)
        {
            return db.Products.Where(x => x.Status && x.ProductCategory.MetaTitle == categoryName).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public List<Product> GetRelatedProductById(int id)
        {
            var product = GetDetail(id);
            decimal plusThreeMilion = product.Price + 3000000;
            decimal minusThreeMilion = product.Price - 3000000;
            var temp = db.Products.Where(x => x.Status);
            var related = temp.Where(x => x.Price < plusThreeMilion && x.Price > minusThreeMilion && x.Id != product.Id).OrderByDescending(x => x.Price);
            if (related.Count() < 3)
            {
                related = temp.Where(x => x.CategoryID == product.CategoryID && x.Id != product.Id).OrderByDescending(x => x.Price);
            }
            return related.Take(5).ToList();
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).OrderByDescending(x => x.ViewCount).Select(x => x.Name).Take(6).ToList();
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
