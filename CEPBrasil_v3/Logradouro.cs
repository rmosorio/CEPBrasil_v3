using System;
using System.Collections.Generic;
using System.Text;

namespace CEPBrasil_v3
{
    public class Logradouro
    {
        public Logradouro() { }
        
        public Logradouro(Bairro bairro, string nome, string cep)
        {
            CEP = cep;
            Nome = nome;
            Bairro = bairro;
            Bairro.Logradouros.Add(this);
        }

        public int Id_Estado { get; set; }
        public int Id_Cidade { get; set; }
        public int Id_Bairro { get; set; }
        public int Id_Logradouro { get; set; }
        public string CEP { get; set; }
        public string Nome { get; set; }

        public Bairro Bairro { get; set; }
    }
}
