using IES300.API.Domain.DTOs.Tema;
using IES300.API.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace IES300.API.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _temaService;
        public TemaController(ITemaService temaService)
        {
            _temaService = temaService;
        }
        [HttpGet]
        public IActionResult ObterTodosTemas()
        {
            try
            {
                var listaTemas = _temaService.ObterTodosTemas();
                return Ok(listaTemas);  //Função de retorno aqui
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult ObterTemaPorId(int id)
        {
            try
            {
                var TemaOutput = _temaService.ObterTemaPorId(id);
                return Ok(TemaOutput);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPut]
        public IActionResult AlterarTema([FromBody] TemaInputDTO temaInput)
        {
            try
            {
                if(!ModelState.IsValid){
                    return BadRequest();
                }
                var alterarOutputRetorno = _temaService.AlterarTema(temaInput);
                return Ok(alterarOutputRetorno);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult InserirTema([FromBody] TemaInputDTO tema)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                
                var response = _temaService.InserirTema(tema);

                if (response == null)
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeletarTema(int id)
        {
            try
            {
                _temaService.DeletarTema(id);
                return Ok(new { msg = $"Tema Id: {id} deletado com sucesso"});
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }
}
