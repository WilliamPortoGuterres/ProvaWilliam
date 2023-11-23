using ProvaWilliam.Infra;
using ProvaWilliam.Models;
using ProvaWilliam.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaWilliam.Interface
{
   public interface IEspecialidadeService
    {

        Task<List<Especialidade>> GetAllAsync();

        Task<SearchResultViewModel> Listagem(int pageNumber = 1, int pageSize = 10);

        Task<List<EspecialidadeDTO>> SearchEspecialidades();


        Task<bool> Create(Especialidade especialidade);


        Task<bool> Edit(Especialidade especialidade);


        Task<RespostaDTO> Delete(int id);

        Task<Especialidade> GetEspecialidadeByIdAsync(int id);



    }
}
