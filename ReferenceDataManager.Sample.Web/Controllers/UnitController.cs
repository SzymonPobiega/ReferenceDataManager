using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ReferenceDataManager.Sample.Web.Controllers
{
    public class UnitController : Controller
    {
        private readonly IObjectFacade facade;

        public UnitController(IObjectFacade facade)
        {
            this.facade = facade;
        }

        public ActionResult Index(string changeSetId)
        {
            var snapshot = facade.GetSnapshot(ChangeSetId.Parse(changeSetId));
            var units = snapshot.List<Unit>();
            return View(units);
        }
    }
}