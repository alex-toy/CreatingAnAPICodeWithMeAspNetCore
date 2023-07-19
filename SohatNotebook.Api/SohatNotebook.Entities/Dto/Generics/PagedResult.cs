﻿namespace SohatNotebook.Entities.Dto.Generics
{
    public class PagedResult<T> : Result<List<T>>
    {
        public int Page { get; set; }
        public int ResultCount { get; set; }
        public int ResultsPerPage{ get; set; }
    }
}
