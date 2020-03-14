using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory
{
    class Program
    {

        private static IServiceProvider _serviceProvider;



        static async Task Main(string[] args)
        {
            RegisterServices();
            var context = _serviceProvider.GetService<UserContext>();


            var users = await AddUsers(context, 500);
            await AddRelations(context, users, 30);

            DisposeServices();
        
            Console.ReadKey();
        }

        private static async Task AddRelations(UserContext context, List<User> users, int quantity)
        {

            List<Relations> relations = new List<Relations>();

            Console.WriteLine("USUARIOS CREADOS " + users.Count);
            foreach (var user in users)
            {
                int max = Faker.RandomNumber.Next(0, quantity);

                List<int> userRelations = new List<int>();

                for (var r = 0; r <= max; r++)
                {

                    int num = Faker.RandomNumber.Next(1, users.Count);

                    while (user.Id == num || userRelations.Any(x => x == num))
                    {
                        num = Faker.RandomNumber.Next(1, users.Count);
                    }

                    userRelations.Add(num);

                }

                relations.AddRange(
                    userRelations.Select((x, i) => new Relations
                    {
                        UserId = user.Id,
                        Relation = RamdomRelation(),
                        User2Id = x
                    }).ToList()
                );

            }
            await context.Relations.AddRangeAsync(relations);

            Console.WriteLine("RELACIONES CREADAS " + relations.Count);
            await context.SaveChangesAsync();
        }


        private static async Task<List<User>> AddUsers(UserContext context, int count){
            
            List<User> users = new List<User>();

            for (var i = 0; i < count; i++)
            {
                var name = Faker.Name.First();
                users.Add(new User
                {
                    Name = name,
                    LastName = Faker.Name.Last(),
                    Phone = RamdomPhone(),
                    Image = RamdomImage(),
                    DocumentNumber = RamdomDocument(),
                    DocumentType = RamdomTypeDocument(),
                    EMail = Faker.Internet.Email(name),
                    Password = "123456",
                });
            }
            context.User.AddRange(users);
            await context.SaveChangesAsync();
            return await context.User.ToListAsync();
        }



        private static void RegisterServices()
        {
            string connectionString = "Server= localhost; Database= document; Integrated Security=True;";
            var collection = new ServiceCollection();
            collection.AddDbContext<UserContext>(options => {
                options.UseSqlServer(connectionString);
            });


            _serviceProvider = collection.BuildServiceProvider();
        }
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }


        public static string RamdomImage() =>
            $"https://i.pravatar.cc/300?img={Faker.RandomNumber.Next(0, 70)}";

        public static Relation RamdomRelation()
        {
            var list = Enum.GetValues(typeof(Relation)).Cast<Relation>().ToList();
            var max = list.Count();
            return list[Faker.RandomNumber.Next(0, max)];
        }

        public static DocumentType RamdomTypeDocument()
        {
            var list = Enum.GetValues(typeof(DocumentType)).Cast<DocumentType>().ToList();
            var max = list.Count();
            return list[Faker.RandomNumber.Next(0, max)];
        }

        private static string RamdomPhone() =>
            $"{Faker.RandomNumber.Next(300000000, 999999999).ToString() + Faker.RandomNumber.Next(0, 9).ToString()}";

        private static string RamdomDocument() =>
            $"{Faker.RandomNumber.Next(300000000, 999999999).ToString() + Faker.RandomNumber.Next(0, 9).ToString()}";

    }
}
