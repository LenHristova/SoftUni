namespace CameraBazar.Services.Contracts
{
    using System.Collections.Generic;
    using Data.Models.Enums;
    using Models.Camera;

    public interface ICameraService
    {
        void Add(
            CameraMake make, 
            string model,
            decimal price, 
            int quantity, 
            int minShutterSpeed, 
            int maxShutterSpeed, 
            MinIso minIso, 
            int maxIso, 
            bool isFullFrame, 
            string videoResolution, 
            IEnumerable<LightMetering> lightMetering, 
            string description, 
            string imageUrl,
            string userId);

        IEnumerable<CameraListingModel> All();

        IEnumerable<CameraListingModel> ByUserId(string userId);

        CameraModel ById(int id);
    }
}
