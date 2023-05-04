using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TJTransportesApp.Models
{
    [Table("CONTA")]
    public class Conta
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A Agência é obrigatório", AllowEmptyStrings = false)]
        [Column("AGENCIA")]
        public string Agencia { get; set; }

        [Required(ErrorMessage = "A Conta é obrigatória", AllowEmptyStrings = false)]
        [Column("CONTA")]
        public string NumeroConta{ get; set; }

        [Required(ErrorMessage = "O TIPO é obrigatório", AllowEmptyStrings = false)]
        [Column("TIPO")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O TITULAR é obrigatório", AllowEmptyStrings = false)]
        [Column("TITULAR")]
        public string Titular { get; set; }

        [Column("DATA_CAD")]
        public DateTime? DataCAd { get; set; }

        [Column("DATA_ALT")]
        public DateTime? DataAlt { get; set; }

        [Column("STATUS")]
        public sbyte? Status { get; set; }

        [Required(ErrorMessage = "Campo Empresa é obrigatório")]
        [ForeignKey("Banco")]
        [Column("BANCO_ID")]
        public int IdBanco { get; set; }

        public virtual Banco Banco { get; set; }


    }
}