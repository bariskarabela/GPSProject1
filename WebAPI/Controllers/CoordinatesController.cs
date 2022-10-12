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
    public class CoordinatesController : ControllerBase
    {
        private ICoordinateService _coordinateService;
        public CoordinatesController(ICoordinateService coordinateService)
        {
            _coordinateService = coordinateService;
        }
        [HttpGet("getbyid")]
        public IActionResult GetByCoordinateId(int id)
        {
            var result = _coordinateService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _coordinateService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getbytownname")]
        public IActionResult GetByTownName(string name)
        {
            var result = _coordinateService.GetByTownName(name);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Coordinate coordinate)
        {
            var result = _coordinateService.Delete(coordinate);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Coordinate coordinate)
        {
            var result = _coordinateService.Update(coordinate);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Coordinate coordinate)
        {
            var result = _coordinateService.Add(coordinate);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
    }
}
