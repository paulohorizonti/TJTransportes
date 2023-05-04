using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TJTransportesApp.Models
{
    [Table("EMPRESA")]
    public class Empresa
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome fantasia é obrigatório", AllowEmptyStrings = false)]
        [Column("FANTASIA")]
        public string Fantasia { get; set; }

        [Required(ErrorMessage = "O nome Razão Social é obrigatório", AllowEmptyStrings = false)]
        [Column("RAZAO_SOCIAL")]
        public string RazaoSocial { get; set; }

        
        [Column("CEP")]
        public string Cep { get; set; }

        [Column("LOGRADOURO")]
        public string Logradouro { get; set; }

        [Column("NUMERO")]
        public string Numero { get; set; }

        [Column("BAIRRO")]
        public string Bairro { get; set; }

        [Column("CIDADE")]
        public string Cidade { get; set; }

        [Column("UF")]
        public string Uf { get; set; }

        [Column("TEL_FIXO")]
        public string TelefoneFixo { get; set; }

        [Column("TEL_CELULAR")]
        public string TelefoneCelular1 { get; set; }


        [Column("TEL_CELULAR2")]
        public string TelefoneCelular2 { get; set; }

        [Column("STATUS")]
        public sbyte? Ativo { get; set; }

        [Column("DATA_CAD")]
        public DateTime? DataCAd { get; set; }

        [Column("DATA_ALT")]
        public DateTime? DataAlt { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }

    }
}