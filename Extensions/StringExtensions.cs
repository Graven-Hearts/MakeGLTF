using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Graven.Hearts.MakeGLTF.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value)
        {
            if (value is null)
                return true;

            return string.IsNullOrEmpty(value);
        }

        public static bool EndsWithAnyIgnoringCase([NotNullWhen(false)] this string? value, IEnumerable<string> endings)
        {
            if (value is null)
                return false;

            foreach(var ending in endings)
            {
                if (value.EndsWith(ending.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
