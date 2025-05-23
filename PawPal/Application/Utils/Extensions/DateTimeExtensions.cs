﻿namespace Application.Utils.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ToNormalizedTime(this DateTime date)
    {
        var utcDate = date.ToUniversalTime();
        return new DateTime(utcDate.Year, utcDate.Month, utcDate.Day, utcDate.Hour, utcDate.Minute, 0, DateTimeKind.Utc);
    }

    public static DateTime ToDateWithTime(this DateTime date, TimeOnly time)
        => new(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, DateTimeKind.Utc);
}
