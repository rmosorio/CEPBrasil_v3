using System;
using System.Collections.Generic;
using System.Text;

namespace CEPBrasil_v3
{
    public class Cidade
    {
        public Cidade() { }

        public Cidade(Estado estado, string nome)
        {

            Estado = estado;
            Nome = nome;
            Bairros = new List<Bairro>();
            Estado.Cidades.Add(this);
        }

        public int Id_Estado { get; set; }
        public int Id_Cidade { get; set; }
        public string Nome { get; set; }        

        public IList<Bairro> Bairros { get; set; }

        public Estado Estado { get; set; }

        public override string ToString()
        {
            return $"Id_Estado: {Id_Estado} Id_Cidade: {Id_Cidade} Nome Cidade: {Nome}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Cidade c = obj as Cidade;

            if (c == null)
            {
                return false;
            }

            return c.Nome == this.Nome && c.Estado.UF == this.Estado.UF;
        }
    }
}
