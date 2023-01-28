using Travello.DataAccess.Repository.IRepository;
using Travello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travello.DataAccess.Repository;


namespace Travello.DataAccess.Repository
{
    public class Main : IMain
    {

        private ApplicationDbContext _db;

        public Main(ApplicationDbContext db)
        {
            _db = db;
            Travel = new TravelRepository(_db);
            Guide= new GuideRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db); 
           
        }

        public IGuideRepository Guide { get; private set; }
        public ITravelRepository Travel { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
