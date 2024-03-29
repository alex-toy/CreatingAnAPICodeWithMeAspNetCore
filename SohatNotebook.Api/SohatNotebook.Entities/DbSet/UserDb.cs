﻿namespace SohatNotebook.Entities.DbSet
{
    public class UserDb : BaseEntity
    {
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Profession { get; set; }
        public string Hobby { get; set; }
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }
        public string? Gender { get; set; }
    }
}