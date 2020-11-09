using System;
using System.Collections.Generic;
using System.Text;

namespace CEPBrasil_v3
{
    public class Estado
    {

        public Estado()
        {            
        }


        public Estado(string uf)
        {
            UF = uf;
            Cidades = new List<Cidade>();
        }

        public int Id_Estado { get; set; }

        public string UF { get; set; }

        public IList<Cidade> Cidades { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Estado e = obj as Estado;

            if (e == null)
            {
                return false;
            }

            return e.UF == this.UF;
        }

        public override string ToString()
        {
            return $"UF: {UF}";
        }


    }
}
