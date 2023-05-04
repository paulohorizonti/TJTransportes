using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace TJTransportesApp.Models.ViewModel
{
    public class EmpresaViewModel
    {
      
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome fantasia é obrigatório", AllowEmptyStrings = false)]
        public string Fantasia { get; set; }

        [Required(ErrorMessage = "O nome Razão Social é obrigatório", AllowEmptyStrings = false)]
        public string RazaoSocial { get; set; }


      
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