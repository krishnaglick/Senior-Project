using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SrProj.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        [Key]
        public int ID { get; set; }
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        [Key]
        public int ID { get; set; }
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
