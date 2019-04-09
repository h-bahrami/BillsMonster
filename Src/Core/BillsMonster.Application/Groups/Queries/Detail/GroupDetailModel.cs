using BillsMonster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Application.Groups.Queries.Detail
{
    public class GroupDetailModel
    {
        public Guid Id { get; set; }
        public DateTime RecordTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public Group? Parent { get; set; }
        public ICollection<Group> Children { get; set; }
        public ICollection<Bill> Bills { get; set; }

        //public static explicit operator GroupDetailModel(Group entity)
        //{
        //    return new GroupDetailModel()
        //    {
        //        Id = 
        //    }
        //}
    }
}
