namespace SohatNotebook.Entities.DbSet
{
    public class HealthDataDb : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; }
        public string Race { get; set; }
        public bool UseGlasses { get; set; }
    }
}