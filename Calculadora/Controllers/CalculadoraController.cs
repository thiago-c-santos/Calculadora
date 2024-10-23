using Calculadora.Models;
using Calculadora.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Calculadora.Controllers
{
    [Route("api/calculadora")]
    [ApiController]
    public class CalcController : ControllerBase
    {
        private readonly ICalculadoraService _calcService;

        public CalcController(ICalculadoraService calcService)
        {
            _calcService = calcService;
        }

        /// <summary>
        /// Retorna a soma de uma lista de valores
        /// </summary>
        /// <param name="valores"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("soma")]
        public IActionResult Somar([FromQuery] List<float> valores)
        {
            return Ok(_calcService.Somar(valores));
        }

        /// <summary>
        /// Retorna o resultado da subtração entre dois valores
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("subtracao")]
        public IActionResult Subtrair([FromQuery] float x, float y)
        {
            return Ok(_calcService.Subtrair(x, y));
        }

        /// <summary>
        /// Retorna o resultado da divisão entre dois valores
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("divisao")]
        [ProducesResponseType(typeof(Resposta), 200)]
        [ProducesResponseType(typeof(Resposta), 400)]
        public IActionResult Divisao([FromQuery] float x, float y)
        {
            var result = _calcService.Divisao(x, y);
            var statusCode = result.Sucesso ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
            return StatusCode(statusCode, result);
        }

        /// <summary>
        /// Retorna, se possível, a raiz quadrada perfeita de um valor
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("raizQuadrada")]
        [ProducesResponseType(typeof(Resposta), 200)]
        [ProducesResponseType(typeof(Resposta), 400)]
        public IActionResult RaizQuadrada([FromQuery] int x)
        {
            var result = _calcService.RaizQuadrada(x);
            var statusCode = result.Sucesso ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
            return StatusCode(statusCode, result);
        }

        /// <summary>
        /// Retorna a raiz quadrada mais próxima de um valor
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("raizQuadradaNaoExata")]
        [ProducesResponseType(typeof(Resposta), 200)]
        [ProducesResponseType(typeof(Resposta), 400)]
        public IActionResult RaizQuadradaNaoExata([FromQuery] double x)
        {
            var result = _calcService.RaizQuadradaNaoExata(x);
            var statusCode = result.Sucesso ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
            return StatusCode(statusCode, result);
        }

        /// <summary>
        /// Retorna a raiz quadrada mais próxima de um valor
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("calculoPersonalizado")]
        [ProducesResponseType(typeof(Resposta), 200)]
        [ProducesResponseType(typeof(Resposta), 400)]
        public IActionResult CalculoPersonalizado(string formula)
        {
            var result = _calcService.CalculoPersonalizado(formula);
            var statusCode = result.Sucesso ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
            return StatusCode(statusCode, result);
        }
    }
}
