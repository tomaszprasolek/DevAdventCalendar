using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevAdventCalendarCompetition.Data;
using DevAdventCalendarCompetition.Models;

namespace DevAdventCalendarCompetition
{
    public class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Tests == null || context.Tests.ToList().Count == 0)
            {
                var sampleTest = new Test
                {
                    Number = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    Answers = new List<TestAnswer>()
                };

                context.Add(sampleTest);
                context.SaveChanges();
            }
        }
    }
}
