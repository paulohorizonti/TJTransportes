using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace TJTransportesApp.Models.ViewModel
{
    public class UsuarioViewModel
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome  é obrigatório", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O LOGIN é obrigatório", AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Required(ErrorMessage = "A SENHA é obrigatório", AllowEmptyStrings = false)]
        public string Senha { get; set; }


       
        public string Telefone { get; set; }


      
        public string Sexo { get; set; }


       
        public string Logradouro { get; set; }

       
        public string Cep { get; set; }

       
        public string Numero { get; set; }

      
        public string Cidade { get; set; }

      
        public string Uf { get; set; }

       
        public string Email { get; set; }

       
        public sbyte? Status { get; set; }

       
        public DateTime? DataCAd { get; set; }

     
        public DateTime? DataAlt { get; set; }


        [Required(ErrorMessage = "Campo Empresa é obrigatório")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Campo Departamento é obrigatório")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "Campo Perfil é obrigatório")]
        public int IdPerfil { get; set; }

       
    }
}