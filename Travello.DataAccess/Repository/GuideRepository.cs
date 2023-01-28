using Travello.DataAccess.Repository.IRepository;
using Travello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Travello.DataAccess.Repository.IRepository;

namespace Travello.DataAccess.Repository
{
    public class GuideRepository : Repository<Guide>, IGuideRepository
    { 
        private ApplicationDbContext _db;

        public GuideRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        

        public void Update(Guide obj)
        {
            var objFromDb = _db.Guides.FirstOrDefault(p => p.Id == obj.Id);

            if (objFromDb != null)
            {
                objFromDb.Name= obj.Name;
                objFromDb.Surname= obj.Surname;
                objFromDb.Description = obj.Description;
                if(obj.ImageUrl!= null)
                {
                    objFromDb.ImageUrl= obj.ImageUrl;
                }
                
            }
            
        }
    }
}
