using ProvaWilliam.Infra;
using ProvaWilliam.Models;
using ProvaWilliam.Models.DTOs;
using ProvaWilliam.Models.Filter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaWilliam.Interface
{
   public interface IMedicoService
    {

        Task<List<Medicos>> Search();


        Task<bool> Create(Medicos Medico);


       Task<bool> Edit(Medicos Medico);


        Task<RespostaDTO> Delete(int id);

        Task<Medicos> GetMedicoByIdAsync(int id);

        Task<SearchResultViewModel> Listagem(int pageNumber = 1, int pageSize = 10);

        Task<SearchResultViewModel> GetMedicoByFilterAsync(FilterMedicos filter);

        Task<List<Medicos>> GetMedicoByEspecialidadeIdAsync(int id);


    }
}
