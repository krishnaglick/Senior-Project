
using System.Data.Entity;

namespace SrProj.Models.Context
{
    public class ContextBase : DbContext
    {
        //TODO: Make this a config option.
        public ContextBase() : base("homeNetwork") { }
    }
}