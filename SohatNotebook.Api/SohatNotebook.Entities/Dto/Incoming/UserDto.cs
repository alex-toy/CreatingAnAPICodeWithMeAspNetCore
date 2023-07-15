using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.Entities.Dto.Incoming
{
    public class UserDto : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Profession { get; set; }
        public string Hobby { get; set; }
    }
}