using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProductsByPOUUID(Guid PoUuid);
        IEnumerable<Product> SaveProducts(IEnumerable<Product> products);
    }
}
