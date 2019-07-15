using System;
using System.Globalization;
using CoreLibrary.BusinessEntities;

namespace CoreLibrary.Helpers
{
    public static class DatetimeHelper
    {
        public static Week WeekOfMonth(this DateTime date)
        {
            date = date.Date;
            var firstMonthDay = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            var absFirstDayOfWeek = (date.AddDays(-1 * diff).Date);
            var absLastDayOfWeek = absFirstDayOfWeek.AddDays(6);


            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            int week = cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int firstWeek = cal.GetWeekOfYear(firstMonthDay, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int lastWeek = cal.GetWeekOfYear(lastDayOfMonth, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            return
                new Week()
                {
                    Number = week,
                    FirstDay = week != firstWeek
                            ? absFirstDayOfWeek.Day
                            : 1,
                    LastDay = week != lastWeek
                          ? absLastDayOfWeek.Day
                          : lastDayOfMonth.Day,
                };
        }
    }
}