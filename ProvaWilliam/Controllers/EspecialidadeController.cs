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

namespace ProvaWilliam.Controllers
{
    public class EspecialidadeController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Especialidade
        public async Task<ActionResult> Index()
        {
            return View(await db.Especialidades.ToListAsync());
        }


         public async Task<JsonResult> SerchEspecialidades()
        {

            var result = await db.Especialidades
                          .Select(e => new { e.id, e.descricao })
                          .ToListAsync();


            

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        // GET: Especialidade/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = await db.Especialidades.FindAsync(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // GET: Especialidade/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Especialidade/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,descricao")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                db.Especialidades.Add(especialidade);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        // GET: Especialidade/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = await db.Especialidades.FindAsync(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidade/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,descricao")] Especialidade especialidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidade).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(especialidade);
        }

        // GET: Especialidade/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = await db.Especialidades.FindAsync(id);
            if (especialidade == null)
            {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Especialidade especialidade = await db.Especialidades.FindAsync(id);
            db.Especialidades.Remove(especialidade);
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
