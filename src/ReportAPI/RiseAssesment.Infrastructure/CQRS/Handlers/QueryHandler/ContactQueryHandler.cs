using AutoMapper;
using MediatR;
using MongoDB.Driver;
using RiseAssesment.Infrastructure.CQRS.Queries;
using RiseAssesment.Infrastructure.CQRS.Queries.Request;
using RiseAssesment.Infrastructure.CQRS.Queries.Response;
using RiseAssesment.Infrastructure.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Handlers.QueryHandler
{
    public class ContactQueryHandler :
         IRequestHandler<GetContactQueryRequest, ContactQueryResponse>,
        IRequestHandler<ListContactQueryRequest, IEnumerable<ListContactQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public ContactQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<ContactQueryResponse> Handle(GetContactQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"contact_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<ContactQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var contact = await _context
                .Contact
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<ContactQueryResponse>(contact);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));
            return result;
        }

        public async Task<IEnumerable<ListContactQueryResponse>> Handle(ListContactQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "contact";
            IFindFluent<Contact, Contact>? query;

            if (string.IsNullOrEmpty(request.DirectoryId))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListContactQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.Contact.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.Contact.Find(x => x.DirectoryId==request.DirectoryId);
            }

            var contact = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListContactQueryResponse>>(contact);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
