using System;

namespace OCS.DAL.Entities.Abstract
{
    public interface IBaseEntity<TId>
    {
        TId Id { get; set; }

        bool IsDeleted { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}