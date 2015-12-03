
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class RoleVolunteer
    {
        [Key]
        public int ID { get; set; }
        public virtual Role Role { get; set; }
        public virtual Volunteer Volunteer { get; set; }

        public override bool Equals(object obj)
        {
            var roleVolunteer = obj as RoleVolunteer;
            if(roleVolunteer == null) return false;

            return this.Role.ID == roleVolunteer.Role.ID
                && this.Volunteer.Username == roleVolunteer.Volunteer.Username;
        }

        public override int GetHashCode()
        {
            return this.Role.GetHashCode() ^ this.Volunteer.GetHashCode();
        }
    }
}
