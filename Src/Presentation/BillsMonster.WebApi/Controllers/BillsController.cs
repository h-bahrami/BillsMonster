using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BillsMonster.Application.Bills.Queries.Detail;
using BillsMonster.Application.Bills.Queries.List;
using Microsoft.AspNetCore.Mvc;

namespace BillsMonster.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : BaseController
    {
       
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {             
            var result = await Mediator.Send(new GetBillsListQuery());
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await Mediator.Send(new GetBillDetailsQuery() { Id = id});
            return Ok(item);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
