namespace teamservice.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    using teamservice.Models;
    using teamservice.Repositories;

    [Route("[controller]")]
	public class TeamsController : Controller
	{
		ITeamRepository repository;

		public TeamsController(ITeamRepository repo) 
		{
			this.repository = repo;
		}

		[HttpGet]
        public virtual IActionResult GetAllTeams()
		{
			return this.Ok(this.repository.List());
		}

		[HttpGet("{id}")]
        public IActionResult GetTeam(Guid id)
		{
			Team team = this.repository.Get(id);		

			if (team != null) // I HATE NULLS, MUST FIXERATE THIS.			  
			{				
				return this.Ok(team);
			}

		    return this.NotFound();
		}		

		[HttpPost]
		public virtual IActionResult CreateTeam([FromBody]Team newTeam) 
		{
			this.repository.Add(newTeam);			

			//TODO: add test that asserts result is a 201 pointing to URL of the created team.
			//TODO: teams need IDs
			//TODO: return created at route to point to team details			
			return this.Created($"/teams/{newTeam.Id}", newTeam);
		}

		[HttpPut("{id}")]
		public virtual IActionResult UpdateTeam([FromBody]Team team, Guid id) 
		{
			team.Id = id;
						
			if(this.repository.Update(team) == null) {
				return this.NotFound();
			}

		    return this.Ok(team);
		}

		[HttpDelete("{id}")]
        public virtual IActionResult DeleteTeam(Guid id)
		{
			Team team = this.repository.Delete(id);

			if (team == null) {
				return this.NotFound();
			}

		    return this.Ok(team.Id);
		}
	}
}
