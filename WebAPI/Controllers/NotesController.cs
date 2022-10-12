using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Notescontroller : ControllerBase
    {
        private INoteService _noteService;
        public Notescontroller(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("getnotebycoordinateid")]
        public IActionResult GetNoteByCoordinateId(int coorinateId)
        {
            var result = _noteService.GetNoteByCoordinateId(coorinateId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Note note)
        {
            var result = _noteService.Add(note);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}