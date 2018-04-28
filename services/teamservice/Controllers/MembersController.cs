namespace teamservice.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using teamservice.Models;
    using teamservice.Repositories;

    [Route("/teams/{teamId}/[controller]")]
	public class MembersController : Controller
	{
		ITeamRepository repository;

		public MembersController(ITeamRepository repo) 
		{
			this.repository = repo;
		}

		[HttpGet]
		public virtual IActionResult GetMembers(Guid teamID) 
		{
			var team = this.repository.Get(teamID);
			
			if(team == null) {
				return this.NotFound();
			}

		    return this.Ok(team.Members);
		}
		

		[HttpGet]
		[Route("/teams/{teamId}/[controller]/{memberId}")]		
		public virtual IActionResult GetMember(Guid teamID, Guid memberId) 
		{
			Team team = this.repository.Get(teamID);
			
			if(team == null) {
				return this.NotFound();
			}

		    var q = team.Members.Where(m => m.Id == memberId);

		    if(!q.Any()) {
		        return this.NotFound();
		    }

		    return this.Ok(q.First());
		}

		[HttpPut]
		[Route("/teams/{teamId}/[controller]/{memberId}")]		
		public virtual IActionResult UpdateMember([FromBody]Member updatedMember, Guid teamID, Guid memberId) 
		{
			var team = this.repository.Get(teamID);
			
			if(team == null) {
				return this.NotFound();
			}

		    var q = team.Members.Where(m => m.Id == memberId);

		    if(!q.Any()) {
		        return this.NotFound();
		    }

		    team.Members.Remove(q.First());
		    team.Members.Add(updatedMember);
		    return this.Ok();
		}

		[HttpPost]
		public virtual IActionResult CreateMember([FromBody]Member newMember, Guid teamID) 
		{
			Team team = this.repository.Get(teamID);
			
			if(team == null) {
				return this.NotFound();
			}

		    team.Members.Add(newMember);
		    var teamMember = new {TeamID = team.Id, MemberID = newMember.Id};
		    return this.Created($"/teams/{teamMember.TeamID}/[controller]/{teamMember.MemberID}", teamMember);
		}

		[HttpGet]
		[Route("/members/{memberId}/team")]
		public IActionResult GetTeamForMember(Guid memberId)
		{
			var teamId = this.GetTeamIdForMember(memberId);
			if (teamId != Guid.Empty) {
				return this.Ok(new {
					TeamID = teamId
				});
			}

		    return this.NotFound();
		}

		private Guid GetTeamIdForMember(Guid memberId) 
		{
			foreach (var team in this.repository.List()) {
				var member = team.Members.FirstOrDefault( m => m.Id == memberId);
				if (member != null) {
					return team.Id;
				}
			}
			return Guid.Empty;
		}    
    }
}