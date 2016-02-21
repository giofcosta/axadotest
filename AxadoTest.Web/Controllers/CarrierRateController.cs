using AxadoTest.Api.Infrastructure;
using AxadoTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AxadoTest.Data.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AxadoTest.Data.Services;
using System.Net;

namespace AxadoTest.Web.Controllers
{
    [Authorize]
    public class CarrierRateController : CrudController<CarrierRateViewModel>
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ICarrierService CarrierService;

        public CarrierRateController(IService<CarrierRateViewModel> service) : base(service)
        {
            CarrierService = DependencyResolver.Current.GetService<ICarrierService>();
        }

        public override ActionResult Index()
        {
            var user = UserManager.FindByName(User.Identity.Name);
            ViewBag.AnyToRate = CarrierService.GetAllCanRate(user.Id).Any();
            return View(((ICarrierRateService)Service).GetAllForUser(user.Id));
        }

        public override ActionResult Create(string redirectUrl)
        {
            var user = UserManager.FindByName(User.Identity.Name);
            ViewBag.CarrierId = new SelectList(CarrierService.GetAllCanRate(user.Id), "Id", "Name");
            return base.Create(redirectUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(CarrierRateViewModel viewmodel, string redirectUrl)
        {
            try
            {
                ViewBag.RedirectUrl = redirectUrl;

                EntityViewModel = viewmodel;

                if (ModelState.IsValid)
                {
                    var user = UserManager.FindByName(User.Identity.Name);
                    EntityViewModel.UserId = user.Id;
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


        public override ActionResult Edit(long? id, string redirectUrl)
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

            ViewBag.CarrierId = new SelectList(CarrierService.GetAll(), "Id", "Name", EntityViewModel.CarrierId);

            return View(EntityViewModel);
        }

        public override ActionResult Edit(CarrierRateViewModel viewmodel, string redirectUrl)
        {
            ViewBag.CarrierId = new SelectList(CarrierService.GetAll(), "Id", "Name", viewmodel.CarrierId);
            var user = UserManager.FindByName(User.Identity.Name);
            viewmodel.UserId = user.Id;
            return base.Edit(viewmodel, redirectUrl);
        }
    }
}