using System;

namespace p19_TheaThePhotographer
{
    class TheaThePhotographer
    {
        static void Main(string[] args)
        {
            int picturesNumber = int.Parse(Console.ReadLine());
            int filterTime = int.Parse(Console.ReadLine());
            byte filterFactor = byte.Parse(Console.ReadLine());
            int uploadTime = int.Parse(Console.ReadLine());

            long timeForFilterAllPictures = (long)picturesNumber * filterTime;
            int goodPicturesNumber = (int)Math.Ceiling(picturesNumber * filterFactor / 100.0);
            long timeForUploadGoodPictures = (long)goodPicturesNumber * uploadTime;
            long neededTimeInSeconds = timeForFilterAllPictures + timeForUploadGoodPictures;
            TimeSpan neededTime = TimeSpan.FromSeconds(neededTimeInSeconds);
            Console.WriteLine(neededTime.ToString(@"d\:hh\:mm\:ss"));
        }
    }
}
