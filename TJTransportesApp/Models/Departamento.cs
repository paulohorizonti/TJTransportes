using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace TJTransportesApp.Models
{
    [Table("DEPARTAMENTO")]
    public class Departamento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome fantasia é obrigatório", AllowEmptyStrings = false)]
        [Column("NOME")]
        public string Nome { get; set; }

        [Column("STATUS")]
        public sbyte? Ativo { get; set; }

        [Column("DATA_CAD")]
        public System.DateTime? DataCAd { get; set; }

        [Column("DATA_ALT")]
        public System.DateTime? DataAlt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
       
    }
}