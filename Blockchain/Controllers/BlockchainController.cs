using BlockchainTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlockchainTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        // GET: api/<BlockchainController>
        [HttpGet("mine_block")]
        public string Get()
        {
            Blockchain bc = Blockchain.GetInstance();
            bc.AddBlock();
            return "Parabéns você minerou um novo bloco! Bloco:" + JsonSerializer.Serialize(bc.GetLastestBlock());
        }

        // GET api/<BlockchainController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlockchainController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BlockchainController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlockchainController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
