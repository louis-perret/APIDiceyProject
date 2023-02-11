using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Entities
{

        /// <summary>
        /// Represents the entity Profile on our database
        /// </summary>
        public class Profile: IEquatable<Profile?>
        {

            [Key]
            /// <summary>
            /// Id of profile
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Name of profile
            /// </summary>

            public string Name { get; set; }

            /// <summary>
            /// Surname of profile
            /// </summary>
            public string Surname { get; set; }

            public ICollection<Throw> Throws { get; set; } = new List<Throw>();

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="id">id of profile</param>
            /// <param name="name">name of profile</param>
            /// <param name="surname">surname of profile</param>
            public Profile(Guid id, string name, string surname) : this(name, surname)
            {
                Id = id;
            }

            /// <summary>
            /// Constructor without id
            /// </summary>
            /// <param name="name">name of profile</param>
            /// <param name="surname">surname of profile</param>
            public Profile(string name, string surname)
            {
                Name = name;
                Surname = surname;
            }

            /// <summary>
            /// Return true if obj is equal to the calling object
            /// </summary>
            /// <param name="obj">Obj to compare</param>
            /// <returns></returns>
            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (GetType() != obj.GetType()) return false;

                return Equals(obj as Profile);
            }

            /// <summary>
            /// Return true if obj is equal to the calling object
            /// </summary>
            /// <param name="obj">Obj to compare</param>
            /// <returns></returns>
            public bool Equals(Profile? other)
            {
                return other is not null && Id == other.Id;
            }

            /// <summary>
            /// Hash method of ProfileEntity
            /// </summary>
            /// <returns>this instance's HashCode</returns>
            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Name, Surname);
            }
        }
}
