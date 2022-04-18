using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFcore.WebApi.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        // GET: Heroi
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return Ok(new Heroi());
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro{ex}");
            }
        } 

        // POST: Heroi/add
        [HttpPost("add")]
        public ActionResult Add()
        {
            // Inserção de dados com relacionamento 1:1
            try
            {
                var heroi = new Heroi 
                { 
                    Nome = "Cavaleiro da lua",
                    Armas = new List<Arma> 
                    { 
                        new Arma { Nome = "bumerangue"},
                    },
                };

                using (var contexto = new HeroiContexto())
                {
                    contexto.Herois.Add(heroi);
                    contexto.SaveChanges();
                }
                return Ok("Você adicionou um novo Heroi");
            }
            catch(Exception ex)
            {
                return BadRequest($"Algo deu errado{ex}");
            }
        }

        [HttpPost("addSecreto")]
        public ActionResult AddSecreto(Heroi model)
        {
            // Inserção de dados com relacionamento 1:1
            try
            {
                using (var contexto = new HeroiContexto())
                {
                    contexto.Herois.Add(model);
                    contexto.SaveChanges();
                }
                return Ok("Você adicionou um novo Heroi");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado{ex}");
            }
        }

        // PUT: Heroi/Update/Id
        [HttpPut("Update/{id}")]
        public ActionResult Update(int id, Heroi model)
        {
            // Atualização de dados com relacionamento 1:1
            try
            {
                using (var contexto = new HeroiContexto())
                {
                    if (contexto.Herois.AsNoTracking() 
                        .FirstOrDefault(h => h.Id == id) != null)
                    {
                        contexto.Herois.Update(model);
                        contexto.SaveChanges();
                    }
                }
                return Ok("Você atualizou um Heroi");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado{ex}");
            }
        }

        // POST: Heroi/id
        [HttpPut("Secreto/{id}")]
        public ActionResult UpdateIdSecreto(int id)
        {
            try
            {
                using (var contexto = new HeroiContexto())
                {
                    var heroi = contexto.Herois.Where
                        (h => h.Id == id).FirstOrDefault();

                    heroi.Nome = "Tartaruga ninja";

                    var heroiSecreto = new Heroi
                    {
                        Identidade = new IdentidadeSecreta
                        {
                            NomeReal = "Sandro Gonçalves ",
                            Id = 1,
                            Heroi = heroi,
                            HeroiId = id
                        },
                    };
                    contexto.Herois.UpdateRange(heroi, heroiSecreto);
                    contexto.SaveChanges();
                }
                return Ok("Você atualizou um Heroi");
            }
            catch
            {
                return Ok();
            }
        }

        // GET: HeroiController/Delete/5
        public ActionResult Delete(int id)
        {
            return Ok();
        }

        // POST: HeroiController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }
    }
}
