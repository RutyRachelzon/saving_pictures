using BL;
using DL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SavingPictures.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingPicturesController : ControllerBase
    {
        public ISavingPicturesBL _savingPicturesBL;
        // GET: api/<SavingPicturesController>
        

        public SavingPicturesController(ISavingPicturesBL savingPicturesBL)
        {
            _savingPicturesBL = savingPicturesBL;
        }
        [HttpGet]
        public async Task<ActionResult<CollectionInfo>> Get(string collectionId)
        {
             CollectionInfo res=await _savingPicturesBL.getCollectionName(collectionId);
             return Ok(res);
        }
        
        // GET api/<SavingPicturesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SavingPicturesController>
        [HttpPost]
        public async Task<ActionResult<NewPicture[]>> Post([FromBody] NewPicture[] picturesToAdd)
        {
            try
            {
                NewPicture[] result = await _savingPicturesBL.addNewPicture(picturesToAdd);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new pictures: {ex.Message}");
                return StatusCode(500, "Internal server error"); // Return an appropriate status code and message
            }
        }

        // PUT api/<SavingPicturesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SavingPicturesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
