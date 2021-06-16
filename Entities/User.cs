using System.Reflection.Metadata;

namespace ConsoleApp3.Entities
{
    public class User
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public bool ActiveUser { get; set; }

        public string Post { get; set; }
    }
}
