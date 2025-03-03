using System;

namespace FrameworkExtensions;

public static class DateTimeExtensions
{
    public static int YearsBeforeToday(this DateTime source)
    {        
        return source.YearsBeforeDate(DateTime.Today);
    }

    public static int YearsBeforeDate(this DateTime source, DateTime target)
    {        
        DateTime sourceDate = source.Date;
        DateTime targetDate = target.Date;
        int years = targetDate.Year - sourceDate.Year;

        if (targetDate.Month <= sourceDate.Month && targetDate.Day < sourceDate.Day) 
        {
            years--;
        }
        if (years < 0) return 0;
        return years;
    }
}
