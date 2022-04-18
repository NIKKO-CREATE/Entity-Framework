using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFcore.WebApi.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepository _repository;
        public BatalhaController(IEFCoreRepository repo)
        {
            _repository = repo;
        }

        // GET: BatalhasController
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return Ok(new Batalha());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro {ex}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBatalha(Batalha model)
        {
            // Inserção de uma batalha
            _repository.Add(model);
            if (await _repository.SaveChangesAsync())
            {
                return Ok("Você adicionou uma nova batalha");
            }

            return BadRequest($"Algo deu errado");
        }

        [HttpPost("addBatalha")]
        public ActionResult add()
        {
            try
            {
                var heroi = new Heroi
                {
                    Nome = "Sandro"
                };

                var batalha = new Batalha
                {
                    ID = 1,
                    Nome = "Batalha de nova York"
                };

                var batalha2 = new Batalha
                {
                    HeroisBatalhas = new List<HeroiBatalha>
                    {
                        new HeroiBatalha { BatalhaId = 1 ,HeroiId = 21, Heroi = heroi, Batalha = batalha}
                    }
                };

                using (var contexto = new HeroiContexto())
                {
                    contexto.Batalhas.AddRange(batalha, batalha2);
                    contexto.SaveChanges();
                }
                return Ok("Você adicionou uma batalha");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado: {ex}");
            }
        }
        // PUT: Batalha/Update/Id
        [HttpPut("Update/{id}")]
        public ActionResult Update(int id, Batalha model)
        {
            // Atualização de dados com relacionamento 1:1
            try
            {
                using (var contexto = new HeroiContexto())
                {
                    if (contexto.Herois.AsNoTracking()
                        .FirstOrDefault(h => h.Id == id) != null)
                    {
                        contexto.Batalhas.Update(model);
                        contexto.SaveChanges();
                    }
                }
                return Ok("Você atualizou uma batalha");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado{ex}");
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            using (var contexto = new HeroiContexto())
            {

                contexto.SaveChanges();
            }

            return Ok("Voce deletou uma batalha");
        }
    }
}
