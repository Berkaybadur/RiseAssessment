using AutoMapper;
using MediatR;
using MongoDB.Driver;
using RiseAssesment.Infrastructure.CQRS.Commands.Request;
using RiseAssesment.Infrastructure.CQRS.Commands.Response;
using RiseAssesment.Infrastructure.CQRS.Common;
using RiseAssesment.Infrastructure.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Handlers.CommandHandlers
{
    public class DirectoryCommandHandler :
        IRequestHandler<CreateDirectoryCommandRequest, CreateDirectoryCommandResponse?>,
        IRequestHandler<DeleteDirectoryCommandRequest, EmptyResponse?>,
        IRequestHandler<UpdateDirectoryCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public DirectoryCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<CreateDirectoryCommandResponse> Handle(CreateDirectoryCommandRequest request, CancellationToken cancellationToken)
        {
            var directory = _mapper.Map<Directory>(request);

            await _context.Directory.InsertOneAsync(directory, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("directory");

            return new CreateDirectoryCommandResponse
            {
                Id = directory.Id
            };
        }

        public async Task<EmptyResponse> Handle(DeleteDirectoryCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<Directory>.Filter.Eq("Id", request.Id);
            var result = await _context.Directory.DeleteOneAsync(filter, cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "directory", $"directory_{request.Id}" });
            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }

        public async Task<EmptyResponse> Handle(UpdateDirectoryCommandRequest request, CancellationToken cancellationToken)
        {
            var isCategoryExists = await _context.Directory.CountDocumentsAsync(x => x.Id == request.Id,
                 cancellationToken: cancellationToken) > 0;

            if (!isCategoryExists)
                return null;

            var filter = Builders<Directory>.Filter.Eq("Id", request.Id);
            var update = Builders<Directory>.Update
                .Set("Name", request.Name)
                .Set("Surname", request.Surname)
                .Set("Company", request.Company)
                .Set("Name", request.Name);

            var result = await _context.Directory.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "directory", $"directory_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
