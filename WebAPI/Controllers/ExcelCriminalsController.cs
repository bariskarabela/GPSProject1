using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using X.PagedList;
using DataAccess.Concrete.EntityFramework;


namespace MiniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelCriminalsController : ControllerBase
    {
        private IExcelCriminalService _excelCriminalService;

   

        public ExcelCriminalsController(IExcelCriminalService excelCriminalService)
        {
            _excelCriminalService = excelCriminalService;
        }
        [HttpGet("getbyid")]
        public IActionResult GetByCriminalId(int id)
        {
            var result = _excelCriminalService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
 
        [HttpPost("delete")]
        public IActionResult Delete(ExcelCriminal criminal)
        {
            var result = _excelCriminalService.Delete(criminal);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(ExcelCriminal excelCriminal)
        {
            var result = _excelCriminalService.Update(excelCriminal);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("movedata")]
        public IActionResult UpdateAndMoveData(int id)
        {
            var result = _excelCriminalService.MoveData(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(ExcelCriminal excelCriminal)
        {
            var result = _excelCriminalService.Add(excelCriminal);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            
            var result = _excelCriminalService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getallpage")]
        public IActionResult GetAllPages(int page=1, int pageSize=10)
        {
           

            var result = _excelCriminalService.GetAllPages(page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("addexcel")]
        public IActionResult Add([FromForm] IFormFile file)
        {
            var result = _excelCriminalService.Add(file);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
