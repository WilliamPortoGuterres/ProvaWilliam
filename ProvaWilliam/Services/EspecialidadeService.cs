using ProvaWilliam.Infra;
using ProvaWilliam.Interface;
using ProvaWilliam.Models;
using ProvaWilliam.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProvaWilliam.Services
{
    public class EspecialidadeService : IEspecialidadeService
    {
        private readonly IMedicoService _medicoService;

        public EspecialidadeService(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        public async Task<List<Especialidade>> GetAllAsync()
        {
            using (var db = new Contexto())
            {
                return await db.Especialidade.ToListAsync();
            }

        }
        public async Task<SearchResultViewModel> Listagem(int pageNumber = 1, int pageSize = 10)
        {
            using (var db = new Contexto())
            {
                int skipAmount = (pageNumber - 1) * pageSize;


                var totalRecords = await db.Especialidade.CountAsync();

                var retorno = await db.Especialidade
                    .OrderBy(x => x.descricao)
                .Skip(skipAmount)
                    .Take(pageSize)
       
                    .ToListAsync();

                var viewModel = new SearchResultViewModel
                {
                    Especialidades = retorno,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = totalRecords
                };
                return viewModel;
            }
        }

        public async Task<List<EspecialidadeDTO>> SearchEspecialidades()
        {
            using (var db = new Contexto())
            {
                var result = await db.Especialidade
                          .Select(e => new EspecialidadeDTO { id = e.id, descricao = e.descricao })
                          .ToListAsync();

                return result;
            }

        }

        public async Task<bool> Create(Especialidade especialidade)
        {
            using (var db = new Contexto())
            {
                db.Especialidade.Add(especialidade);
                var result = await db.SaveChangesAsync();
                return result > 0;
            }
        }

        public async Task<bool> Edit(Especialidade especialidade)
        {
            using (var db = new Contexto())
            {
                db.Entry(especialidade).State = EntityState.Modified;
                var result = await db.SaveChangesAsync();
                return result > 0;
            }
        }

        public async Task<RespostaDTO> Delete(int id)
        {
            var respostaDTO = new RespostaDTO();
            using (var db = new Contexto())
            {
                var medicos = await _medicoService.GetMedicoByEspecialidadeIdAsync(id);

                if (medicos.Count() > 0)
                {
                    respostaDTO.Sucesso = false;
                    respostaDTO.Mensagem = "Não é possível deletar pois existe ao menos um médico nesta especialidade";
                    return respostaDTO;
                }
                Especialidade especialidade = await db.Especialidade.FindAsync(id);
                db.Especialidade.Remove(especialidade);
                var result = await db.SaveChangesAsync();


                respostaDTO.Sucesso = result > 0;
                respostaDTO.Mensagem = respostaDTO.Sucesso ? "Especialidade removida com sucesso!" : "Não foi possível remover a especialidade, entre em contato com o suporte";

                return respostaDTO;
            }
        }

        public async Task<Especialidade> GetEspecialidadeByIdAsync(int id)
        {
            using (var db = new Contexto())
            {
                Especialidade especialidade = await db.Especialidade.FindAsync(id);
                return especialidade;
            }

        }

      
    }
}