namespace personservice.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Personservice.Models;
    using Personservice.Repositories;

    /// <inheritdoc />
    /// <summary>
    /// The memory people repository.
    /// </summary>
    public class MemoryPeopleRepository : IPeopleRepository
    {
        /// <summary>
        /// The _people.
        /// </summary>
        private static ICollection<Person> people;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryPeopleRepository"/> class.
        /// </summary>
        public MemoryPeopleRepository()
        {
            people = new List<Person>();
        }

        /// <summary>
        /// The add person.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="Person"/>.
        /// </returns>
        public Person AddPerson(Person person) 
        {
            people.Add(person);
            return person;
        }

        /// <inheritdoc />
        /// <summary>
        /// The get person.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T:Personservice.Models.Person" />.
        /// </returns>
        public Person GetPerson(Guid id)
        {
            return people.FirstOrDefault(t => t.Id == id);            
        }
    }
}