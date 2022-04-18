using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EFcore.WebApi.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        // GET Heroi
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            // Retorna os dados do banco
            using (var contexto = new HeroiContexto())
            {
               var listHeroi = contexto.Herois.ToList();
                contexto.SaveChanges();

                return Ok(listHeroi);
            }
        }
        // GET Heroi/Filtro
        [HttpGet("filtro/{nome}")]
        public ActionResult<IEnumerable<string>> GetFiltro(string nome)
        {
            // Retorna os dados do banco
            using (var contexto = new HeroiContexto())
            {
                var listHeroi = contexto.Herois.Where(h => h.Nome.Contains(nome)).ToList();

                contexto.SaveChanges();

                return Ok(listHeroi);
            }
        }
        // GET Heroi/nomeHeroi
        [HttpGet("{nomeHeroi}")]
        public ActionResult GetInsere(string nomeHeroi)
        {
            //Insere na tabela 
            var heroi = new Heroi { Nome = "Miranha" };
            using (var contexto = new HeroiContexto())
            {
                contexto.Herois.Add(heroi); 
                contexto.SaveChanges();
            }
            return Ok();
        }
        // GET Heroi/Id
        [HttpGet("atualiza/{id}")]
        public ActionResult GetAtualiza(int id)
        {
            //Atualiza na tabela 
            using (var contexto = new HeroiContexto())
            {
                var heroi = contexto.Herois.Where
                    (h => h.Id == id).FirstOrDefault();

                heroi.Nome = "Pantera negra";
                contexto.SaveChanges();
            }
            return Ok();
        }
        // GET Heroi/addrange
        [HttpGet("addrange")]
        public ActionResult GetRange()
        {
            //Atualiza na tabela 
            using (var contexto = new HeroiContexto())
            {
                contexto.AddRange(
                    new Heroi { Nome = "Captão america " },
                    new Heroi { Nome = "Gavião arqueiro" },
                    new Heroi { Nome = "Falcão negro" },
                    new Heroi { Nome = "Mutantes " },
                    new Heroi { Nome = "Batman " },
                    new Heroi { Nome = "X-men " },
                    new Heroi { Nome = "Coringa " },
                    new Heroi { Nome = "Captã Marvel" }
                );
                contexto.SaveChanges();
            }
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE Heroi/delete/id
        [HttpGet("delete/{id}")]
        public void Delete(int id)
        {
            using (var contexto = new HeroiContexto())
            {
                var heroi = contexto.Herois.Where
                    (h => h.Id == id).Single();

                contexto.Herois.Remove(heroi);
                contexto.SaveChanges();
            }
        }
    }
}
