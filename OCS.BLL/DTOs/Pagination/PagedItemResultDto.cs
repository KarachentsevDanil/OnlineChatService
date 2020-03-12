using System.Collections.Immutable;

namespace OCS.BLL.DTOs.Pagination
{
    public class PagedItemResultDto<TItem>
    {
        public PagedItemResultDto(IImmutableList<TItem> list, int totalCount)
        {
            Items = list;
            TotalCount = totalCount;
        }

        public IImmutableList<TItem> Items { get; set; }

        public int TotalCount { get; set; }
    }
}