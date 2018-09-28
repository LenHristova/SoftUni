namespace CameraBazar.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Camera;
    using Services.Contracts;

    public class CamerasController : Controller
    {
        public readonly UserManager<User> userManager;
        public readonly ICameraService cameras;

        public CamerasController(
            UserManager<User> userManager,
            ICameraService cameras)
        {
            this.cameras = cameras;
            this.userManager = userManager;
        }

        public IActionResult All() => View(this.cameras.All());

        [Authorize]
        public IActionResult Add() => View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCameraViewModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraModel);
            }

            this.cameras.Add(
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinIso,
                cameraModel.MaxIso,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMetering,
                cameraModel.Description,
                cameraModel.ImageUrl,
                this.userManager.GetUserId(User));

            return Redirect("/");
            //return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var camera = this.cameras.ById(id);
            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }
    }
}
