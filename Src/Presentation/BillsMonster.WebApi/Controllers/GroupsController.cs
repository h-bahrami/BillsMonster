using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillsMonster.Application.Groups.Commands.Create;
using BillsMonster.Application.Groups.Queries.List;
using Microsoft.AspNetCore.Mvc;

namespace BillsMonster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : BaseController
    {        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var groups = await Mediator.Send(new GetGroupsListQuery());
            return Ok(groups);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGroupCommand createGroup)
        {
            var groupId = await Mediator.Send(createGroup);
            return Ok(groupId);
        }
    }
}