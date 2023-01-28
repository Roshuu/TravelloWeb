using Travello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travello.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int IncrementTicket(ShoppingCart shoppingCart, int Ticket);
        int DecrementTicket(ShoppingCart shoppingCart, int Ticket);

    }
}
