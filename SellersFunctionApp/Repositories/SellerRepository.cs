using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SellersFunctionApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellersFunctionApp.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly IMongoCollection<SellerProduct> _sellerCollection;

        public SellerRepository(IMongoDatabase database)
        {
            _sellerCollection = database.GetCollection<SellerProduct>("sellerproduct");
        }

        public async Task<ActionResult<IEnumerable<SellerProduct>>> AddProduct(SellerProduct product)
        {
            try
            {
                await _sellerCollection.InsertOneAsync(product);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
                throw;
            }
            return new OkObjectResult(GetAllProduuctsAsync());
        }

        public async Task<ActionResult<IEnumerable<SellerProduct>>> GetAllProduuctsAsync()
        {
            IEnumerable<SellerProduct> product = null;
            try
            {
                product = await _sellerCollection.Find(_ => true).ToListAsync<SellerProduct>();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
                throw;
            }
            return new OkObjectResult(product);
        }

        public async Task<ActionResult<SellerProduct>> GetSingleProductDetailsAsync(ObjectId id)
        {
            try
            {
                var product = await _sellerCollection.Find(_ => _.ProductId.Equals(id)).ToListAsync<SellerProduct>();
                return new OkObjectResult(product);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
                throw;
            }
        }
    }
}
