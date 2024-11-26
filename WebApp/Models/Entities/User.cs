using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Constructor
        public User(string name, string email, string password)
        {
            Id = Guid.NewGuid(); // or however you want to set it
            Name = name;
            Email = email;
            Password = password;
        }
    }

}

