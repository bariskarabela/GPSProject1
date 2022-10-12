using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinateImagesController : ControllerBase
    {
        private ICoordinateImageService _coordinateImageService;

        public CoordinateImagesController(ICoordinateImageService coordinateImageService)
        {
            _coordinateImageService = coordinateImageService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile file, [FromForm] CoordinateImage coordinateImage)
        {
            var result = _coordinateImageService.Add(file,coordinateImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(CoordinateImage coordinateImage)
        {
            var result = _coordinateImageService.Delete(coordinateImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("getbycoordinateid")]
        public IActionResult GetByCoordinateId(int coordinateId)
        {
            var result = _coordinateImageService.GetByCoordinateId(coordinateId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
