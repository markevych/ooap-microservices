using Common.Domain.Enums;

namespace IdentityService.Services.Models
{
    public class UpdateUserModel
    {
        public int UserId { get; set; }
        public int UpdaterUserId { get; set; }
        public string NewName { get; set; }
        public string NewEmail { get; set; }
        public UserRole NewRole { get; set; }
    }
}
