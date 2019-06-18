using AutoMapper;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Create
{
    public class CreateGroupCommand : IRequest
    {
        public GroupDetailModel Model { get; set; }

        public class Handler : IRequestHandler<CreateGroupCommand, Unit>
        {
            private readonly IGroupsRepository groupsRepo;
            private readonly IMediator mediator;
            private readonly IMapper mapper;

            public Handler(IGroupsRepository repo, IMediator mediator, IMapper mapper)
            {
                this.groupsRepo = repo;
                this.mediator = mediator;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                var mappedObj = mapper.Map<Group>(request.Model);
                await groupsRepo.InsertAsync(mappedObj);
                await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.CREATE, nameof(Group))
                {
                    Id = mappedObj.Id,
                    Title = mappedObj.Title
                }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
