using Microsoft.AspNetCore.Identity;
using OCS.DAL.Entities.Abstract;
using OCS.DAL.Entities.Chats;
using System;
using System.Collections.Generic;

namespace OCS.DAL.Entities.Users
{
    public class User : IdentityUser, IBaseEntity<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        
        public bool IsOnline { get; set; }
        
        public DateTime LastSeenAt { get; set; }

        public ICollection<UserContact> Contacts { get; set; }

        public ICollection<PrivateChat> PrivateChats { get; set; }

        public ICollection<UserGroupChat> GroupChats { get; set; }

        public ICollection<GroupChat> OwnedGroups { get; set; }
    }
}