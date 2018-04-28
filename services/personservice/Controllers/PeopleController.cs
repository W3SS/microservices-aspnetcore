namespace Personservice.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Personservice.Models;
    using Personservice.Repositories;

    /// <summary>
    /// The people controller.
    /// </summary>
    [Route("api/[controller]")]
	public class PeopleController : Controller
	{
	    /// <summary>
	    /// The _repository.
	    /// </summary>
	    private readonly IPeopleRepository repository;

	    /// <summary>
	    /// Initializes a new instance of the <see cref="PeopleController"/> class.
	    /// </summary>
	    /// <param name="repository">
	    /// The repository.
	    /// </param>
	    public PeopleController(IPeopleRepository repository)
	    {
	        this.repository = repository;
	    }

        /// <summary>
        /// The get all people.
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet("all")]
        public virtual IActionResult GetAllPeople()
        {
            return this.Ok("person");
		}

	    /// <summary>
	    /// The get person.
	    /// </summary>
	    /// <param name="id">
	    /// The id.
	    /// </param>
	    /// <returns>
	    /// </returns>
	    [HttpGet("person")]
        public virtual IActionResult GetPerson(Guid id)
		{
			var person = this.repository.GetPerson(id);

			if(person == null)
				return this.NotFound();
		    return this.Ok(person);
		}

	    /// <summary>
	    /// The create person.
	    /// </summary>
	    /// <param name="newPerson">
	    /// The new person.
	    /// </param>
	    /// <returns>
	    /// </returns>
	    [HttpPut("create")]
	    public virtual IActionResult CreatePerson([FromBody]Person newPerson) 
		{
			newPerson = this.repository.AddPerson(newPerson);
			return this.Created($"/people/{newPerson.Id}", newPerson);
		}
	}
}