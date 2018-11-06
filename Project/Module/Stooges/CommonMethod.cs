using NodaTime;
using NodaTime.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Module.Stooges
{
    public class CommonMethod
    {
        public static bool IsSixDigit(string value)
        {
            Regex regex = new Regex(@"^[0-9]{6}$", RegexOptions.IgnoreCase);
            Match match = regex.Match(value);
            return match.Success;
        }

        public static DateTime GetTodayStartInUtcByTimezone(string timezoneId = "Asia/Kuala_Lumpur")
        {
            var userTimeZone = DateTimeZoneProviders.Tzdb[timezoneId];
            var clock = SystemClock.Instance.InZone(userTimeZone);
            var now = clock.GetCurrentOffsetDateTime();
            return new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, DateTimeKind.Utc);
        }

        public static void CreateTextFile(string fullPath, string content)
        {
            if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            }
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath).Dispose();
            }
            using (TextWriter tw = new StreamWriter(fullPath))
            {
                tw.WriteLine(content);
            }
        }
    }
}
