using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace MiniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImageService _criminalImageService;

        public ImagesController(IImageService criminalImageService)
        {
            _criminalImageService = criminalImageService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile file, [FromForm] Image criminalImage)
        {
            var result = _criminalImageService.Add(file,criminalImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Image criminalImage)
        {
            var result = _criminalImageService.Delete(criminalImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("getbyid")]
        public IActionResult GetById(int criminalId)
        {
            var result = _criminalImageService.GetById(criminalId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
