using System;
using System.Collections.Generic;
using System.Text;

namespace CEPBrasil_v3
{
    public class Linha_OO
    {
        public Linha_OO(Estado estado, Cidade cidade, Bairro bairro, Logradouro logradouro)
        {
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;
            Logradouro = logradouro;
        }

        public Estado Estado { get; set; }
        public Cidade Cidade { get; set; }
        public Bairro Bairro { get; set; }
        public Logradouro Logradouro { get; set; }
    }
}
