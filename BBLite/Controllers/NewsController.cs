using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BBLite.Data;

namespace BBLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _repository;

        public NewsController(INewsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("articles")]
        public async Task<IActionResult> GetPage(int page = 1)
        {
            return Ok(await _repository.GetAll(page));
        }

        [HttpGet("articles/{path}")]
        public async Task<IActionResult> GetArticle(string path = "")
        {
            return Ok(await _repository.GetArticle(path));
        }
    }
}