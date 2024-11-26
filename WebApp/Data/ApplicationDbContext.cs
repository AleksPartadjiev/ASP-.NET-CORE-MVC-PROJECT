using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using WebApp.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace WebApp.DB

{

    /*
     DbContext е основният клас в Entity Framework, който представлява сесия с базата данни. Той управлява обектите от типа "съществуване" и предоставя API за
      взаимодействие с базата данни, включително операции за извличане, добавяне, актуализиране и изтриване на записи.
     */
    public class ApplicationDbContext : DbContext
    {
        // Този конструктор приема параметър options, който е от тип DbContextOptions<ApplicationDbContext>. Тези опции съдържат конфигурацията
        // за свързване с базата данни (например, информация за тип на базата данни, адрес на сървъра, идентификационни данни и т.н.).
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        //DbSet<User> Users: DbSet представлява колекция от обекти от тип User, които са свързани с таблицата Users в базата данни.
        //Чрез DbSet<User> можем да извършваме CRUD операции (Create, Read, Update, Delete) върху потребителите:
        public DbSet<User> Users { get; set; }

    }
    
}
