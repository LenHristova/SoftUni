namespace IRunes.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Models.Albums;
    using Models.Tracks;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AlbumService : IAlbumService
    {
        public string Create(string name, string coverImageUrl)
        {
            using (var db = new IRunesDbContext())
            {
                var album = new Album
                {
                    Name = name,
                    CoverImageUrl = coverImageUrl
                };

                db.Albums.Add(album);

                try
                {                   
                    db.SaveChanges();
                    return album.Id;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

                return null;
            }
        }

        public IEnumerable<AlbumListingModel> All()
        {
            using (var db = new IRunesDbContext())
            {
                return db.Albums
                    .Select(a => new AlbumListingModel
                    {
                        Id = a.Id,
                        Name = a.Name
                    })
                    .ToList();
            }
        }

        public AlbumModel ById(string id)
        {
            using (var db = new IRunesDbContext())
            {
                return db.Albums
                    .Where(a => a.Id == id)
                    .Select(a => new AlbumModel
                    {
                        Name = a.Name,
                        CoverImageUrl = a.CoverImageUrl,
                        Price = a.Tracks.Any() ? a.Tracks.Sum(t => t.Track.Price) * 0.87m : 0,
                        Tracks = a.Tracks
                            .Select(t => new TrackListingModel
                            {
                                Id = t.TrackId,
                                Name = t.Track.Name
                            })
                            .ToList()
                    })
                    .FirstOrDefault();
            }
        }

        public bool Exists(string id)
        {
            using (var db = new IRunesDbContext())
            {
                return db.Albums
                    .Any(a => a.Id == id);
            }
        }
    }
}
