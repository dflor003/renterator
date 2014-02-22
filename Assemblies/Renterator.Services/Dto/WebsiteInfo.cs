using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Renterator.Services.Dto
{
    public class WebsiteInfo
    {
        public int Id { get; set; }

        [Required]
        public string WebsiteName { get; set; }

        [Required]
        public string WebsiteCode { get; set; }

        public bool IsActive { get; set; }

        public bool IsPublished { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public ICollection<object> ModuleData { get; set; }

        public ICollection<UserInfo> Users { get; set; }
    }
}
