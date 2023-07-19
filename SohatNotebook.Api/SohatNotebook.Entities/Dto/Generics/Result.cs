namespace SohatNotebook.Entities.Dto.Generics
{
    public class Result<T>
    {
        public T Content { get; set; }
        public Error Error { get; set; }
        public bool IsSuccess => Error == null;
        public DateTime ResponseTime => DateTime.UtcNow;
    }
}
