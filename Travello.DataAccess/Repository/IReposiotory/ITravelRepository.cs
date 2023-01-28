using Travello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travello.DataAccess.Repository.IRepository;

namespace Travello.DataAccess.Repository.IRepository
{
    public interface ITravelRepository:IRepository<Travel>
    {
        void Update(Travel obj);
    }
}
