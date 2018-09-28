namespace CameraBazar.Services.Models.Camera
{
    using Data.Models.Enums;
    using User;

    public class CameraModel
    {
        public CameraMake Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int MinShutterSpeed { get; set; }

        public int MaxShutterSpeed { get; set; }

        public MinIso MinIso { get; set; }

        public int MaxIso { get; set; }

        public bool IsFullFrame { get; set; }

        public string VideoResolution { get; set; }

        public LightMetering LightMetering { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public UserModel User { get; set; }
    }
}
