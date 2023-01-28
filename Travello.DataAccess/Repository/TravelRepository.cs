using Travello.DataAccess.Repository.IRepository;
using Travello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Travello.DataAccess;
using Travello.DataAccess.Repository;

namespace Travello.DataAccess.Repository
{
    public class TravelRepository : Repository<Travel>, ITravelRepository
    { 
        private ApplicationDbContext _db;

        public TravelRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        

        public void Update(Travel obj)
        {
            var objFromDb = _db.Travels.FirstOrDefault(p => p.Id == obj.Id);

            if (objFromDb != null)
            {
                objFromDb.Name= obj.Name;
                objFromDb.Country= obj.Country;
                objFromDb.Price = obj.Price;
                objFromDb.City = obj.City;
                objFromDb.TravelTime = obj.TravelTime;
                objFromDb.GuideId = obj.GuideId;
                objFromDb.Description = obj.Description;
                if(obj.ImageUrl!= null)
                {
                    objFromDb.ImageUrl= obj.ImageUrl;
                }
                
            }
            
        }

       
    }
}
