using System;

namespace BillsMonster.Domain.Entities
{
    public class UserProfile
    {
        public User User { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordHint { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
