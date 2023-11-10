using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Models
{
    namespace ConsoleUI.Models
    {
        public class TimeSheetEntry
        {
            public string WorkDone { get; set; }
            public double HoursWorked { get; set; }

            private TimeSheetEntry() { }

            public class TimeSheetEntryBuilder
            {
                private string workDone;
                private double hoursWorked;

                public TimeSheetEntryBuilder SetWorkDone()
                {
                    Console.Write("Enter what you did: ");
                    workDone = Console.ReadLine();
                    return this;
                }

                public TimeSheetEntryBuilder SetHoursWorked()
                {
                    do
                    {
                        Console.Write("How long did you do it for: ");
                        string rawTimeWorked = Console.ReadLine();

                        while (!double.TryParse(rawTimeWorked, out hoursWorked) || hoursWorked < 0)
                        {
                            Console.WriteLine("\nInvalid number given");
                            Console.Write("How long did you do it for: ");
                            rawTimeWorked = Console.ReadLine();
                        }

                    } while (hoursWorked < 0);

                    return this;
                }

                public TimeSheetEntry Build()
                {
                    return new TimeSheetEntry
                    {
                        WorkDone = workDone,
                        HoursWorked = hoursWorked
                    };
                }
            }
        }
    }

}
