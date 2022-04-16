﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.Models
{
    public class MongoDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _mongoDatabase;
        public MongoDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetValue<string>("MongoDBConfiguration:ConnectionString");
            var db = _configuration.GetValue<string>("MongoDBConfiguration:Database");
            var client = new MongoClient(connectionString);
            _mongoDatabase = client.GetDatabase(db);
        }
        public IMongoCollection<Directory> Directory => _mongoDatabase.GetCollection<Directory>(nameof(Directory));
        public IMongoCollection<Contact> Contact => _mongoDatabase.GetCollection<Contact>(nameof(Contact));
        public IMongoCollection<Reports> Reports => _mongoDatabase.GetCollection<Reports>(nameof(Reports));


    }
}
