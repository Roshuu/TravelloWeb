using Travello.DataAccess.Repository.IRepository;
using Travello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Travello.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementTicket(ShoppingCart shoppingCart, int Ticket)
        {
            shoppingCart.Ticket -= Ticket;
            return shoppingCart.Ticket;
        }

        public int IncrementTicket(ShoppingCart shoppingCart, int Ticket)
        {
            shoppingCart.Ticket += Ticket;
            return shoppingCart.Ticket;
        }
    }
}
