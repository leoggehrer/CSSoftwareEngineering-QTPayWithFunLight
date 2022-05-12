using Microsoft.AspNetCore.Mvc;
using QTPayWithFunLight.Logic;
using QTPayWithFunLight.Logic.Entities;

namespace QTPayWithFunLight.AspMvc.Controllers
{
    public class PaymentsController : GenericController<Logic.Entities.Payment, Models.Payment>
    {
        public PaymentsController(IDataAccess<Payment> dataAccess) : base(dataAccess)
        {
        }

        protected override Payment[] AfterQuery(Payment[] entities)
        {
            return entities.OrderBy(e => e.Date).ToArray();
        }

        public override async Task<IActionResult> Index()
        {
            var filter = new Models.FilterModel();
            var instanceDataAccess = DataAccess as Logic.Controllers.PaymentsController;
            var accessModels = await instanceDataAccess!.QueryByAsync(filter.CardNumber, filter.Year, filter.Month, filter.Day);
            var volume = await instanceDataAccess!.QueryVolumeByAsync(filter.CardNumber, filter.Year, filter.Month, filter.Day);

            filter.Volume = volume;
            ViewBag.Filter = filter;

            return View(AfterQuery(accessModels).Select(e => ToViewModel(e, ActionMode.Index)));
        }
        public virtual async Task<IActionResult> Filter(Models.FilterModel filter)
        {
            var instanceDataAccess = DataAccess as Logic.Controllers.PaymentsController;
            var accessModels = await instanceDataAccess!.QueryByAsync(filter.CardNumber, filter.Year, filter.Month, filter.Day);
            var volume = await instanceDataAccess!.QueryVolumeByAsync(filter.CardNumber, filter.Year, filter.Month, filter.Day);

            filter.Volume = volume;
            ViewBag.Filter = filter;
            return View("Index", AfterQuery(accessModels).Select(e => ToViewModel(e, ActionMode.Index)));
        }
    }
}
