using AutoMapper;
using MediatR;
using MongoDB.Driver;
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
    public class DirectoryQueryHandler :
         IRequestHandler<GetDirectoryQueryRequest, DirectoryQueryResponse>,
        IRequestHandler<ListDirectoryQueryRequest, IEnumerable<ListDirectoryQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public DirectoryQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<DirectoryQueryResponse> Handle(GetDirectoryQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"directory_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<DirectoryQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var directory = await _context
                .Directory
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<DirectoryQueryResponse>(directory);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListDirectoryQueryResponse>> Handle(ListDirectoryQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "directory";
            IFindFluent<Directory, Directory>? query;
            if (string.IsNullOrEmpty(request.Name))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListDirectoryQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.Directory.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.Directory.Find(x => x.Name != null && x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var directory = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListDirectoryQueryResponse>>(directory);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
