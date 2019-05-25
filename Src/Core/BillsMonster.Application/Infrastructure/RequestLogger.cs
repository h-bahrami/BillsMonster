using BillsMonster.Domain.Entities;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Infrastructure
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            _logger.LogInformation($"ToDoList Request: {name} {request}");
            return Task.CompletedTask;
        }
    }
}
