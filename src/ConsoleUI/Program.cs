using ConsoleUI.Models.ConsoleUI.Models;
using System;
using System.Collections.Generic;
using static ConsoleUI.Models.ConsoleUI.Models.TimeSheetEntry;

namespace ConsoleUI
{
    class Program
    {
        private const int StandardHoursPerWeek = 40;
        private const double AcmeRate = 150;
        private const double AbcRate = 125;
        private const double StandardRate = 10;
        private const double OvertimeRate = 15;

        static void Main(string[] args)
        {
            List<TimeSheetEntry> entries = CollectTimeSheetEntries();

            ProcessAndDisplayResults(entries);

            Console.WriteLine();
            Console.Write("Press any key to exit application...");
            Console.ReadKey();
        }

        static List<TimeSheetEntry> CollectTimeSheetEntries()
        {
            List<TimeSheetEntry> entries = new List<TimeSheetEntry>();
            string answer;

            do
            {
                TimeSheetEntryBuilder builder = new TimeSheetEntryBuilder();
                builder.SetWorkDone().SetHoursWorked();

                TimeSheetEntry entry = builder.Build();
                entries.Add(entry);

                Console.Write("Do you want to enter more time (yes/no): ");
                answer = Console.ReadLine().ToLower();

            } while (answer == "yes");

            return entries;
        }

        static void ProcessAndDisplayResults(List<TimeSheetEntry> entries)
        {
            double acmeTotal = CalculateTotalBill(entries, "acme", AcmeRate);
            double abcTotal = CalculateTotalBill(entries, "abc", AbcRate);
            double totalHours = CalculateTotalHours(entries);

            DisplayBill("Acme", acmeTotal);
            DisplayBill("ABC", abcTotal);

            if (totalHours > StandardHoursPerWeek)
            {
                double overtimePay = CalculateOvertimePay(totalHours);
                Console.WriteLine($"You will get paid ${overtimePay} for your work.");
            }
            else
            {
                double standardPay = CalculateStandardPay(totalHours);
                Console.WriteLine($"You will get paid ${standardPay} for your time.");
            }
        }

        static double CalculateTotalBill(List<TimeSheetEntry> entries, string keyword, double rate)
        {
            double total = 0;

            foreach (var entry in entries)
            {
                if (string.IsNullOrWhiteSpace(keyword) || entry.WorkDone.ToLower().Contains(keyword))
                {
                    total += entry.HoursWorked;
                }
            }

            return total * rate;
        }

        static double CalculateTotalHours(List<TimeSheetEntry> entries)
        {
            double total = 0;

            foreach (var entry in entries)
            {
                total += entry.HoursWorked;
            }

            return total;
        }

        static double CalculateOvertimePay(double totalHours)
        {
            return ((totalHours - StandardHoursPerWeek) * OvertimeRate) + (StandardHoursPerWeek * StandardRate);
        }

        static double CalculateStandardPay(double totalHours)
        {
            return totalHours * StandardRate;
        }

        static void DisplayBill(string client, double total)
        {
            Console.WriteLine($"Simulating Sending email to {client}");
            Console.WriteLine($"Your bill is ${total} for the hours worked.");
        }
    }
}
