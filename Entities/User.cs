using System.Runtime.Serialization;
namespace Entities
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public byte[] Password { get; set; }

        public User()
        {}
    }
}
