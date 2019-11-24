using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkerProfileApi.Dto
{
    public class Paginated<T>
    {
        public const int MIN_PAGE_SIZE = 10;
        public const int MAX_PAGE_SIZE = 50;

        public Paginated()
        {

        }
        public Paginated(int page, int pageSize, IQueryable<T> query)
        {
            this.TotalElements = query.Count();

            this.PageSize = pageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : (pageSize > 0 ? pageSize : MIN_PAGE_SIZE);
            this.PageCount = this.TotalElements / this.PageSize + (this.TotalElements % this.PageSize > 0 ? 1 : 0);
            this.Page = page > this.PageCount ? this.PageCount : (page > 0 ? page : 1);
            if (this.Page == 0)
            {
                this.Page = 1;
            }
            var offset = (this.Page - 1) * this.PageSize;

            var res = query.Skip(offset).Take(this.PageSize);
            this.Payload = res.ToList();
        }

        public Paginated<E> To<E>(Func<T, E> mapper)
        {
            return new Paginated<E>()
            {
                Page = this.Page,
                    PageSize = this.PageSize,
                    PageCount = this.PageCount,
                    TotalElements = this.TotalElements,
                    Payload = this.Payload.Select(r => mapper(r)).ToList()
            };
        }

        public int Page { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int TotalElements { get; set; }

        public List<T> Payload { get; set; }
    }

}