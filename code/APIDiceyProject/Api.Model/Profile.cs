using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model
{
    public abstract class Profile : IEquatable<Profile>
    {
        /// <summary>
        /// Palyer's id
        /// </summary>
        public Guid Id { get; private set; }


        /// <summary>
        /// Player's name
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Name));
                }
                _name = value;
            }
        }
        private string _name;




        /// <summary>
        /// Player's surname
        /// </summary>
        public string Surname
        {
            get => _surname;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Name));
                }
                _surname = value;
            }
        }
        private string _surname;

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="id">player's id</param>
        /// <param name="name">player's name</param>
        /// <param name="surname">player's surname</param>
        /// <exception cref="ArgumentException"></exception>
        public Profile(Guid id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }

        /// <summary>
        /// Constructor with parameters, initializes the Id at Guid.Empty
        /// </summary>
        /// <param name="name">player's name</param>
        /// <param name="surname">player's surname</param>
        public Profile(string name, string surname) : this(Guid.Empty, name, surname)
        {
        }

        /// <summary>
        /// Equals method wioth a profile. If both Profiles have an Id, then only the Ids are compared.
        /// Else, both the name and surname are compared
        /// </summary>
        /// <param name="other">The other profile we want to compare with this one</param>
        /// <returns>true if both Profiles are equal, false otherwise</returns>
        public bool Equals(Profile? other)
        {
            if (other == null) return false;

            if (Id.Equals(Guid.Empty) || other.Id.Equals(Guid.Empty))
                return Name.Equals(other.Name) && Surname.Equals(other.Surname);
            else
                return Id.Equals(other.Id);
        }

        /// <summary>
        /// Equals method with an object of unknown type 
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>true if both objects are equal, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!GetType().Equals(obj.GetType())) return false;

            return Equals((Profile)obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Surname);
        }

    }

}
