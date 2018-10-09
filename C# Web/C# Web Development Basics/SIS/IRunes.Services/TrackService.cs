namespace IRunes.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using System;
    using System.Linq;
    using Models.Tracks;

    public class TrackService : ITrackService
    {
        public string Create(string name, string videoUrl, decimal price, string albumId)
        {
            using (var db = new IRunesDbContext())
            {
                var track = new Track
                {
                    Name = name,
                    VideoUrl = videoUrl,
                    Price = price
                };

                var trackAlbum = new TrackAlbum()
                {
                    Track = track,
                    AlbumId = albumId
                };

                db.TracksAlbums.Add(trackAlbum);

                try
                {
                    db.SaveChanges();
                    return track.Id;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

                return null;
            }
        }

        public TrackModel ById(string id)
        {
            using (var db = new IRunesDbContext())
            {
                return db.Tracks
                    .Where(t => t.Id == id)
                    .Select(t => new TrackModel
                    {
                        Name = t.Name,
                        VideoUrl = t.VideoUrl,
                        Price = t.Price
                    })
                    .FirstOrDefault();
            }
        }
    }
}
