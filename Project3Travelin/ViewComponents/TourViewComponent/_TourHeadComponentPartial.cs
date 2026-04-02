using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.TourViewComponent
{
    public class _TourHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
