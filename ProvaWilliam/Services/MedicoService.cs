using ProvaWilliam.Infra;
using ProvaWilliam.Interface;
using ProvaWilliam.Models;
using ProvaWilliam.Models.DTOs;
using ProvaWilliam.Models.Filter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProvaWilliam.Services
{
    public class MedicoService : IMedicoService
    {

        public async Task<SearchResultViewModel> Listagem(int pageNumber = 1, int pageSize = 10)
        {
            using (var db = new Contexto())
            {
                int skipAmount = (pageNumber - 1) * pageSize;


                var totalRecords = await db.Medico.CountAsync();

                var retorno = await db.Medico
                    .OrderBy(x => x.nome)
                .Skip(skipAmount)
                    .Take(pageSize)
                    .Include(x => x.Especialidade)
                    .ToListAsync();

                var viewModel = new SearchResultViewModel
                {
                    Medicos = retorno,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = totalRecords
                };
                return viewModel;
            }
        }

        public async Task<SearchResultViewModel> GetMedicoByFilterAsync(FilterMedicos filter)
        {
            using (var db = new Contexto())
            {
                int skipAmount = (filter.PageNumber - 1) * filter.PageSize;
                var query = db.Medico
            .Where(x => (filter.nome == null || x.nome.Contains(filter.nome))
             && (filter.crm == null || x.crm.Contains(filter.crm))
             && (filter.id_especialidade == 0 || x.id_especialidade.Equals(filter.id_especialidade)));





                var totalRecords = await query.CountAsync();

                var retorno = await query
                    .OrderBy(x => x.nome)
                    .Skip(skipAmount)
                    .Take(filter.PageSize)
                    .Include(x => x.Especialidade)
                    .ToListAsync();


                var viewModel = new SearchResultViewModel
                {
                    Medicos = retorno,
                    CurrentPage = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalRecords = totalRecords
                };
                return viewModel;
            }
        }
        public async Task<List<Medicos>> Search()
        {
            using (var db = new Contexto())
            {
                return await db.Medico.ToListAsync();
            }
        }

        public async Task<bool> Create(Medicos Medico)
        {
            using (var db = new Contexto())
            {
                db.Medico.Add(Medico);
                var result = await db.SaveChangesAsync();
                return result > 0;
            }
        }

        public async Task<bool> Edit(Medicos Medico)
        {
            using (var db = new Contexto())
            {
                db.Entry(Medico).State = EntityState.Modified;
                var result = await db.SaveChangesAsync();
                return result > 0;
            }
        }

        public async Task<RespostaDTO> Delete(int id)
        {
            var respostaDTO = new RespostaDTO();
            using (var db = new Contexto())
            {
                Medicos Medico = await db.Medico.FindAsync(id);
                db.Medico.Remove(Medico);
                var result = await db.SaveChangesAsync();
               
               
              
               


                respostaDTO.Sucesso = result > 0;
                respostaDTO.Mensagem = respostaDTO.Sucesso ? "Médico removido com sucesso!" : "Não foi possível remover o médico, entre em contato com o suporte";
                return respostaDTO;

            }
        }
        public async Task<Medicos> GetMedicoByIdAsync(int id)
        {
            using (var db = new Contexto())
            {
                Medicos Medico = await db.Medico.Include(x => x.Especialidade).FirstOrDefaultAsync(x => x.id == id);
                return Medico;
            }

        }
        public async Task<List<Medicos>> GetMedicoByEspecialidadeIdAsync(int id)
        {
            using (var db = new Contexto())
            {
                var Medico = await db.Medico.Where(x => x.id_especialidade == id).ToListAsync();
                return Medico;
            }

        }

    }
}