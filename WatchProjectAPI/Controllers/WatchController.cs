using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchProjectAPI.Model;
using WatchProjectAPI.Repositories;
using WatchProjectAPI.Services;

namespace WatchProjectAPI.Controller
{
 
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WatchController : ControllerBase
    {
        private readonly IWatchService _watchService;
       

        public WatchController(IWatchService watchService)
        {
            _watchService = watchService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(string? watchName)
        {
            var data = await _watchService.GetAll(watchName);

            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetRandom()
        {
            try
            {

                return Ok(await _watchService.GetRandom());
            }
            catch (Exception ex) { throw ex; }

           
        }

        [HttpGet]
        public async Task<IActionResult> GetRandom8()
        {
            try
            {
                return Ok(await _watchService.GetRandom8());
            }
            catch (Exception ex) { throw ex; }


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(Guid id)
        {
            var data = await _watchService.GetbyId(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

       
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Watch model)
        {
            try
            {
                var res = await _watchService.SaveData(model);
                     return Ok(res);
            }
               catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Watch model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _watchService.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _watchService.Delete(id);
            return Ok();
        }



    }
}
