using System.Runtime.Serialization;

namespace ENTOBEL_AURAVINA_API.Resources
{
    public class AddUserViewModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }

        public AddUserViewModel(string name, string username, string password)
        {
            UserName = username;
            Password = password;
            Name = name;
        }
    }
}
