namespace Personservice.Repositories
{
    using System;
    using Personservice.Models;

    /// <summary>
    /// The PeopleRepository interface.
    /// </summary>
    public interface IPeopleRepository
    {
        /// <summary>
        /// The add person.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="Person"/>.
        /// </returns>
        Person AddPerson(Person person);

        /// <summary>
        /// The get person.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Person"/>.
        /// </returns>
        Person GetPerson(Guid id);
    }
}