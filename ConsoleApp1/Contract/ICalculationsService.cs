using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Contract;

public interface ICalculationsService
{
    int CustomerHoursOutsidePackage(DateTime EntryDate, DateTime ExitDate, DateTime PackageStartTime, DateTime PackageEndTime, DateTime ExpiryDate);

}
