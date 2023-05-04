using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace TJTransportesApp.Models.ViewModel
{
    public class ContaViewModel
    {
      
        public int Id { get; set; }

        [Required(ErrorMessage = "A Agência é obrigatório", AllowEmptyStrings = false)]
        public string Agencia { get; set; }

        [Required(ErrorMessage = "A Conta é obrigatória", AllowEmptyStrings = false)]
        public string NumeroConta { get; set; }

        [Required(ErrorMessage = "O TIPO é obrigatório", AllowEmptyStrings = false)]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O TITULAR é obrigatório", AllowEmptyStrings = false)]
        public string Titular { get; set; }

    
        public DateTime? DataCAd { get; set; }

      
        public DateTime? DataAlt { get; set; }

      
        public sbyte? Status { get; set; }

        [Required(ErrorMessage = "Campo Banco é obrigatório")]
        public int IdBanco { get; set; }

    }
}