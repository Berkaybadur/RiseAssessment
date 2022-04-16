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
    public class ReportsQueryHandler :
        IRequestHandler<GetReportsQueryRequest, ReportsQueryResponse>,
        IRequestHandler<ListReportsQueryRequest, IEnumerable<ListReportsQueryResponse>>

    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public ReportsQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<ReportsQueryResponse> Handle(GetReportsQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"report_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<ReportsQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var reports = await _context
                .Reports
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<ReportsQueryResponse>(reports);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListReportsQueryResponse>> Handle(ListReportsQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            IFindFluent<Reports, Reports>? query;
            query = _context.Reports.Find(x => x.CreateDate == request.CreateDate);
            var report = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListReportsQueryResponse>>(report);
            return result;
        }
    }
}
