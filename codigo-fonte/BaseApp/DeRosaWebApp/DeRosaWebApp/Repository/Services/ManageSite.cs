using DeRosaWebApp.Context;
using DeRosaWebApp.Models.Management;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Repository.Services
{
    public class ManageSite : IManageSite
    {
        private readonly AppDbContext _context;
        public ManageSite(AppDbContext context)
        {
            _context = context;
        }

        public async Task AlterarTextoSobre(ManagementSobre textoAtualizado)
        {
            var managementSobre = await _context.ManagementsSobre.FirstOrDefaultAsync(p => p.Id == 1221);
            if (managementSobre is ManagementSobre)
            {
                managementSobre.TextoSobre = textoAtualizado.TextoSobre;
                managementSobre.Color = textoAtualizado.Color;
                managementSobre.Font = textoAtualizado.Font;
                _context.Entry(managementSobre).Property(ms => ms.TextoSobre).IsModified = true;
                _context.Entry(managementSobre).Property(ms => ms.Font).IsModified = true;
                _context.Entry(managementSobre).Property(ms => ms.Color).IsModified = true;

                await _context.SaveChangesAsync();
            }
        }
        public async Task AlterarTituloDaSemana(ManagementHome tituloAtualizado)
        {
            var managementHome = await _context.ManagementsHome.FirstOrDefaultAsync(p => p.Id == 1221);
            if (managementHome is ManagementHome)
            {
              
                managementHome.TituloSemana = tituloAtualizado.TituloSemana;
                managementHome.Font = tituloAtualizado.Font;
                managementHome.Color = tituloAtualizado.Color;

                _context.Entry(managementHome).Property(p => p.TituloSemana).IsModified = true;
                _context.Entry(managementHome).Property(p => p.Font).IsModified = true;
                _context.Entry(managementHome).Property(p => p.Color).IsModified = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task AlterarTituloSobre(ManagementSobre tituloAtualizado)
        {
            var managementSobre =  await _context.ManagementsSobre.FirstOrDefaultAsync(p => p.Id == 1221);
            if(managementSobre is ManagementSobre)
            {
                managementSobre.TituloSobre = tituloAtualizado.TituloSobre;
                managementSobre.Font = tituloAtualizado.Font;
                managementSobre.Color = tituloAtualizado.Color;
                _context.Entry(managementSobre).Property(ms => ms.TituloSobre).IsModified = true;
                _context.Entry(managementSobre).Property(ms => ms.Color).IsModified = true;
                _context.Entry(managementSobre).Property(ms => ms.Font).IsModified = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ManagementHome> GetManagementsHome()
        {
            var managementHome = await _context.ManagementsHome.FirstOrDefaultAsync(p => p.Id == 1221);
            if(managementHome is ManagementHome)
            {
                return managementHome;
            }
            else
            {
                return await _context.ManagementsHome.FirstOrDefaultAsync(p => p.Id > 0);
            }
        }

        public async Task<ManagementSobre> GetManagementSobre()
        {
            var managementSobre = await _context.ManagementsSobre.FirstOrDefaultAsync(p => p.Id == 1221);
            if (managementSobre is ManagementSobre)
            {
                return managementSobre;
            }
            else
            {
                return await _context.ManagementsSobre.FirstOrDefaultAsync(p => p.Id > 0);
            }

        }

        public async Task UpdateDefault()
        {
            ManagementSobre managementSobre = new ManagementSobre()
            {
                Id = 1221,
                TextoSobre = "Nossos doces são feitos utilizando técnicas artesanais, preservando o sabor autêntico e tradicional que só os doces feitos à mão podem oferecer.\r\n                Em cada receita, em cada mistura, colocamos um pedaço da história e da alma da família, garantindo que cada mordida seja uma experiência única e inesquecível." +
                "Valorizamos a qualidade dos ingredientes, a atenção aos detalhes e, principalmente, o amor pelo que fazemos. Por isso, cada doce não só é delicioso, mas também carrega consigo a essência e a paixão da culinária de vó.",
                TituloSobre = "Bem-vindos à nossa doceria, um lugar onde cada doce é feito à mão com dedicação e muito amor.\r\n        Aqui, revivemos as receitas da vó, preparando doces que tocam o coração e despertam memórias afetivas.",
                ImagemUrl = "~/Imagens/julia.png"
            };
            ManagementHome managementHome = new()
            {
                Id = 1221,
                TituloSemana = "Produtos da Semana"
            };
            await _context.ManagementsSobre.AddAsync(managementSobre);
            await _context.ManagementsHome.AddAsync(managementHome);

            await _context.SaveChangesAsync();
        }

        public async Task VerifyIsNull()
        {
            var managements = _context.ManagementsSobre.ToList();
            var managementsHome = _context.ManagementsHome.ToList();
            if (managements.Count == 0 || managementsHome.Count == 0)
            {
                await UpdateDefault();
            }
        }
    }
}
