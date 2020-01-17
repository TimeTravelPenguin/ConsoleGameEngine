using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CoreGameEngine.Resources;

namespace CoreGameEngine.Extensions
{
  public static class StringExtensions
  {
    public static bool StartsWith(this string input, IEnumerable<string> values)
    {
      if (values is null)
      {
        throw new ArgumentNullException(nameof(values), Exceptions.Argument_IsNull);
      }

      return values.Any(value => input.StartsWith(value, StringComparison.InvariantCulture));
    }

    public static string FirstCharToUpper(this string input)
    {
      return input switch
      {
        null => throw new ArgumentNullException(nameof(input)),
        "" => throw new ArgumentException(
          string.Format(CultureInfo.InvariantCulture, Exceptions.Argument_StringEmpty_ExceptionTakesParam,
            nameof(input)), nameof(input)),
        _ => input.First().ToString(CultureInfo.InvariantCulture).ToUpperInvariant() + input.Substring(1)
      };
    }
  }
}