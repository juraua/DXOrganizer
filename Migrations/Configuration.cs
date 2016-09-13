using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DXOrganizer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DXOrganizer.Models.DatabaseContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.DatabaseContext context)
        {
            var imageOn = context.AlarmStatusImages
                .SingleOrDefault(e => e.AlarmStatusImageId == AlarmStatusImageId.On) ?? new AlarmStatusImage
                {
                    AlarmStatusImageId = AlarmStatusImageId.On,
                    AlarmStatusImageInstance = File.ReadAllBytes(AssemblyDirectory + @"\Images\alarm_on.png")
                };
            context.AlarmStatusImages.AddOrUpdate(imageOn);

            var imageOff = context.AlarmStatusImages
                .SingleOrDefault(e => e.AlarmStatusImageId == AlarmStatusImageId.Off) ?? new AlarmStatusImage
                {
                    AlarmStatusImageId = AlarmStatusImageId.Off,
                    AlarmStatusImageInstance = File.ReadAllBytes(AssemblyDirectory + @"\Images\alarm_off.png")
                };
            context.AlarmStatusImages.AddOrUpdate(imageOff);

            context.AlarmClocks.Add(new AlarmClock{
                AlarmClockName = "Утренняя пробежка",
                Start = new TimeSpan(6, 10, 0),
                Repeat = true,
                DaysOfWeek =
                    new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday },
                Status = true,
                AlarmStatusImage = imageOn,
                MelodyId = MelodyName.Chimes
            });

            context.AlarmClocks.Add(new AlarmClock
            {
                AlarmClockName = "Вечерние занятия",
                Start = new TimeSpan(17, 30, 0),
                Repeat = true,
                DaysOfWeek = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Friday },
                Status = true,
                AlarmStatusImage = imageOn,
                MelodyId = MelodyName.Chords
            });

            context.AlarmClocks.Add(new AlarmClock
            {
                AlarmClockName = "Приготовить обед",
                Start = new TimeSpan(12, 45, 0),
                Repeat = false,
                DaysOfWeek = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Status = true,
                AlarmStatusImage = imageOn,
                MelodyId = MelodyName.Echo
            });
            context.AlarmClocks.Add(new AlarmClock
            {
                AlarmClockName = "Временный",
                Start = new TimeSpan(9, 0, 0),
                Repeat = true,
                DaysOfWeek = new List<DayOfWeek> {DayOfWeek.Saturday, DayOfWeek.Sunday},
                Status = false,
                AlarmStatusImage = imageOff,
                MelodyId = MelodyName.Bounce
            });

            var melodyIds = Enum.GetValues(typeof (MelodyName)).Cast<MelodyName>();

            foreach (var melodyName in melodyIds)
            {
                context.Melodies.AddOrUpdate(new Melody
                {
                    MelodyId = melodyName,
                    MelodyName = melodyName.ToString(),
                });
            }
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
        
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }

}
//Команды для миграции 
//Enable-Migrations –EnableAutomaticMigrations  //Включить автоматическую миграцию
//Add-Migration Name_Migrations //Создает файл миграции,вместо "Name_Migrations" указать имя миграции,напр.: FirstMigration
//Update-Database –Verbose //Обновить базу данных на основе последней миграции