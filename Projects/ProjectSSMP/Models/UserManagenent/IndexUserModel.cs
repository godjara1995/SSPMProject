using System;
namespace ProjectSSMP.Models.UserManagenent
{
    public class IndexUserModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string JobResponsible { get; set; }
        public string UserEditBy { get; set; }
        public DateTime? UserEditDate { get; set; }
        public string GroupId { get; set; }
        public string Status { get; set; }
        public string GroupName { get; set; }

    }
}
