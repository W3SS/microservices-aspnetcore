namespace teamservice.Repositories
{
    using System;
    using System.Collections.Generic;

    using teamservice.Models;

    public interface ITeamRepository {
	    IEnumerable<Team> List();
		Team Get(Guid id);
		Team Add(Team team);
		Team Update(Team team);		
		Team Delete(Guid id);
	}
}