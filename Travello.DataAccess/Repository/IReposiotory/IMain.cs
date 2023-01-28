using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travello.DataAccess.Repository.IRepository;

namespace Travello.DataAccess.Repository.IRepository
{
    public interface IMain
    {
        IGuideRepository Guide { get; }
        ITravelRepository Travel { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        
        void Save();
    }
}
