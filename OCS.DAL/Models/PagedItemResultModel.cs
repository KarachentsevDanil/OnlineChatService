using System.Collections.Generic;

namespace OCS.DAL.Models
{
    public class PagedItemResultModel<TItem>
    {
        public ICollection<TItem> Items { get; set; }

        public int TotalCount { get; set; }
    }
}