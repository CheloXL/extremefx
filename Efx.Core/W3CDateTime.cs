// Decompiled with JetBrains decompiler
// Type: Efx.Core.W3CDateTime
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Efx.Core
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct W3CDateTime
  {
    private static readonly string[] _monthNames = new string[12]
    {
      "Jan",
      "Feb",
      "Mar",
      "Apr",
      "May",
      "Jun",
      "Jul",
      "Aug",
      "Sep",
      "Oct",
      "Nov",
      "Dec"
    };
    private const string RFC822_DATE_FORMAT = "^((Mon|Tue|Wed|Thu|Fri|Sat|Sun), *)?(?<day>\\d\\d?) +(?<month>Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) +(?<year>\\d\\d(\\d\\d)?) +(?<hour>\\d\\d):(?<min>\\d\\d)(:(?<sec>\\d\\d))? +(?<ofs>([+\\-]?\\d\\d\\d\\d)|UT|GMT|EST|EDT|CST|CDT|MST|MDT|PST|PDT)$";
    private const string W3_C_DATE_FORMAT = "^(?<year>\\d\\d\\d\\d)(-(?<month>\\d\\d)(-(?<day>\\d\\d)(T(?<hour>\\d\\d):(?<min>\\d\\d)(:(?<sec>\\d\\d)(?<ms>\\.\\d+)?)?(?<ofs>(Z|[+\\-]\\d\\d:\\d\\d)))?)?)?$";

    public static string ToString(DateTime pDateTime, string pFormat)
    {
      switch (pFormat)
      {
        case "R":
          return pDateTime.ToString("ddd, dd MMM yyyy HH:mm:ss ");
        case "W":
          return pDateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        default:
          throw new ArgumentException("Unrecognized date format requested.");
      }
    }

    public static DateTime Parse(string pText)
    {
      Match match = new Regex(string.Format("(?<rfc822>{0})|(?<w3c>{1})", (object) "^((Mon|Tue|Wed|Thu|Fri|Sat|Sun), *)?(?<day>\\d\\d?) +(?<month>Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) +(?<year>\\d\\d(\\d\\d)?) +(?<hour>\\d\\d):(?<min>\\d\\d)(:(?<sec>\\d\\d))? +(?<ofs>([+\\-]?\\d\\d\\d\\d)|UT|GMT|EST|EDT|CST|CDT|MST|MDT|PST|PDT)$", (object) "^(?<year>\\d\\d\\d\\d)(-(?<month>\\d\\d)(-(?<day>\\d\\d)(T(?<hour>\\d\\d):(?<min>\\d\\d)(:(?<sec>\\d\\d)(?<ms>\\.\\d+)?)?(?<ofs>(Z|[+\\-]\\d\\d:\\d\\d)))?)?)?$")).Match(pText);
      if (!match.Success)
        throw new FormatException("String is not a valid date time stamp.");
      try
      {
        bool success = match.Groups["rfc822"].Success;
        int year = int.Parse(match.Groups["year"].Value);
        if (year < 1000)
        {
          if (year < 50)
            year += 2000;
          else
            year += 1999;
        }
        int month = !success ? (match.Groups["month"].Success ? int.Parse(match.Groups["month"].Value) : 1) : W3CDateTime.parseRfc822Month(match.Groups["month"].Value);
        int day = match.Groups["day"].Success ? int.Parse(match.Groups["day"].Value) : 1;
        int hour = match.Groups["hour"].Success ? int.Parse(match.Groups["hour"].Value) : 0;
        int minute = match.Groups["min"].Success ? int.Parse(match.Groups["min"].Value) : 0;
        int second = match.Groups["sec"].Success ? int.Parse(match.Groups["sec"].Value) : 0;
        int millisecond = match.Groups["ms"].Success ? (int) Math.Round(1000.0 * double.Parse(match.Groups["ms"].Value)) : 0;
        TimeSpan timeSpan = TimeSpan.Zero;
        if (match.Groups["ofs"].Success)
          timeSpan = success ? W3CDateTime.parseRfc822Offset(match.Groups["ofs"].Value) : W3CDateTime.parseW3COffset(match.Groups["ofs"].Value);
        return new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc) - timeSpan;
      }
      catch (Exception ex)
      {
        throw new FormatException("String is not a valid date time stamp.", ex);
      }
    }

    private static int parseRfc822Month(string pMonthName)
    {
      for (int index = 0; index < 12; ++index)
      {
        if (pMonthName == W3CDateTime._monthNames[index])
          return index + 1;
      }
      throw new ApplicationException("Invalid month name");
    }

    private static TimeSpan parseRfc822Offset(string pOffset)
    {
      if (pOffset == string.Empty)
        return TimeSpan.Zero;
      int num = 0;
      switch (pOffset)
      {
        case "UT":
        case "GMT":
          return TimeSpan.FromHours((double) num);
        case "EDT":
          num = -4;
          goto case "UT";
        case "EST":
        case "CDT":
          num = -5;
          goto case "UT";
        case "CST":
        case "MDT":
          num = -6;
          goto case "UT";
        case "MST":
        case "PDT":
          num = -7;
          goto case "UT";
        case "PST":
          num = -8;
          goto case "UT";
        default:
          if (pOffset[0] == '+')
            return TimeSpan.Parse(pOffset.Substring(1, 2) + ":" + pOffset.Substring(3, 2));
          return TimeSpan.Parse(pOffset.Insert(pOffset.Length - 2, ":"));
      }
    }

    private static TimeSpan parseW3COffset(string pOffset)
    {
      if (pOffset == string.Empty || pOffset == "Z")
        return TimeSpan.Zero;
      if (pOffset[0] != '+')
        return TimeSpan.Parse(pOffset);
      return TimeSpan.Parse(pOffset.Substring(1));
    }
  }
}
