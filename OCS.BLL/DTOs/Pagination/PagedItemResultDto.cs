using System.Collections.Immutable;

namespace OCS.BLL.DTOs.Pagination
{
    public class PagedItemResultDto<TItem>
    {
        public IImmutableList<TItem> Items { get; set; }

        public int TotalCount { get; set; }
    }
}