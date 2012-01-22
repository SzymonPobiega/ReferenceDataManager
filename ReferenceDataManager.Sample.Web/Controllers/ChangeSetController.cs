using System.Linq;
using System.Web.Mvc;
using ReferenceDataManager.Sample.Web.Models;

namespace ReferenceDataManager.Sample.Web.Controllers
{
    public class ChangeSetController : Controller
    {
        private readonly IDataStore dataStore;
        private readonly ChangeSetTreeViewModelFactory changeSetTreeViewModelFactory;

        public ChangeSetController(IDataStore dataStore, ChangeSetTreeViewModelFactory changeSetTreeViewModelFactory)
        {
            this.dataStore = dataStore;
            this.changeSetTreeViewModelFactory = changeSetTreeViewModelFactory;
        }

        public ActionResult Index()
        {
            var changeSets = dataStore.LoadAllChangeSets().ToList();
            var model = changeSetTreeViewModelFactory.Create(changeSets);
            return View(model);
        }
    }
}