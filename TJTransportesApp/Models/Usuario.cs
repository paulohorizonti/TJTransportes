using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace TJTransportesApp.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome  é obrigatório", AllowEmptyStrings = false)]
        [Column("NOME")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O LOGIN é obrigatório", AllowEmptyStrings = false)]
        [Column("LOGIN")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A SENHA é obrigatório", AllowEmptyStrings = false)]
        [Column("SENHA")]
        public string Senha { get; set; }


        [Column("TELEFONE")]
        public string Telefone { get; set; }

       
        [Column("SEXO")]
        public string Sexo { get; set; }


        [Column("LOGRADOURO")]
        public string Logradouro { get; set; }

        [Column("CEP")]
        public string Cep { get; set; }

        [Column("NUMERO")]
        public string Numero { get; set; }

        [Column("CIDADE")]
        public string Cidade { get; set; }

        [Column("UF")]
        public string Uf { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("STATUS")]
        public sbyte? Status { get; set; }

        [Column("DATA_CAD")]
        public DateTime? DataCAd { get; set; }

        [Column("DATA_ALT")]
        public DateTime? DataAlt { get; set; }


        [Required(ErrorMessage = "Campo Empresa é obrigatório")]
        [ForeignKey("Empresa")]
        [Column("EMPRESA_ID")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Campo Departamento é obrigatório")]
        [ForeignKey("Departamento")]
        [Column("DEPARTAMENTO_ID")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "Campo Perfil é obrigatório")]
        [ForeignKey("Perfil")]
        [Column("PERFIL_ID")]
        public int IdPerfil { get; set; }

        public virtual Empresa Empresa { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Departamento Departamento { get; set; }


    }
}