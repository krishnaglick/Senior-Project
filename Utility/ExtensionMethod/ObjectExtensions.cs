
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utility.ExtensionMethod
{
    public static class ObjectExtensions
    {
        public static T GetEnumAttribute<T>(this object enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static List<string> Compare(this object me, object target, string prefix = "", List<string> recurse = null)
        {
            //Questionable if this works
            var diffs = recurse ?? new List<string>();

            Type myType = me.GetType();
            if(myType != target.GetType())
                throw new Exception("Objects are not of same type!");

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            var properties = myType.GetProperties(flags);

            foreach (PropertyInfo prop in properties)
            {
                if (prop.GetValue(me).GetType().GetGenericTypeDefinition() == typeof (IEnumerable<>))
                    diffs.AddRange(prop.GetValue(me).Compare(prop.GetValue(target), $"{prefix}.{prop.Name}", diffs));
                else if (prop.GetValue(me) != prop.GetValue(target))
                    diffs.Add(prefix == null ? prop.Name : $"{prefix}.{prop.Name}");
            }

            return diffs;
        }

        public static object Replace(this object Base, object Target)
        {
            //Questionable if this works
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            Type[] arrayTypes = new Type[]
            {
                typeof (IEnumerable<>),
                typeof (HashSet<>)
            };
            var props = Base.GetType().GetProperties(flags);
            foreach (var prop in props)
            {
                if (arrayTypes.Contains(prop.GetValue(Base).GetType().GetGenericTypeDefinition()))
                {
                    prop.GetValue(Base).Replace(prop.GetValue(Target));
                }
                else
                    prop.SetValue(Base, prop.GetValue(Target));
            }
            return Base;
        }
    }
}