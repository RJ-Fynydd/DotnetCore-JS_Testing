using JS_Grid_Testing.Data.Entities;
using JS_Grid_Testing.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JS_Grid_Testing.Data
{
    public static class DbInitializer
    {

        public static Random random = new Random();

        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.People.AsNoTracking().Any())
            {
                return;   // DB has been seeded
            }

            

            var people = new Person[]
            {
                new Person{Name = "Carson", Age = GetNewAge(), Note = "Test"},
                new Person{Name = "Dan", Age = GetNewAge(), Note = "Test"},
                new Person{Name = "Jal", Age = GetNewAge(), Note = "Test"},
                new Person{Name = "Brian", Age = GetNewAge(), Note = "Test"},
                new Person{Name = "Jason", Age = GetNewAge(), Note = "Test"},
                new Person{Name = "Liz", Age = GetNewAge(), Note = "Test"},
                new Person{Name = "Michael", Age = GetNewAge(), Note = "Test"},
                new Person{Name = "Nick", Age = GetNewAge(), Note = "Test"},
                new Person{Name = "Jalee", Age = GetNewAge(), Note = "Test"},
                new Person{Name = "Collin", Age = GetNewAge(), Note = "Test"}
            };
            foreach (Person p in people)
            {
                context.People.Add(p);
            }
            context.SaveChanges();
            
        }

        public static int GetNewAge()
        {
            return random.Next(1, 100);
        }


    }
}
