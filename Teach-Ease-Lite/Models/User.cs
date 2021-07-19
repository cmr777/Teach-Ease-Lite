using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teach_Ease_Lite.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public int ReferenceType { get; set; }
        public int ReferenceID { get; set; }
        public bool IsPasswordSet { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserProfile
    {
        public int UserProfileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbImageUrl { get; set; }
        public string HeadLine { get; set; }
        public string Biography { get; set; }
        public string WebsiteUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkdinUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public bool IsActive { get; set; }
        public string MobileNo { get; set; }
    }
}