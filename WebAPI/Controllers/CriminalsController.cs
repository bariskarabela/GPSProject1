using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Business.BusinessAspects.Autofac;

namespace MiniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriminalsController : ControllerBase
    {
        private ICriminalService _criminalService;
        public CriminalsController(ICriminalService criminalService)
        {
            _criminalService = criminalService;
        }
        [HttpGet("getbyid")]
        public IActionResult GetByCriminalId(int id)
        {
            var result = _criminalService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        //[HttpGet("getalls")]
        //public IActionResult GetAlls()
        //{
        //    var result = _criminalService.GetDetails();
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }

        //    return BadRequest(result);
        //}
        [HttpGet("getbytownname")]
        public IActionResult GetByTownName(string name)
        {
            var result = _criminalService.GetByTownName(name);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Criminal criminal)
        {
            var result = _criminalService.Delete(criminal);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Criminal criminal)
        {
            var result = _criminalService.Update(criminal);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Criminal criminal)
        {
            var result = _criminalService.Add(criminal);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _criminalService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getallbydate")]
        public IActionResult GetAllByDate(DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            var result = _criminalService.GetByDate(dateTimeStart, dateTimeFinish);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("getallbydateandcategoryid")]
        public IActionResult GetByCategoryAndDate([FromBody] RequestModel model)
        {
            var result = _criminalService.GetByCategoryAndDate(model.DateTimeStart, model.DateTimeFinish, model.CategoryIds);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        
        [HttpGet("getallpage")]
        
        public IActionResult GetAllPages(int page = 1, int pageSize = 10, string param = null)
        {


            var result = _criminalService.GetAllPages(page, pageSize, param);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
