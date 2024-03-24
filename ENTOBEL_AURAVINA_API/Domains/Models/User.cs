namespace ENTOBEL_AURAVINA_API.Domains.Models
{
    public class User 
    {
        public int Id {  get; set; }
        public string Name { get; set;}
        public string UserName { get; set;}
        public string Password { get; set;}

        public User(string name, string userName, string password)
        {

            Name = name;
            UserName = userName;
            Password = password;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public User()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
    }
}
