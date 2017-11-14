using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Aubio.Extensions
{
    [PublicAPI]
    public static class EnumExtensions
    {
        public static T GetAttribute<T>([NotNull] this Enum value) where T : Attribute
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var type = value.GetType();

            var typeInfo = type.GetTypeInfo();

            if (!typeInfo.IsEnum)
                throw new ArgumentOutOfRangeException(nameof(value));

            var memberInfo = typeInfo.DeclaredMembers.Single(s => s.Name == value.ToString());

            var attribute = memberInfo.GetCustomAttribute<T>();

            return attribute;
        }
    }
}