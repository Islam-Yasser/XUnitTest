using ConsoleApp1.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Application;

public class CalculationsService : ICalculationsService
{
    struct DateInt()
    {
        public int Hours { get; set; }
        public int TotalHours { get; set; }
        public int Minutes { get; set; }
        public int DayOfYear { get; set; }
        public DateTime Date { get; set; }

        public TimeSpan TimeOfDay { get; set; }
    };
    public int CustomerHoursOutsidePackage(DateTime EntryDate, DateTime ExitDate, DateTime PackageStartTime, DateTime PackageEndTime, DateTime ExpiryDate)
    {
        var EntryDateInt= DateTimeToDateInt(EntryDate);
        var ExitDateInt= DateTimeToDateInt(ExitDate);
        var PackageStartDateInt= DateTimeToDateInt(PackageStartTime);
        var PackageEndDateInt= DateTimeToDateInt(PackageEndTime);
        DateTime minimumDate = MinDate(ExitDate, ExpiryDate);
        var minimumDateInt = DateTimeToDateInt(minimumDate);

        int TotalHours = (int)Math.Ceiling((ExitDate - EntryDate).TotalHours);
        int TotalTimeInsidePackage = 0;

        var daysInsidePackage = (minimumDateInt.DayOfYear - EntryDateInt.DayOfYear);
        var totalHoursOfPackage = PackageDuration(PackageStartTime, PackageEndTime);
        TotalTimeInsidePackage += (daysInsidePackage * totalHoursOfPackage) + totalHoursOfPackage;
        if(PackageStartTime.TimeOfDay > PackageEndTime.TimeOfDay)
        {
            EntryDateInt.Hours += 24;
            ExitDateInt.Hours += 24;
            PackageStartDateInt.Hours += 24;
            PackageEndDateInt.Hours += 24;
        }
        //cases of entrance
        if (EntryDateInt.Hours > PackageStartDateInt.Hours)
        {
            TotalTimeInsidePackage = HandleEntranceCases(EntryDateInt, PackageEndDateInt, TotalTimeInsidePackage, totalHoursOfPackage);

        }
        //cases of left
        // remove Time of day and replace with pur custom object.
        if ((minimumDateInt.Hours < PackageEndDateInt.Hours)&&  ExitDateInt.Date == EntryDateInt.Date)
        {
            TotalTimeInsidePackage = CasesOfLeft(minimumDateInt, PackageStartDateInt, TotalTimeInsidePackage, totalHoursOfPackage);
        }
        if (ExitDateInt.Date == EntryDateInt.Date)
        {
            return TotalHours - TotalTimeInsidePackage;
        }
        //cases of left
        if ((minimumDateInt.Hours < PackageEndDateInt.Hours) )
        {
            TotalTimeInsidePackage = CasesOfLeft(ExitDateInt, PackageStartDateInt, TotalTimeInsidePackage, totalHoursOfPackage);

        }
        var result= TotalHours - TotalTimeInsidePackage;
        return Math.Max(result, 0);

    }

    private static int HandleEntranceCases(DateInt EntryDate, DateInt PackageEndTime, int TotalTimeInsidePackage, int totalHoursOfPackage)
    {
        if (EntryDate.TimeOfDay < PackageEndTime.TimeOfDay)
        {
            var timeOustidePackage = totalHoursOfPackage - CalculatesHoursBetweenTwoTimes(PackageEndTime, EntryDate); ;
            TotalTimeInsidePackage -= timeOustidePackage;
        }
        else
        {
            TotalTimeInsidePackage -= totalHoursOfPackage;

        }

        return TotalTimeInsidePackage;
    }
    private static int PackageDuration(DateTime PackageStart , DateTime PackageEnd)
    {
       var hours= (PackageEnd.TimeOfDay - PackageStart.TimeOfDay).Hours;
        if(hours < 0)
        {
            hours += 24;
        }
        var minutes = (PackageEnd.TimeOfDay - PackageEnd.TimeOfDay).Minutes;
        if (minutes < 0) { 
            minutes += 60;
        }
        return hours + (minutes>0?1:0);


    }
    private DateInt DateTimeToDateInt(DateTime Date)
    {
        return new DateInt
        {
            DayOfYear = Date.DayOfYear,
            Hours = Date.Hour,
            Minutes = Date.Minute,
            TimeOfDay = Date.TimeOfDay,
            Date = Date

        };
    }
    private static int CalculatesHoursBetweenTwoTimes(DateInt PackageStartTime, DateInt PackageEndTime)
    {
        return ((Math.Abs((PackageEndTime.TimeOfDay - PackageStartTime.TimeOfDay).Hours) + ((Math.Abs((PackageEndTime.TimeOfDay - PackageStartTime.TimeOfDay).Minutes)) > 0 ? 1 : 0)));
    }

    private static int CasesOfLeft(DateInt ExitDate, DateInt PackageStartTime, int TotalTimeInsidePackage, int totalHoursOfPackage)
    {
        if (ExitDate.TimeOfDay < PackageStartTime.TimeOfDay)
        {
            TotalTimeInsidePackage -= totalHoursOfPackage;
        }
        else
        {
            TotalTimeInsidePackage -= totalHoursOfPackage - (Math.Abs((ExitDate.TimeOfDay - PackageStartTime.TimeOfDay).Hours) + ((ExitDate.TimeOfDay - PackageStartTime.TimeOfDay).Minutes > 0 ? 1 : 0));

        }

        return TotalTimeInsidePackage;
    }

    public static DateTime MinDate(DateTime T1, DateTime T2) => T1.Date < T2.Date ? T1 : T2;
    public static DateTime MaxTimeOfDay(DateTime T1, DateTime T2) => T1.TimeOfDay > T2.TimeOfDay ? T1 : T2;
    public static DateTime MinTimeOfDay(DateTime T1, DateTime T2) => T1.TimeOfDay > T2.TimeOfDay ? T2 : T1;

}
