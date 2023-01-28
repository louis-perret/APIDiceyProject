using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model
{
    /// <summary>
    /// Concrete class that represents a Profile with only a name, surname and Id
    /// </summary>
    public class SimpleProfile : Profile
    {

        /// <summary>
        /// Constructor with parameters, initializes the Id at Guid.Empty
        /// </summary>
        /// <param name="name">player's name</param>
        /// <param name="surname">player's surname</param>
        public SimpleProfile(string name, string surname) : base(name, surname)
        {
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="id">player's id</param>
        /// <param name="name">player's name</param>
        /// <param name="surname">player's surname</param>
        /// <exception cref="ArgumentException"></exception>
        public SimpleProfile(Guid id, string name, string surname) : base(id, name, surname)
        {
        }
    }

}
