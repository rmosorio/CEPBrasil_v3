using System;
using System.Collections.Generic;
using System.Text;

namespace CEPBrasil_v3
{
    public class Bairro
    {
        public Bairro() { }
        
        public Bairro(Cidade cidade, string nome)
        {
            Cidade = cidade;
            Nome = nome;
            Logradouros = new List<Logradouro>();
            Cidade.Bairros.Add(this);
        }

        public int Id_Estado { get; set; }
        public int Id_Cidade { get; set; }
        public int Id_Bairro { get; set; }
        public string Nome { get; set; }

        public IList<Logradouro> Logradouros { get; set; }

        public Cidade Cidade { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Bairro b = obj as Bairro;

            if (b == null)
            {
                return false;
            }

            return b.Nome == this.Nome && b.Cidade.Nome == this.Cidade.Nome && b.Cidade.Estado.UF == this.Cidade.Estado.UF;
        }

    }
}
