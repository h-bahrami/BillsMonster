using AutoMapper;
using BillsMonster.Application.Interfaces.Mapping;
using BillsMonster.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace BillsMonster.Application.Groups
{
    public class GroupDetailModel: IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public DateTime RecordTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }

        public static explicit operator GroupDetailModel(Group entity)
        {
            return Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<Group, GroupDetailModel>> Projection
        {
            get
            {
                return entity => new GroupDetailModel()
                {
                    Id = entity.Id,
                    RecordTime = entity.RecordTime,
                    Title = entity.Title,
                    Description = entity.Description,
                    ParentId = entity.ParentId,
                };
            }
        }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Group, GroupDetailModel>()
                .ForMember(ent => ent.Id, opt => opt.MapFrom(g => g.Id))
                .ForMember(ent => ent.RecordTime, opt => opt.MapFrom(g => g.RecordTime))
                .ForMember(ent => ent.Title, opt => opt.MapFrom(g => g.Title))
                .ForMember(ent => ent.Description, opt => opt.MapFrom(g => g.Description))
                .ForMember(ent => ent.ParentId, opt => opt.MapFrom(g => g.ParentId));
        }
    }
}
