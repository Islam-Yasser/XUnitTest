namespace TestCalculations;
using ConsoleApp1;
using ConsoleApp1.Application;
using ConsoleApp1.Contract;
using System;

public class UnitTest1 
{
    private readonly ICalculationsService _calculationsService;
    public UnitTest1()
    {
        _calculationsService = new CalculationsService() ;
    } 

    [Fact]
    public void EarlyEntry()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 7, 30, 0),
            new DateTime(2025, 1, 4, 13, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 8, 23, 59, 59)
        );
        // 1 hour
        Assert.True(result==1);
    }
    
    [Fact]
    public void LateEntryandEarlyExit()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 19, 0, 0),
            new DateTime(2025, 1, 5, 6, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 8, 23, 59, 59)
        );
        // Expected result = 11 hours
        Assert.True(result == 11);


    }
    [Fact]
    public void NoTimeOutsidePackage()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 7, 0, 0),
            new DateTime(2025, 1, 4, 7, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 8, 23, 59, 59)
        );

        //0
        Assert.True(result == 0);

    }

    [Fact]
    public void LateExit()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 7, 0, 0),
            new DateTime(2025, 1, 4, 21, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 8, 23, 59, 59)
        );
        // Expected result = 4 hours
        Assert.True(result == 4);


    }
    [Fact]
     public void ExtendedStay()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 7, 0, 0),
            new DateTime(2025, 1, 5, 10, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 8, 23, 59, 59)
        );
        // Expected result = 17 hours
        Assert.True(result == 15);


    }

    [Fact]
    public void NightStay()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 5, 7, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 8, 23, 59, 59)
        );
        Console.WriteLine(result);
        // Expected result = 13 hours
        Assert.True(result == 13);


    }

    [Fact]
    public void ExtendedNightStay()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 5, 10, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 8, 23, 59, 59)
        );
        // Expected result = 14 hours
        Assert.True(result==14);
    }

    [Fact]
    public void EntranceBeforePackageEndsAndLeavesAfter()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 17, 30, 0),
            new DateTime(2025, 1, 4, 22, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 4, 23, 59, 59)
        );
        Assert.True(result == 4); // Expected result = 2 hours
    }

    [Fact]
    public void EntersBeforePackageExpiryAndLeavesShortlyAfter()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 17, 0, 0),
            new DateTime(2025, 1, 4, 19, 30, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 4, 23, 59, 59)
        );
        Assert.True(result == 2); // Expected result = 2 hours
    }

    [Fact]
    public void JustBeforePackageEndsAndExitsAfter()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 17, 50, 0),
            new DateTime(2025, 1, 4, 21, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 4, 23, 59, 59)
        );
        Assert.True(result == 3); // Expected result = 3 hours
    }

    [Fact]
    public void BeforePackageEndsAndExitsAfterExpiry()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 17, 0, 0),
            new DateTime(2025, 1, 4, 20, 30, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 4, 23, 59, 59)
        );
        Assert.True(result == 3); // Expected result = 3 hours
    }

    [Fact]
    public void EntersAfterPackageEndsAndExitsAtExpiry()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 19, 0, 0),
            new DateTime(2025, 1, 4, 22, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 4, 22, 0, 0)
        );
        Assert.True(result == 3); // Expected result = 3 hours
    }

    [Fact]
    public void LeavesRightBeforeExpiryAfterEnteringLate()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 19, 0, 0),
            new DateTime(2025, 1, 4, 23, 59, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 4, 23, 59, 59)
        );
        Assert.True(result == 5); // Expected result = 5 hours
    }

    [Fact]
    public void EntersEarlyAndLeavesAfterPackageEnds()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 7, 30, 0),
            new DateTime(2025, 1, 4, 19, 0, 0),
            new DateTime(2025, 1, 4, 8, 0, 0),
            new DateTime(2025, 1, 4, 18, 0, 0),
            new DateTime(2025, 1, 4, 23, 59, 59)
        );
        Assert.True(result == 2); // Expected result = 2 hours
    }
    [Fact]
    public void EntryBeforeExpirationExitAfter()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 9, 0, 0),  // EntryDate
            new DateTime(2025, 1, 5, 10, 0, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 30); // Expected: 30 hours outside package
    }

    [Fact]
    public void EntryAndExitBeforeExpiration()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 7, 0, 0),  // EntryDate
            new DateTime(2025, 1, 3, 20, 0, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 3); // Expected: 4 hours outside package
    }

    [Fact]
    public void ExitWhenPackageExpires()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 7, 0, 0),  // EntryDate
            new DateTime(2025, 1, 4, 23, 59, 59), // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 7); // Expected: 7 hours outside package
    }

    [Fact]
    public void EntryAndExitAfterExpiration()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 5, 6, 0, 0),  // EntryDate
            new DateTime(2025, 1, 5, 20, 0, 0), // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 14); // Expected: 14 hours outside package
    }

    [Fact]
    public void EntryBeforeExpirationExitAfterNextDay()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 12, 0, 0),  // EntryDate
            new DateTime(2025, 1, 5, 10, 0, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 30); // Expected: 26 hours outside package
    }

    [Fact]
    public void EntryAndExitAtPackageBoundary()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 8, 0, 0),  // EntryDate
            new DateTime(2025, 1, 3, 18, 0, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 0); // Expected: 0 hours outside package
    }

    [Fact]
    public void EntryAtStartExitAfterExpiration()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 8, 0, 0),  // EntryDate
            new DateTime(2025, 1, 5, 8, 0, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 28); // Expected: 28 hours outside package
    }

    [Fact]
    public void EntryAndExitDifferentDaysPartialOverlap()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 7, 0, 0),  // EntryDate
            new DateTime(2025, 1, 4, 19, 0, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 16); // Expected: 17 hours outside package
    }
    [Fact]
    public void ComplexExpiration_EntryBeforeExpirationExitLongAfter()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 2, 10, 0, 0),  // EntryDate
            new DateTime(2025, 1, 5, 15, 30, 0), // ExitDate
            new DateTime(2025, 1, 2, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 2, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 50); // Expected: 46 hours outside package
    }

    [Fact]
    public void ComplexExpiration_MultipleDaysAfterExpiration()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 9, 0, 0),  // EntryDate
            new DateTime(2025, 1, 6, 10, 45, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 55); // Expected: 63 hours outside package
    }

    [Fact]
    public void ComplexExpiration_ExactBoundaryOnExpiration()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 9, 0, 0),  // EntryDate
            new DateTime(2025, 1, 4, 23, 59, 59), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 20); // Expected: 19 hours outside package
    }

    [Fact]
    public void MinuteRounding_OneMinuteOutside()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 17, 59, 0),  // EntryDate
            new DateTime(2025, 1, 3, 18, 0, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 17, 59, 0), // PackageEndTime
            new DateTime(2025, 1, 3, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 1); // Expected: 1 hour outside package
    }

    [Fact]
    public void MinuteRounding_59MinutesOutside()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 17, 1, 0),  // EntryDate
            new DateTime(2025, 1, 3, 18, 0, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 17, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 3, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 1); // Expected: 1 hour outside package
    }

    [Fact]
    public void MinuteRounding_MultipleMinuteBlocks()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 16, 30, 0),  // EntryDate
            new DateTime(2025, 1, 3, 18, 15, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 16, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 3, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 2); // Expected: 3 hours outside package
    }
    [Fact]
    public void StayWithinPackage_EntireStayWithinPackage()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 23, 30, 0),
            new DateTime(2025, 1, 5, 2, 30, 0),
            new DateTime(2025, 1, 4, 23, 0, 0),
            new DateTime(2025, 1, 5, 6, 0, 0),
            new DateTime(2025, 1, 5, 23, 59, 59)
        );
        
        Assert.True(result == 0);
    }

    [Fact]
    public void ExitAfterPackageEnds()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 23, 15, 0),
            new DateTime(2025, 1, 5, 7, 15, 0),
            new DateTime(2025, 1, 4, 23, 0, 0),
            new DateTime(2025, 1, 5, 6, 0, 0),
            new DateTime(2025, 1, 5, 23, 59, 59)
        );
        Assert.True(result == 1);
    }

    [Fact]
    public void EntryBeforePackageStarts()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 22, 0, 0),
            new DateTime(2025, 1, 4, 23, 30, 0),
            new DateTime(2025, 1, 4, 23, 0, 0),
            new DateTime(2025, 1, 5, 6, 0, 0),
            new DateTime(2025, 1, 5, 6, 0, 0)
        );
        Assert.True(result == 1);
    }

    [Fact]
    public void EntryBeforeAndExitAfter()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 21, 30, 0),
            new DateTime(2025, 1, 5, 7, 30, 0),
            new DateTime(2025, 1, 4, 23, 0, 0),
            new DateTime(2025, 1, 5, 6, 0, 0),
            new DateTime(2025, 1, 5, 23, 59, 59)
        );
        Assert.True(result == 3);
    }

    [Fact]
    public void StayAcrossMultipleDays()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 22, 30, 0),
            new DateTime(2025, 1, 5, 7, 30, 0),
            new DateTime(2025, 1, 3, 23, 0, 0),
            new DateTime(2025, 1, 4, 6, 0, 0),
            new DateTime(2025, 1, 5, 23, 59, 59)
        );
        Assert.True(result == 19);
    }

    [Fact]
    public void ExactPackageTimeBoundary()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 23, 0, 0),
            new DateTime(2025, 1, 5, 6, 0, 0),
            new DateTime(2025, 1, 4, 23, 0, 0),
            new DateTime(2025, 1, 5, 6, 0, 0),
            new DateTime(2025, 1, 5, 23, 59, 59)
        );
        Assert.True(result == 0);
    }
    [Fact]
    public void EntryAfterPackageStartAndLeaveBeforePackageEnd()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 3, 9, 30, 0),  // EntryDate
            new DateTime(2025, 1, 3, 17, 12, 0), // ExitDate
            new DateTime(2025, 1, 3, 8, 0, 0),  // PackageStartTime
            new DateTime(2025, 1, 3, 18, 0, 0), // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 0); // Expected: 0 hours outside package
    }
    [Fact]
    public void FullDayStayWithinPackage()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 8, 0, 0),   // EntryDate
            new DateTime(2025, 1, 4, 18, 0, 0),  // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),   // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0),  // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 0); // Expected result = 0 hours outside package
    }

    [Fact]
    public void FullDayStayOutsidePackage()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 7, 0, 0),   // EntryDate
            new DateTime(2025, 1, 4, 19, 0, 0),  // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),   // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0),  // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 2); // Expected result = 2 hours outside package
    }

    [Fact]
    public void StayCrossingMidnight()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 22, 0, 0),  // EntryDate
            new DateTime(2025, 1, 5, 6, 0, 0),   // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),   // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0),  // PackageEndTime
            new DateTime(2025, 1, 8, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 8); // Expected result = 8 hours outside package
    }

    [Fact]
    public void StayWithinPackageOnExpirationDay()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 10, 0, 0),  // EntryDate
            new DateTime(2025, 1, 4, 17, 0, 0),  // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),   // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0),  // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 0); // Expected result = 0 hours outside package
    }

    [Fact]
    public void StayExtendingToExpirationTime()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 19, 0, 0),  // EntryDate
            new DateTime(2025, 1, 4, 23, 59, 59), // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),   // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0),  // PackageEndTime
            new DateTime(2025, 1, 4, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 5); // Expected result = 5 hours outside package
    }

    [Fact]
    public void StayEntirelyBeforePackageStarts()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 6, 0, 0),   // EntryDate
            new DateTime(2025, 1, 4, 7, 0, 0),   // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),   // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0),  // PackageEndTime
            new DateTime(2025, 1, 8, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 1); // Expected result = 1 hour outside package
    }

    [Fact]
    public void StayEntirelyAfterPackageExpires()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 9, 0, 0, 0),   // EntryDate
            new DateTime(2025, 1, 9, 6, 0, 0),   // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),   // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0),  // PackageEndTime
            new DateTime(2025, 1, 8, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 6); // Expected result = 6 hours outside package
    }

    [Fact]
    public void OverlappingMultipleDays()
    {
        var result = _calculationsService.CustomerHoursOutsidePackage(
            new DateTime(2025, 1, 4, 17, 0, 0),  // EntryDate
            new DateTime(2025, 1, 6, 9, 0, 0),   // ExitDate
            new DateTime(2025, 1, 4, 8, 0, 0),   // PackageStartTime
            new DateTime(2025, 1, 4, 18, 0, 0),  // PackageEndTime
            new DateTime(2025, 1, 8, 23, 59, 59) // ExpiryDate
        );
        Assert.True(result == 28); // Expected result = 33 hours outside package
    }


}