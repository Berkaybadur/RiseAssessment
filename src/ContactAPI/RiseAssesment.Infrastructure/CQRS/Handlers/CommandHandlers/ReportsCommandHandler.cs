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
    public class ReportsCommandHandler :
        IRequestHandler<CreateReportsCommandRequest, CreateReportsCommandResponse?>,
        IRequestHandler<UpdateReportsCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public ReportsCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<CreateReportsCommandResponse> Handle(CreateReportsCommandRequest request, CancellationToken cancellationToken)
        {
            var reports = _mapper.Map<Reports>(request);
            await _context.Reports.InsertOneAsync(reports, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("report");

            return new CreateReportsCommandResponse
            {
                Id = reports.Id
            };

        }

        public async Task<EmptyResponse> Handle(UpdateReportsCommandRequest request, CancellationToken cancellationToken)
        {
            var isreportsExists = await _context.Reports.CountDocumentsAsync(x => x.Id == request.Id,
                            cancellationToken: cancellationToken) > 0;

            if (!isreportsExists)
                return null;

            var filter = Builders<Reports>.Filter.Eq("Id", request.Id);
            var update = Builders<Reports>.Update
                .Set("ReportStatus", request.ReportStatus);

            var result = await _context.Reports.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "report", $"report_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
