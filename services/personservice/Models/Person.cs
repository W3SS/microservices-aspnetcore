using System;

namespace Personservice.Models
{
    /// <summary>
    /// The person.
    /// </summary>
    public class Person 
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id {get; set;}

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }
    }
}