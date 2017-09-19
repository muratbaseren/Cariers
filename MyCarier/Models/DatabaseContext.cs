using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using FD = FakeData;

namespace MyCarier.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<PersonInfo> PersonInfos { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<WorkExprience> WorkExpriences { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<SharedUrl> SharedUrls { get; set; }


        public DatabaseContext()
        {
            Database.SetInitializer(new MyInit());
        }
    }

    public class MyInit : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            //person info..
            PersonInfo pi = new PersonInfo()
            {
                Name = FD.NameData.GetFirstName(),
                Surname = FD.NameData.GetSurname(),
                City = FD.PlaceData.GetCity(),
                Country = FD.PlaceData.GetCountry(),
                Email = "mb@mail.com",
                Phone = FD.PhoneNumberData.GetPhoneNumber(),
                AddtionalInfo = FD.TextData.GetSentences(2),
                Title = "Software Developer",
                PhotoImageName = "profile.png"
            };

            context.PersonInfos.Add(pi);
            context.SaveChanges();

            // skills
            string[] abilities = new string[] { "Visual C#", "T-SQL", "Javascript", "Html & CSS", "OOP" };

            foreach (string ab in abilities)
            {
                context.Abilities.Add(new Ability()
                {
                    Category = "skill",
                    Name = ab,
                    Scale = FD.NumberData.GetNumber(0, 100),
                    PersonInfo = pi
                });
            }

            // languages
            string[] langs = new string[] { "English", "Turkish", "French", "German" };

            foreach (string lang in langs)
            {
                context.Abilities.Add(new Ability()
                {
                    Category = "lang",
                    Name = lang,
                    Scale = (Array.IndexOf(langs, lang) + 1) * 25,
                    PersonInfo = pi
                });
            }

            context.SaveChanges();

            //Education..
            for (int i = 0; i < 3; i++)
            {
                Education edu = new Education()
                {
                    SchoolName = FD.PlaceData.GetCounty() + " School",
                    Description = FD.TextData.GetSentence(),
                    StartYear = FD.DateTimeData.GetDatetime().Year,
                    IsCurrent = FD.BooleanData.GetBoolean(),
                    PersonInfo = pi
                };

                if (edu.IsCurrent == false)
                    edu.EndYear = edu.StartYear + 4;

                context.Educations.Add(edu);
            }

            context.SaveChanges();

            // Work Expriences..
            for (int i = 0; i < 3; i++)
            {
                WorkExprience we = new WorkExprience()
                {
                    CompanyName = FD.NameData.GetCompanyName(),
                    Description = FD.TextData.GetSentences(2),
                    StartDate = FD.DateTimeData.GetDatetime(),
                    Title = FD.PlaceData.GetStreetName(),
                    IsCurrent = FD.BooleanData.GetBoolean(),
                    PersonInfo = pi
                };

                if (we.IsCurrent == false)
                    we.EndDate = we.StartDate.AddMonths(FD.NumberData.GetNumber(6, 36));

                context.WorkExpriences.Add(we);
            }

            //logins..
            context.Logins.Add(new Login()
            {
                Id = Guid.NewGuid(),
                Email = "mb@mail.com",
                Password = Crypto.SHA256("123"),
                PersonInfo = pi
            });

            context.SaveChanges();


            // socials..
            string[] socialIcons = new string[] {
                "facebook-official", "instagram", "snapchat","pinterest-p","twitter","linkedin" };

            foreach (string si in socialIcons)
            {
                context.Socials.Add(new Social()
                {
                    Id = Guid.NewGuid(),
                    IconName = si,
                    Url = FD.NetworkData.GetDomain(),
                    PersonInfo = pi
                });
            }

            context.SaveChanges();

        }


    }
}