using AutoMapper;
using BillsMonster.Application.Interfaces.Mapping;
using BillsMonster.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace BillsMonster.Application.Bills
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public DateTime RecordTime { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordHint { get; set; }        
        public DateTime JoinedAt { get; set; }
        

        public static explicit operator UserModel(User entity)
        {
            return Projection.Compile().Invoke(entity);
        }

        public static explicit operator User(UserModel model)
        {
            return ReverseProjection.Compile().Invoke(model);
        }

        /// <summary>
        /// Entit to Model projection
        /// </summary>
        public static Expression<Func<User, UserModel>> Projection
        {
            get
            {
                return entity => new UserModel()
                {
                    Id = entity.Id,
                    RecordTime = entity.RecordTime,
                    Email = entity.Email,
                    Name = entity.Profile.Name,
                    Password = entity.Profile.Password,
                    PasswordHint = entity.Profile.PasswordHint,
                    JoinedAt = entity.Profile.JoinedAt,                    
                };
            }
        }

        public static Expression<Func<UserModel, User>> ReverseProjection
        {
            get
            {
                return model => new User()
                {
                    Id = model.Id,
                    RecordTime = model.RecordTime,                   
                    Email = model.Email,
                    Profile = new UserProfile() {
                        Name = model.Name,
                        JoinedAt = model.JoinedAt,
                        Password = model.Password,
                        PasswordHint = model.PasswordHint,                        
                    } 
                };
            }
        }
        
    }
}
