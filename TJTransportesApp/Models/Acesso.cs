using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace TJTransportesApp.Models
{
    [Table("ACESSO")]
    public class Acesso
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da tabela é obrigatório", AllowEmptyStrings = false)]
        [Column("TABELA")]
        public String Tabela { get; set; }

       
        [Column("TABELA_V")]
        public sbyte? TabelaV { get; set; }

        [Column("TABELA_I")]
        public sbyte? TabelaI { get; set; }

        [Column("TABELA_A")]
        public sbyte? TabelaA { get; set; }

        [Column("TABELA_E")]
        public sbyte? TabelaE { get; set; }

       
        [Column("OBSERVACAO")]
        public String Obs { get; set; }

        [Column("DATA_CAD")]
        public System.DateTime? DataCAd { get; set; }

        [Column("DATA_ALT")]
        public System.DateTime? DataAlt { get; set; }

        public string DataAltFormatada
        {
            get { return DataAlt?.ToShortDateString(); }
        }
        public string DataCadFormatada
        {
            get { return DataCAd?.ToShortDateString(); }
        }

        [Column("STATUS")]
        public sbyte? Ativo { get; set; }

        [Required(ErrorMessage = "Campo Perfil é obrigatório")]
        [ForeignKey("Perfil")]
        [Column("PERFIL_ID")]
        public int IdPerfil { get; set; }

       
        public virtual Perfil Perfil { get; set; }


    }
}