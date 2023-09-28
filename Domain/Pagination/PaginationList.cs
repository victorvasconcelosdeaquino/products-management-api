using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Pagination
{
    public class PaginationList<T> : List<T>
    {
        public PaginationList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalPagesCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPagesCount = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalPagesCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPagesCount;

        public static PaginationList<T> ToPaginationList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationList<T>(items, count, pageNumber, pageSize);
        }
    }
}
