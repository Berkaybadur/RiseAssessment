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

namespace RiseAssesment.Infrastructure.CQRS.Handlers
{
    public class ContactCommandHandler :
        IRequestHandler<CreateContactCommandRequest, CreateContactCommandResponse?>,
        IRequestHandler<DeleteContactCommandRequest, EmptyResponse?>,
        IRequestHandler<UpdateContactCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public ContactCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<CreateContactCommandResponse> Handle(CreateContactCommandRequest request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);
            var isCategoryExists = await _context.Contact.CountDocumentsAsync(x => x.Email == request.Email,
                cancellationToken: cancellationToken) > 0;

            if (isCategoryExists)
                return null;

            await _context.Contact.InsertOneAsync(contact, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("contact");

            return new CreateContactCommandResponse
            {
                Id = contact.Id
            };
        }

        public async Task<EmptyResponse> Handle(DeleteContactCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<Contact>.Filter.Eq("Id", request.Id);
            if (!string.IsNullOrEmpty(request.Id))
                filter = Builders<Contact>.Filter.Eq("Id", request.Id);
            if (!string.IsNullOrEmpty(request.DirectoryId))
                filter = Builders<Contact>.Filter.Eq("DirectoryId", request.DirectoryId);

            var result = await _context.Contact.DeleteOneAsync(filter, cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "contact", $"contact_{request.Id}" });
            return EmptyResponse.Default;
        }

        public async Task<EmptyResponse> Handle(UpdateContactCommandRequest request, CancellationToken cancellationToken)
        {
            var isContactExists = await _context.Contact.CountDocumentsAsync(x => x.Id == request.Id,
                 cancellationToken: cancellationToken) > 0;

            if (!isContactExists)
                return null;

            var filter = Builders<Contact>.Filter.Eq("Id", request.Id);
            var update = Builders<Contact>.Update
                .Set("Email", request.Email)
                .Set("Location", request.Location)
                .Set("PhoneNumber", request.PhoneNumber);


            var result = await _context.Contact.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "contact", $"contact_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
