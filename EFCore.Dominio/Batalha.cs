using System;
using System.Collections.Generic;

namespace EFCore.Dominio
{
    public class Batalha
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Dt_inicio { get; set; }
        public DateTime Dt_Final { get; set; }
        public List<HeroiBatalha> HeroisBatalhas { get; set; }
    }
}
