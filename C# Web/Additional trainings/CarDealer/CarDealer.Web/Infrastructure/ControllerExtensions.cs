namespace CarDealer.Web.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public static class ControllerExtensions
    {
        public static IActionResult ViewOrNotFoundView(this Controller controller, object model)
            => model == null
                ? controller.NotFoundView()
                : controller.View(model: model);

        public static IActionResult NotFoundView(this Controller controller)
            => controller.View("Error", new ErrorModel(Constants.NotFoundErrorMessage));
    }
}
