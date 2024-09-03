using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_03_09
{
    internal class DBManager
    {

        public DbContextOptions<ApplicationContext> GetConectionOptions()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            return optionsBuilder.UseSqlServer(connectionString).Options;

        }


        public void EnsurePopulate()
        {
            using (ApplicationContext db = new ApplicationContext(GetConectionOptions()))
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                List<User> users = new List<User>
                {
                    new User {  Name = "Ivan Petrov", Email = "ivan.petrov@example.com", Password = "Password123" },
                    new User {  Name = "Olga Smirnova", Email = "olga.smirnova@example.com", Password = "SecurePass456" },
                    new User {  Name = "Sergey Ivanov", Email = "sergey.ivanov@example.com", Password = "StrongPass789" }

                };


                List<UserSettings> settings = new List<UserSettings>
                {
                    new UserSettings {  Country = "Germany", RegistrationDate = new DateTime(2023, 1, 15), UserId = 1 },
                    new UserSettings {  Country = "Ukraine", RegistrationDate = new DateTime(2023, 5, 20), UserId = 2 },
                    new UserSettings {  Country = "Litva", RegistrationDate = new DateTime(2023, 7, 30), UserId = 3 }
                };

                //db.Users.AddRange(users);
                db.UserSettings.AddRange(settings);

                db.SaveChanges();
            }
        }

        //GetUserById
        public User? GetUserAndRelated(int id)
        {
            using (ApplicationContext db = new ApplicationContext(GetConectionOptions()))
            {
                return db.Users
                    .Include(u=>u.Settings)
                    .FirstOrDefault(u => u.Id == id);
            }
        }

        public void DeleteUserById(int id)
        {
            using(ApplicationContext db = new ApplicationContext(GetConectionOptions()))
            {
                var delUser = GetUserAndRelated(id);
                if(delUser != null)
                {
                    db.Users.Remove(delUser);
                    db.SaveChanges();

                }
            }
        }
    }
}
