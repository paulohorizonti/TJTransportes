using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace TJTransportesApp.Models
{
    [Table("PERFIL")]
    public class Perfil
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição do perfil é obrigatória **", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Perfil")]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Column("STATUS")]
        public sbyte? Ativo { get; set; }

        [Column("DATA_CAD")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public System.DateTime? DataCAd { get; set; }

        [Column("DATA_ALT")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public System.DateTime? DataAlt { get; set; }


        public string DataAltFormatada
        {
            get { return DataAlt?.ToShortDateString(); }
        }
        public string DataCadFormatada
        {
            get { return DataCAd?.ToShortDateString(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Acesso> Acessso { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}