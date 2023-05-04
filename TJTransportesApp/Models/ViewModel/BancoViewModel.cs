using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace TJTransportesApp.Models.ViewModel
{
    public class BancoViewModel
    {


       
        public int Id { get; set; }

        [Required(ErrorMessage = "O código é obrigatório", AllowEmptyStrings = false)]
        public string Codigo { get; set; }


        [Required(ErrorMessage = "O NOME é obrigatório", AllowEmptyStrings = false)]
       public string Nome { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório", AllowEmptyStrings = false)]
        public string Cep { get; set; }

        public string Logradouro { get; set; }

       
        public string Numero { get; set; }

      
        public string Bairro { get; set; }

     
        public string Cidade { get; set; }

      
        public string Uf { get; set; }

        
        public string TelefoneFixo { get; set; }

       
        public string TelefoneCelular1 { get; set; }


       
        public string TelefoneCelular2 { get; set; }

     
        public sbyte? Ativo { get; set; }

       
        public DateTime? DataCAd { get; set; }

        
        public DateTime? DataAlt { get; set; }

        
    }
}