using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SellersFunctionApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellersFunctionApp.Repositories
{
    public interface ISellerRepository
    {
        Task<ActionResult<IEnumerable<SellerProduct>>> GetAllProduuctsAsync();

        Task<ActionResult> AddProduct(SellerProduct product);

        Task<ActionResult<SellerProduct>> GetSingleProductDetailsAsync(ObjectId id);

        Task<ActionResult> DeleteProductAsync(ObjectId id);

        Task<ActionResult> UpdateProductAsync(ObjectId id, string email, string productName, string shortDescription, string detailedDescription, double startingPrice, DateTime bidEndDate);

    }
}
