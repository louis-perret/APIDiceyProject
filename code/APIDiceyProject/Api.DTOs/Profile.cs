using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DTOs
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public Profile(Guid id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname; 
        }
    }
}
