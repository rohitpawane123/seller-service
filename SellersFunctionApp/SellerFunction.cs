using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Bson;
using SellersFunctionApp.Model;
using SellersFunctionApp.Repositories;
using System.Collections.Generic;

namespace SellersFunctionApp
{
    public class SellerFunction
    {
        private readonly ISellerRepository _seller;

        public SellerFunction(ISellerRepository seller)
        {
            _seller = seller;
        }

        [FunctionName("list-all-products")]
        public async Task<ActionResult<IEnumerable<SellerProduct>>> GetAllProducts([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger logger)
        {
            return new OkObjectResult(await _seller.GetAllProduuctsAsync());
        }

        [FunctionName("add-product")]
        public async Task<ActionResult<IEnumerable<SellerProduct>>> AddProduct([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger logger)
        {
            try
            {
                var reqBody = await new StreamReader(req.Body).ReadToEndAsync();
                var input = JsonConvert.DeserializeObject<SellerProduct>(reqBody);
                var product = new SellerProduct
                {
                    ProductName = input.ProductName,
                    Address = input.Address,
                    BidEndDate = input.BidEndDate,
                    Category = input.Category,
                    City = input.City,
                    DetailedDescription = input.DetailedDescription,
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Phone = input.Phone,
                    Pin = input.Pin,
                    ShortDescription = input.ShortDescription,
                    StartingPrice = input.StartingPrice,
                    State = input.State,
                };
                return new OkObjectResult(await _seller.AddProduct(product));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
                throw;
            }
        }

        [FunctionName("get-product-by-id")]
        public async Task<ActionResult<SellerProduct>> GetProductById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "get-product-by-id/{id}")] HttpRequest req, ILogger logger, string id)
        {
            return new OkObjectResult(await _seller.GetSingleProductDetailsAsync(ObjectId.Parse(id)));
        }

        [FunctionName("delete-product")]
        public async Task<ActionResult> DeleteProduct([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "delete-product/{id}")] HttpRequest request, ILogger logger, string id)
        {
            try
            {
                return new OkObjectResult(await _seller.DeleteProductAsync(ObjectId.Parse(id)));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
                throw;
            }
        }

        [FunctionName("update-product")]
        public async Task<ActionResult> UpdateProduct([HttpTrigger(AuthorizationLevel.Function, "put", Route = "update-product/{id}/{email}/{productName}/{shortDescription}/{detailedDescription}/{startingPrice}/{bidEndDate}")] HttpRequest request, ILogger logger, string id, string email, string productName, string shortDescription, string detailedDescription, double startingPrice, DateTime bidEndDate)
        {
            try
            {

                return new OkObjectResult(await _seller.UpdateProductAsync(ObjectId.Parse(id), email, productName, shortDescription, detailedDescription, startingPrice, DateTime.Now.AddDays(10)));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
                throw;
            }
        }
    }
}
