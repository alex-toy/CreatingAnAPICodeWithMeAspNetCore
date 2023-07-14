namespace SohatNotebook.Entities.DbSet
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public int Status { get; set; } = 1;

        public DateTime AddedData { get; set; } = DateTime.UtcNow;

        public DateTime UpdateDate { get; set; }
    }
}
