using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM_Console.TM_Base
{
    internal class AgeCalculator
    {
        public void CalculateAge()
        {
            DateTime dateTime = DateTime.Now;

            int isCalculate = 1;

            for (int i = 1; i == isCalculate; i++)
            {
                Console.Write("Enter you Date of Birth: {mm/dd/yyyy}: ");
                string dob = Console.ReadLine();
                try
                {
                    dateTime = Convert.ToDateTime(dob);
                }
                catch
                {
                    Console.WriteLine("Invalid Date format!");
                }

                int years = 0;
                int month = 0;
                int day = 0;
                DateTime dateNow = DateTime.Now;

                (years, month, day) = CalculateAge(dateTime);

                Console.WriteLine("Your age: " + years + " Years, " + month + " Month, " + day + " Days.");
                Console.Write("Do you want to calculate again? (y/n): ");
                string Yes = Console.ReadLine();
                if (Yes == "y")
                {
                    isCalculate++;
                }
                else
                {
                    isCalculate--;
                }
            }  
        }
        public (int years, int months, int days) CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;

            // Calculate the differences
            int years = today.Year - dateOfBirth.Year;
            int months = today.Month - dateOfBirth.Month;
            int days = today.Day - dateOfBirth.Day;

            // Adjust the year and month difference if needed
            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(today.Year, today.Month == 1 ? 12 : today.Month - 1);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            return (years, months, days);
        }
    }
}

