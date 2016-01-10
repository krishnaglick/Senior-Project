
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class RoleVolunteer
    {
        [Key, Column(Order = 2)]
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
        [Key, Column(Order = 1)]
        public string VolunteerUsername { get; set; }
        [ForeignKey("VolunteerUsername")]
        public virtual Volunteer Volunteer { get; set; }

        public override bool Equals(object obj)
        {
            var roleVolunteer = obj as RoleVolunteer;
            if(roleVolunteer == null) return false;

            return this.RoleID == roleVolunteer.RoleID
                && this.VolunteerUsername == roleVolunteer.VolunteerUsername;
        }

        public override int GetHashCode()
        {
            return this.RoleID.GetHashCode() ^ this.VolunteerUsername.GetHashCode();
        }
    }
}
