using AutoMapper;
using BillsMonster.Application.Interfaces.Mapping;
using BillsMonster.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace BillsMonster.Application.Bills
{
    public class BillDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public DateTime RecordTime { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string ReferenceId { get; set; }
        public float Amount { get; set; }
        public string Image { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? PaidAt { get; set; }
        public Guid GroupId { get; set; }

        public static explicit operator BillDto(Bill entity)
        {
            return Projection.Compile().Invoke(entity);
        }

        public static explicit operator Bill(BillDto model)
        {
            return ReverseProjection.Compile().Invoke(model);
        }

        /// <summary>
        /// Entit to Model projection
        /// </summary>
        public static Expression<Func<Bill, BillDto>> Projection
        {
            get
            {
                return entity => new BillDto()
                {
                    Id = entity.Id,
                    RecordTime = entity.RecordTime,
                    Title = entity.Title,
                    Note = entity.Note,
                    ReferenceId = entity.ReferenceId,
                    Amount = entity.Amount,
                    Image = entity.Image,
                    ReceivedAt = entity.ReceivedAt,
                    DueDate = entity.DueDate,
                    PaidAt = entity.PaidAt,
                    GroupId = entity.GroupId,
                };
            }
        }

        public static Expression<Func<BillDto, Bill>> ReverseProjection
        {
            get
            {
                return model => new Bill()
                {
                    Id = model.Id,
                    RecordTime = model.RecordTime,
                    Title = model.Title,
                    Note = model.Note,
                    ReferenceId = model.ReferenceId,
                    Amount = model.Amount,
                    Image = model.Image,
                    ReceivedAt = model.ReceivedAt,
                    DueDate = model.DueDate,
                    PaidAt = model.PaidAt,
                    GroupId = model.GroupId,
                };
            }
        }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Bill, BillDto>()
                .ForMember(ent => ent.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(ent => ent.RecordTime, opt => opt.MapFrom(b => b.RecordTime))
                .ForMember(ent => ent.Title, opt => opt.MapFrom(b => b.Title))
                .ForMember(ent => ent.Note, opt => opt.MapFrom(b => b.Note))
                .ForMember(ent => ent.ReferenceId, opt => opt.MapFrom(b => b.ReferenceId))
                .ForMember(ent => ent.Amount, opt => opt.MapFrom(b => b.Amount))
                .ForMember(ent => ent.Image, opt => opt.MapFrom(b => b.Image))
                .ForMember(ent => ent.ReceivedAt, opt => opt.MapFrom(b => b.ReceivedAt))
                .ForMember(ent => ent.DueDate, opt => opt.MapFrom(b => b.DueDate))
                .ForMember(ent => ent.PaidAt, opt => opt.MapFrom(b => b.PaidAt))
                .ForMember(ent => ent.GroupId, opt => opt.MapFrom(b => b.GroupId));
        }

        
    }
}
