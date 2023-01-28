using Travello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travello.DataAccess.Repository.IRepository;
using Travello.Models;

namespace Travello.DataAccess.Repository.IRepository
{
    public interface IGuideRepository:IRepository<Guide>
    {
        void Update(Guide obj);
    }
}
