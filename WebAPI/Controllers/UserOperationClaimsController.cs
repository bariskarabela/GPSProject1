﻿using Business.Abstract;
using Business.Constants;
using Core.Entites.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiniAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private IUserOperationClaimService _userOperationClaimService;
        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }
        [HttpPost("update")]
        public IActionResult Update(UpdateClaimDto updateClaimDto)
        {
            var result = _userOperationClaimService.Update(updateClaimDto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(UserOperationClaim userOperationClaim)
        {

            var result = _userOperationClaimService.Add(userOperationClaim);
                if (result.Success)
                {
                    return Ok(result);

                }
                return BadRequest(result);
            
        }
        [HttpGet("getclaimbyuserid")]
        public IActionResult GetByUserId(int id)
        {
            var result = _userOperationClaimService.GetByUserId(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
