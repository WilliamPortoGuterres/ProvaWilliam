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

namespace ProvaWilliam.Controllers
{
    public class MedicoController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Medico
      
        public async Task<ActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {

            int skipAmount = (pageNumber - 1) * pageSize;


            var totalRecords = await db.Medicos.CountAsync();

            var retorno = await db.Medicos
                .OrderBy(x => x.nome)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new SearchResultViewModel
            {
                Medicos = retorno,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };

            return View(viewModel);
        }




        public async Task<ActionResult> SearchMedicos(FilterMedicos filter)
        {

            int skipAmount = (filter.PageNumber - 1) * filter.PageSize;

            var query = db.Medicos

                .Where(x => (filter.nome == null || x.nome == filter.nome)
                       && (filter.crm == null || x.crm == filter.crm)
                       && (x.id_especialidade.Equals(filter.id_especialidade)));
                      


            var totalRecords = await query.CountAsync();

            var retorno = await query
                .OrderBy(x => x.nome)
                .Skip(skipAmount)
                .Take(filter.PageSize)
                .ToListAsync();


            var viewModel = new SearchResultViewModel
            {
                Medicos = retorno,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalRecords
            };

            return View("Index", viewModel);
        }



        // GET: Medico/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = await db.Medicos.FindAsync(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        // GET: Medico/Create
        public ActionResult Create()
        {
            ViewBag.id_especialidade = new SelectList(db.Especialidades, "id", "descricao");
            return View();
        }

        // POST: Medico/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nome,crm,id_especialidade")] Medicos medicos)
        {
            if (ModelState.IsValid)
            {
                db.Medicos.Add(medicos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_especialidade = new SelectList(db.Especialidades, "id", "descricao", medicos.id_especialidade);
            return View(medicos);
        }

        // GET: Medico/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = await db.Medicos.FindAsync(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_especialidade = new SelectList(db.Especialidades, "id", "descricao", medicos.id_especialidade);
            return View(medicos);
        }

        // POST: Medico/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nome,crm,id_especialidade")] Medicos medicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_especialidade = new SelectList(db.Especialidades, "id", "descricao", medicos.id_especialidade);
            return View(medicos);
        }

        // GET: Medico/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = await db.Medicos.FindAsync(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Medicos medicos = await db.Medicos.FindAsync(id);
            db.Medicos.Remove(medicos);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
