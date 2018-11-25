﻿using Microsoft.AspNetCore.Authorization;

namespace Globalque.Controllers
{
    [Authorize]
    public class ProtectedTeamsController : TeamsController
    {
        // Just reuse another controller, and expose the endpoints in a
        // controller requiring Authorization.

        public ProtectedTeamsController(PeopleDbContext db)
            : base(db)
        { }
    }
}