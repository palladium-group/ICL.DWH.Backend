using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IProductDetailRepository _productDetailRepository;

        public ProductDetailService(IProductDetailRepository productDetailRepository)
        {
            _productDetailRepository = productDetailRepository;
        }

        public ProductDetail GetProductDetailByProductCode(string productCode)
        {
            try
            {
                return _productDetailRepository.GetAll(x => x.trade_item_code == productCode).FirstOrDefault();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
