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

        public async Task<ActionResult> AddProduct(SellerProduct product)
        {
            try
            {
                await _sellerCollection.InsertOneAsync(product);
                return new OkObjectResult("Product Added Successfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
                throw;
            }
        }

        public async Task<ActionResult> DeleteProductAsync(ObjectId id)
        {
            try
            {
                await _sellerCollection.FindOneAndDeleteAsync(Builders<SellerProduct>.Filter.Eq("_id", id));
                return new OkObjectResult("Product Deleted Successfully");
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(ex.Message);
                throw;
            }
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

        public async Task<ActionResult> UpdateProductAsync(ObjectId id, string email, string productName, string shortDescription, string detailedDescription, double startingPrice, DateTime bidEndDate)
        {
            try
            {
                await _sellerCollection.FindOneAndUpdateAsync(Builders<SellerProduct>.Filter.Eq("_id", id), Builders<SellerProduct>.Update.Set("email", email).Set("productName", productName).Set("shortDescription", shortDescription).Set("detailedDescription", detailedDescription).Set("startingPrice", startingPrice).Set("bidEndDate", bidEndDate));
                return new OkObjectResult("Product updated successfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
                throw;
            }
        }
    }
}
