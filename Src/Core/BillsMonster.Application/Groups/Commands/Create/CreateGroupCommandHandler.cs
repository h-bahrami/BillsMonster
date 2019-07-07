using AutoMapper;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Create
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
    {
        private readonly IGroupsRepository groupsRepo;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateGroupCommandHandler(IGroupsRepository repo, IMediator mediator, IMapper mapper)
        {
            this.groupsRepo = repo;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var mappedObj = mapper.Map<Group>(request);
            mappedObj.RecordTime = System.DateTime.Now;

            await groupsRepo.InsertAsync(mappedObj);
            await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.CREATE, nameof(Group))
            {
                Id = mappedObj.Id,
                Title = mappedObj.Title
            }, cancellationToken);

            return mappedObj.Id;
        }
    }
}

