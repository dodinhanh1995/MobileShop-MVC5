using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class SlideDAO
    {
        static SlideDAO instance;
        static object key = new object();
        static MobileShopDbContext db;

        public static SlideDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (key)
                    {
                        instance = new SlideDAO();
                        db  = new MobileShopDbContext();
                    }
                }
                return instance;
            }
            
        }

        SlideDAO() { }

        public List<Slide> GetListAll()
        {
            return db.Slides.OrderBy(x=>x.DisplayOrder).ToList();
        }

        public bool Create(Slide slide)
        {
            try
            {
                slide.CreatedDate = DateTime.Now;
                db.Slides.Add(slide);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Slide GetDetail(int id)
        {
            return db.Slides.Find(id);
        }

        public bool CheckNameIsExist(string name)
        {
            return db.Slides.Count(x => x.Name == name) > 0;
        }
        
        public bool Update(Slide slide)
        {
            try
            {
                    db.SaveChanges();
                    return true;
            }
            catch { return false; }

        }

        public bool Delete(string id)
        {
            try
            {
                var slide = db.Slides.Find(int.Parse(id));
                if (slide != null)
                {
                    db.Slides.Remove(slide);
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
