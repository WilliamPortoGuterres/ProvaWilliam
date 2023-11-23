using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProvaWilliam.Infra;
using ProvaWilliam.Models;
using ProvaWilliam.Models.Filter;
using ProvaWilliam.Interface;

namespace ProvaWilliam.Controllers
{
    public class MedicoController : Controller
    {
        private IMedicoService _medicoService;
        private IEspecialidadeService _especialidadeService;
        public MedicoController() { }
        public MedicoController(IMedicoService medicoService, IEspecialidadeService especialidadeService)
        {
            _medicoService = medicoService;
            _especialidadeService = especialidadeService;

        }

        public async Task<ActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.respostaDelete = TempData["respostaDelete"];
            ViewBag.respostaMSGSemEspecialidade = TempData["msg_sem_especialidade"];

            var viewModel = await _medicoService.Listagem(pageNumber, pageSize);
            return View(viewModel);
        }




        public async Task<ActionResult> SearchMedicos(FilterMedicos filter)
        {
            var viewModel = await _medicoService.GetMedicoByFilterAsync(filter);
            return View("Index", viewModel);
        }




        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = await _medicoService.GetMedicoByIdAsync(id.Value);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }


        public async Task<ActionResult> Create()
        {
            var validaEspecialidade =await _especialidadeService.GetAllAsync();
            if (validaEspecialidade.Count() <= 0)
            {

                TempData["msg_sem_especialidade"] = "Cadastre pelo menos uma especialidade antes de cadastrar os medicos!";
                return RedirectToAction("Index");
            }

            var especialidade = await _especialidadeService.GetAllAsync();
            ViewBag.id_especialidade = new SelectList(especialidade, "id", "descricao");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nome,crm,id_especialidade")] Medicos medicos)
        {

            

            if (ModelState.IsValid)
            {
                var retorno =await _medicoService.Create(medicos);
                return RedirectToAction("Index");
            }
            var especialidades =await _especialidadeService.GetAllAsync();
            ViewBag.id_especialidade = new SelectList(especialidades, "id", "descricao", medicos.id_especialidade);
            return View(medicos);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = await _medicoService.GetMedicoByIdAsync(id.Value);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            var especialidades = await _especialidadeService.GetAllAsync();
            ViewBag.id_especialidade = new SelectList(especialidades, "id", "descricao", medicos.id_especialidade);
            return View(medicos);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nome,crm,id_especialidade")] Medicos medicos)
        {
            if (ModelState.IsValid)
            {
                var retorno =await _medicoService.Edit(medicos);  
                return RedirectToAction("Index");
            }
            var especialidades = await _especialidadeService.GetAllAsync();
            ViewBag.id_especialidade = new SelectList(especialidades, "id", "descricao", medicos.id_especialidade);
            return View(medicos);
        }

      
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = await _medicoService.GetMedicoByIdAsync(id.Value);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var resposta = await _medicoService.Delete(id);
            TempData["respostaDelete"] = resposta;
            return RedirectToAction("Index");
        }

        
    }
}
