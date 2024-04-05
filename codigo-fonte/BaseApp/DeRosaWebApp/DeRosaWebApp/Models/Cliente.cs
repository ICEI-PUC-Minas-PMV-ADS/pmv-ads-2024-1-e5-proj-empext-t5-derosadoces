﻿﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DeRosaWebApp.Models
{
    public class Cliente 
    {

        [Key]
        public int Cod_Cliente { get; set; }
        public string Id_User { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(100, ErrorMessage = "O nome só permite 100 caracteres")]
        [DisplayName("Nome completo")]
        public string Nome { get; set; }
        [Required(ErrorMessage = " O usuário é obrigatório!")]
        public string NomeUsuario { get; set; } 
        [Required(ErrorMessage = "O número de telefone é obrigatório!")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O CPF é obrigatório!")]
        [MaxLength(11)]
        [MinLength(11)]
        public string CPF { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória!")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "No minimo 8 caracteres.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O email é obrigatório!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de nascimento")]
        public DateTime DateNasc { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o CEP!")]
        [UIHint("_CepTemplate")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o logradouro!")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Número!")]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        public List<Pedido> _Pedidos { get; set; }
        
        public virtual Carrinho _Carrinho { get; set; }
       
    }
}
