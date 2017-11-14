using System.ComponentModel;
using Aubio.Extensions;

namespace Aubio
{
    internal static class FVecWindowTypeExtensions
    {
        public static string GetDescription(this FVecWindowType windowType)
        {
            var attribute = windowType.GetAttribute<DescriptionAttribute>();
            var description = attribute.Description;
            return description;
        }
    }
}