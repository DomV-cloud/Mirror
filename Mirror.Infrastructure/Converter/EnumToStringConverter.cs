using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mirror.Infrastructure.Converter
{
    public class EnumToStringConverter<T> : ValueConverter<T, string> where T : Enum
    {
        public EnumToStringConverter() : base(
            v => GetEnumDisplayName(v),
            v => GetEnumFromDisplayName(v))
        {
        }

        private static string GetEnumDisplayName(T enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?
                .Name ?? enumValue.ToString();
        }

        private static T GetEnumFromDisplayName(string displayName)
        {
            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (attribute.Name == displayName)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }
            return (T)Enum.Parse(type, displayName);
        }
    }
}
