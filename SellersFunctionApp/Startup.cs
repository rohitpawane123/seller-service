﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SellersFunctionApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(SellersFunctionApp.Startup))]
namespace SellersFunctionApp
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IMongoDatabase>(_ =>
            {
                var client = new MongoClient("mongodb://eauctin-db:m14BXrm8gLVvW7ngdnT7dAxsqCtsHtjB9cIXRNuXRWiOMctHo2rmVAlbuGapfPhDbCAvr7SxnraWMJsbp8Dz8Q==@eauctin-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@eauctin-db@\r\n");
                return client.GetDatabase("eauctin-db");
            });

            builder.Services.AddSingleton<ISellerRepository, SellerRepository>();
        }
    }
}
