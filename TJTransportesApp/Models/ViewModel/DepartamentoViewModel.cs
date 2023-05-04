using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace TJTransportesApp.Models.ViewModel
{
    public class DepartamentoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do departamento é ", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        
        public sbyte? Ativo { get; set; }

       
        public System.DateTime? DataCAd { get; set; }

      
        public System.DateTime? DataAlt { get; set; }

       
    }
}