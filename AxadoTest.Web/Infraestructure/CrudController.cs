using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using AxadoTest.Data.Infrastructure;

namespace AxadoTest.Api.Infrastructure
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public partial class CrudController<T> : Controller
        where T : class
    {
        public IService<T> Service;

        public T EntityViewModel;

        public CrudController(IService<T> service)
        {
            Service = service;
        }
        
        // GET: ControllerName
        public virtual ActionResult Index()
        {
            return View(Service.GetAll());
        }

        // GET: ControllerName/Details/5
        public virtual ActionResult Details(long? id, string redirectUrl)
        {
            ViewBag.RedirectUrl = redirectUrl;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntityViewModel = Service.GetById((long)id);

            if (EntityViewModel == null)
            {
                return HttpNotFound();
            }

            return View(EntityViewModel);
        }

        // GET: ControllerName/Create
        public virtual ActionResult Create(string redirectUrl)
        {
            ViewBag.RedirectUrl = redirectUrl;
            return View();
        }

        // POST: ControllerName/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(T viewmodel, string redirectUrl)
        {
            try
            {
                ViewBag.RedirectUrl = redirectUrl;

                EntityViewModel = viewmodel;

                if (ModelState.IsValid)
                {
                    Service.Add(EntityViewModel);
                    if (!string.IsNullOrEmpty(redirectUrl))
                    {
                        return Redirect(redirectUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", new { c = true });
                    }
                }
            }
            catch (Exception e)
            {
                ErrorHelper.SetModelExceptionError(ModelState, e);
            }

            return View(EntityViewModel);
        }

        // GET: ControllerName/Edit/5
        public virtual ActionResult Edit(long? id, string redirectUrl)
        {
            ViewBag.RedirectUrl = redirectUrl;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntityViewModel = Service.GetById((long)id);

            if (EntityViewModel == null)
            {
                return HttpNotFound();
            }

            return View(EntityViewModel);
        }

        // POST: ControllerName/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(T viewmodel, string redirectUrl)
        {
            try
            {
                ViewBag.RedirectUrl = redirectUrl;

                EntityViewModel = viewmodel;

                if (ModelState.IsValid)
                {
                    Service.Update(EntityViewModel);
                    if (!string.IsNullOrEmpty(redirectUrl))
                    {
                        return Redirect(redirectUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", new { e = true });
                    }
                }
            }
            catch (Exception e)
            {
                ErrorHelper.SetModelExceptionError(ModelState, e);
            }

            return View(EntityViewModel);
        }

        // GET: ControllerName/Delete/5
        public virtual ActionResult Delete(long? id, string redirectUrl)
        {
            ViewBag.RedirectUrl = redirectUrl;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntityViewModel = Service.GetById((long)id);

            if (EntityViewModel == null)
            {
                return HttpNotFound();
            }

            return View(EntityViewModel);
        }

        // POST: ControllerName/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id, string redirectUrl)
        {
            try
            {
                ViewBag.RedirectUrl = redirectUrl;
                EntityViewModel = Service.GetById(id);
                Service.Delete(id);

                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    return Redirect(redirectUrl);
                }
                else
                {
                    return RedirectToAction("Index", new { d = true });
                }
            }
            catch (Exception e)
            {
                ErrorHelper.SetModelExceptionError(ModelState, e);
            }

            return View(EntityViewModel);
        }

        public virtual ActionResult Search(string text)
        {
            ViewBag.SearchTerm = text;
            return View("Index", Service.Search(text));
        }

        public virtual ActionResult JsonSearch(string text)
        {
            string json = JsonConvert.SerializeObject(Service.Search(text));
            return Content(json, "application/json");
        }
    }
}
