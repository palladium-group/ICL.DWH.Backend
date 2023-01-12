using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetProductsByPOUUID(Guid PoUuid)
        {
            List<Product> products = _productRepository.GetAll().Where(x => x.PoUuid == PoUuid).ToList();
            return products;
        }

        public IEnumerable<Product> SaveProducts(IEnumerable<Product> products)
        {
            List<Product> savedproducts = new List<Product>();
            try
            {
                foreach (var product in products)
                {
                    savedproducts.Add(_productRepository.Create(product));
                }

                return savedproducts;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
