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
using Newtonsoft.Json;
using ProvaWilliam.Services;
using ProvaWilliam.Interface;

namespace ProvaWilliam.Controllers
{
    public class EspecialidadeController : Controller
    {

        private IEspecialidadeService _especialidadeService;
        public EspecialidadeController(IEspecialidadeService especialidadeService)
        {
            _especialidadeService = especialidadeService;
        }


        public async Task<ActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.respostaDelete = TempData["respostaDelete"];
            var retorno = await _especialidadeService.Listagem(pageNumber ,  pageSize );
            return View(retorno);
        }


        public async Task<JsonResult> SerchEspecialidades()
        {

            var result = await _especialidadeService.SearchEspecialidades();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = await _especialidadeService.GetEspecialidadeByIdAsync(id.Value);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,descricao")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                var retorno = await _especialidadeService.Create(especialidade);

                return RedirectToAction("Index");
            }

            return View(especialidade);
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = await _especialidadeService.GetEspecialidadeByIdAsync(id.Value);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,descricao")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                var Retorno = await _especialidadeService.Edit(especialidade);

                return RedirectToAction("Index");
            }
            return View(especialidade);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = await _especialidadeService.GetEspecialidadeByIdAsync(id.Value);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var resposta = await _especialidadeService.Delete(id);

            TempData["respostaDelete"] =resposta;
            return RedirectToAction("Index");
        }


    }
}
