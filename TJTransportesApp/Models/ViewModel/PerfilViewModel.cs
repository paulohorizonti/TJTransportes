using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TJTransportesApp.Models.ViewModel
{
    public class PerfilViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Descrição do Perfil é obrigatória **", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Perfil")]
        public string Descricao { get; set; }

        
        public sbyte? Ativo { get; set; }

        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public System.DateTime? DataCAd { get; set; }

        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public System.DateTime? DataAlt { get; set; }

       
    }
}