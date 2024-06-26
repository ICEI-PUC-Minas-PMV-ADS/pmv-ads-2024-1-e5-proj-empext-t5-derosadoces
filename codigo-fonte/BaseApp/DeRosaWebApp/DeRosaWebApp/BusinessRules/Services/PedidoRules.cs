﻿using DeRosaWebApp.BusinessRules.Interfaces;
using DeRosaWebApp.BusinessRules.Validations;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;

namespace DeRosaWebApp.BusinessRules.Services
{
    public class PedidoRules : IPedidoRules
    {
        // Não use AppDbContext aqui, por questões segurança

        public void VerificaComemorativosSeteDiasAntecedencia(List<Produto> produtosEmPedido, DateTime dataEntrega)
        {
            foreach (var item in produtosEmPedido)
            {
                var dataMinimaPermitida = DateTime.Now.AddDays(7).Date;

                DeRosaExceptionValidation.When(dataEntrega.Date < dataMinimaPermitida,
                    $"Pedidos comemorativos só são permitidos com até 7 dias de antecedência! Pedidos comemorativos: {item.Nome}");
            }
        }
        public void VerificaCidade(string cidade)
        {
            string cidadePocos = "Poços de Caldas";
            DeRosaExceptionValidation.When(!string.Equals(cidade, cidadePocos), "Os pedidos só são aceitos em endereços de Poços de Caldas - MG");
        }

    }
}
