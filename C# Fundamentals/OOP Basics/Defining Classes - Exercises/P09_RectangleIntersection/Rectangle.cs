namespace P09_RectangleIntersection
{
    public class Rectangle
    {
        private string _id;
        private double _width;
        private double _height;
        private Point _topLeftCorner;

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public double Width
        {
            get => _width;
            set => _width = value;
        }

        public double Height
        {
            get => _height;
            set => _height = value;
        }

        public Point TopLeftCorner
        {
            get => _topLeftCorner;
            set => _topLeftCorner = value;
        }

        public Rectangle(string id, double width, double height, Point topLeftCorner)
        {
            _id = id;
            _width = width;
            _height = height;
            _topLeftCorner = topLeftCorner;
        }

        public bool Intersect(Rectangle rectangle)
        {
            return this.TopLeftCorner.X + Width >= rectangle.TopLeftCorner.X
                   && this.TopLeftCorner.X <= rectangle.TopLeftCorner.X + rectangle.Width
                   && this.TopLeftCorner.Y - Height <= rectangle.TopLeftCorner.Y
                   && this.TopLeftCorner.Y >= rectangle.TopLeftCorner.Y - rectangle.Height;
        }
    }
}