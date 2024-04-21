﻿using System;
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
        [StringLength(50,ErrorMessage = "O nome de usuário deve ter no maximo 50 caracteres")]
        public string NomeUsuario { get; set; } 

        [Required(ErrorMessage = "O número de telefone é obrigatório!")]
        [StringLength(13,ErrorMessage = "O número deve ser no formato: { 035 999999999 }")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório!")]
        [StringLength(11)]
        [MinLength(11,ErrorMessage = "O CPF deve conter 11 caracteres. Lembre de retirar pontos e traços!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "No minimo 8 caracteres!")]
        [StringLength(200,ErrorMessage ="No máximo 200 caracteres!")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O email é obrigatório!")]
        [StringLength(100,ErrorMessage ="O Email deve ter no maximo 100 caracteres!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de nascimento")]
        public DateTime DateNasc { get; set; }

        [ForeignKey(nameof(Endereco))]
        public int IdEndereco { get; set; }

        public List<Endereco> _Enderecos { get; set; } = new List<Endereco>();

        public List<Pedido> _Pedidos { get; set; }
        
        public virtual Carrinho _Carrinho { get; set; }
       
    }
}
