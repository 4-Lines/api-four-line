using IES300.API.Domain.DTOs.Patrocinador;
using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IES300.API.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrocinadorController : ControllerBase
    {
        private readonly IPatrocinadorService _patrocinadorService;

        public PatrocinadorController(IPatrocinadorService patrocinadorService)
        {
            _patrocinadorService = patrocinadorService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult InserirPatrocinador([FromBody] PatrocinadorInputDTO patrocinadorInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var patrocinadorOutput = _patrocinadorService.InserirPatrocinador(patrocinadorInput);
                if (patrocinadorOutput == null)
                    return BadRequest();

                return Ok(patrocinadorOutput);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
