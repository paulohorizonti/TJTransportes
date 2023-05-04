using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TJTransportesApp.Models.ViewModel
{
    public class AcessoViewModel
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da tabela é obrigatório", AllowEmptyStrings = false)]
        public String Tabela { get; set; }


      
        public sbyte TabelaV { get; set; }

      
        public sbyte TabelaI { get; set; }

       
        public sbyte TabelaA { get; set; }

      
        public sbyte TabelaE { get; set; }


      
        public String Obs { get; set; }

       
        public System.DateTime? DataCAd { get; set; }

        
        public System.DateTime? DataAlt { get; set; }

       
        public sbyte Ativo { get; set; }

        [Required(ErrorMessage = "Campo Perfil é obrigatório")]
        public int IdPerfil { get; set; }


       
    }
}