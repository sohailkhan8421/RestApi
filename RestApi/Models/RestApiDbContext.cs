using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using Bogus;


namespace RestApi.Data
{
    public class RestApiDbContext : DbContext
    {
        public RestApiDbContext(DbContextOptions<RestApiDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Users> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    var id = 1;

        //    var fakeUsers = new Faker<Users>()
        //        .RuleFor(u => u.Id, _ => id++)
        //        .RuleFor(u => u.FullName, f => f.Name.FullName())
        //        .RuleFor(u => u.Email, f => f.Internet.Email())
        //        .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("9#########"))
        //        .RuleFor(u => u.Username, f => f.Internet.UserName())
        //        .RuleFor(u => u.Password, f => f.Internet.Password()) // or hash if you need
        //        .RuleFor(u => u.Address, f => f.Address.StreetAddress())
        //        .RuleFor(u => u.City, f => f.Address.City())
        //        .RuleFor(u => u.Role, f => f.PickRandom(new[] { "Admin", "User", "Manager" }));

        //    var userList = fakeUsers.Generate(20);

        //    modelBuilder.Entity<Users>().HasData(userList);
        //}
    }
}
