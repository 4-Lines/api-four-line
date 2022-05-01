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
    [AllowAnonymous]
    public class PatrocinadorController : ControllerBase
    {
        private readonly IPatrocinadorService _patrocinadorService;

        public PatrocinadorController(IPatrocinadorService patrocinadorService)
        {
            _patrocinadorService = patrocinadorService;
        }

        [HttpPost]
        public IActionResult InserirPatrocinador([FromBody] PatrocinadorInputDTO patrocinadorInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var patrocinadorOutput = _patrocinadorService.InserirPatrocinador(patrocinadorInput);
                if (patrocinadorOutput == null)
                    return BadRequest();

                Response.StatusCode = 201;
                return new ObjectResult(patrocinadorOutput);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]       
        public IActionResult ObterTodosPatrocinadores()
        {
            try
            {
                var listaPatrocinadoresOutput = _patrocinadorService.ObterTodosPatrocinadores();
               
                return Ok(listaPatrocinadoresOutput);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult ObterPatrocinadorPorId(int id)
        {
            try
            {
                var patrocinadoresOutput = _patrocinadorService.ObterPatrocinadorPorId(id);

                return Ok(patrocinadoresOutput);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult AlterarPatrocinador([FromBody] PatrocinadorOutputDTO patrocinadorOutput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var patrocinadorOutputRetorno = _patrocinadorService.AlterarPatrocinador(patrocinadorOutput);
                if (patrocinadorOutputRetorno == null)
                    return BadRequest();

                return Ok(patrocinadorOutputRetorno);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
