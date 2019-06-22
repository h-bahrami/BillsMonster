using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Queries.Detail
{
    public class GetGroupDetailQueryHandler : IRequestHandler<GetGroupDetailQuery, GroupDetailModel>
    {
        private readonly IGroupsRepository repo;

        public GetGroupDetailQueryHandler(IGroupsRepository repo)
        {
            this.repo = repo;
        }

        public async Task<GroupDetailModel> Handle(GetGroupDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await repo.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }
            return (GroupDetailModel)entity;
        }
    }
}

