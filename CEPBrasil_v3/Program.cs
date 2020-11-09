using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading;

namespace CEPBrasil_v3
{
    class Program
    {
        static void Main(string[] args)
        {

            // CarregaCEPBrasil();
            ProgramaConsultaCEP();            
        }

        private static void ProgramaConsultaCEP()
        {
            string v_CEP = null;
            string v_CEP_formatado = null;
            //
            Console.WriteLine();
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("***************************** SISTEMA DE CONSULTA DE CEP'S *******************************************");
            Console.WriteLine("******************************************************************************************************");
            //
            do
            {
                Console.WriteLine();
                Console.Write("Informe o [CEP] que deseja consultar ou [sair] para finalizar o programa: ");
                v_CEP = Console.ReadLine();
                //
                if (v_CEP.ToUpper() != "SAIR")
                {
                    v_CEP_formatado = Regex.IsMatch(v_CEP, "[0-9]{5}-?[0-9]{3}") ? Regex.Match(v_CEP, "[0-9]{5}-?[0-9]{3}").ToString().Replace("-", "") : null;
                    //
                    if (v_CEP_formatado == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Formato de CEP inválido!");
                        continue;
                    }
                    //
                    ConsultaCEP(v_CEP_formatado);
                }
            } while (v_CEP.ToUpper() != "SAIR");
        }

        private static void ConsultaCEP(string p_CEP)
        {
            if (p_CEP != "sair")
            {            
                using (var c = new CEPBrasilContext())
                {                
                    var logradouro = c.Logradouros.Include(x => x.Bairro)
                                                  .Include(x => x.Bairro.Cidade)
                                                  .Include(x => x.Bairro.Cidade.Estado)
                                                  .Where(x => x.CEP == p_CEP)
                                                  .FirstOrDefault();
                    if (logradouro == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"O CEP {p_CEP} não existe!");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Resultado => Estado: {logradouro.Bairro.Cidade.Estado.UF} Cidade: {logradouro.Bairro.Cidade.Nome} Bairro: {logradouro.Bairro.Nome} Endereço: {logradouro.Nome} - CEP: {logradouro.CEP}");
                    }
                }
            }
            
        }

        private static void CarregaCEPBrasil()
        {
            var enderecoDoArquivo     = "cep-20190602.csv";
            List<Estado> lista_estado = new List<Estado>();
            //
            Estado v_estado           = null;
            Cidade v_cidade           = null;
            Bairro v_bairro           = null;
            Logradouro v_logradouro   = null;
            //
            string linha              = null;
            Linha_OO linha_OO         = null;
            //
            using (var fluxoDeArquivo = new FileStream(enderecoDoArquivo, FileMode.Open))
            using (var leitor = new StreamReader(fluxoDeArquivo))
            {
                while (!leitor.EndOfStream)
                {
                    //
                    linha = leitor.ReadLine();                    
                    //
                    try
                    {
                        linha_OO = ConverterStringParaObjetos(linha);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Erro na linha: {linha}");
                        continue;
                    }
                    //
                    v_estado = lista_estado.Where(e => e.Equals(linha_OO.Estado)).FirstOrDefault();
                    //
                    if (v_estado == null)
                    {                            
                        v_estado = new Estado(linha_OO.Estado.UF);
                        lista_estado.Add(v_estado);
                    }
                    //
                    v_cidade = v_estado.Cidades.Where(c => c.Equals(linha_OO.Cidade)).FirstOrDefault();
                    //
                    if (v_cidade == null)
                    {
                        v_cidade = new Cidade(v_estado, linha_OO.Cidade.Nome);
                    }
                    //
                    v_bairro = v_cidade.Bairros.Where(b => b.Equals(linha_OO.Bairro)).FirstOrDefault();
                    //
                    if (v_bairro == null)
                    {
                        v_bairro = new Bairro(v_cidade, linha_OO.Bairro.Nome);
                    }
                    //
                    v_logradouro = new Logradouro(v_bairro, linha_OO.Logradouro.Nome, linha_OO.Logradouro.CEP);
                    //
                }
            }
            using (var c = new CEPBrasilContext())
            {
                foreach (var estado in lista_estado)
                {
                    c.Estados.Add(estado);
                    //
                    c.SaveChanges();
                }
            }
        }

        static Linha_OO ConverterStringParaObjetos(string linha)
        {
            try
            {
                //
                string[] campos = linha.Split(';');
                //
                var desc_uf          = campos[0];
                var desc_cidade      = campos[1];
                var desc_bairro      = campos[2];
                var nr_cep           = campos[3];
                var desc_logradouro  = campos[4];
                //
                var estado = new Estado(desc_uf);
                var cidade = new Cidade(estado, desc_cidade);
                var bairro = new Bairro(cidade, desc_bairro);
                var logradouro = new Logradouro(bairro, desc_logradouro, nr_cep);
                //
                return new Linha_OO(estado, cidade, bairro, logradouro);
            }
            catch (Exception)
            {
                throw;
            }            
        }                
    }
}
