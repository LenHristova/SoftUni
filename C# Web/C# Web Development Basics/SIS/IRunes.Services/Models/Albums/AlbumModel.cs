namespace IRunes.Services.Models.Albums
{
    using System.Collections.Generic;
    using Tracks;

    public class AlbumModel
    {
        public string Name { get; set; }

        public string CoverImageUrl { get; set; }

        public decimal Price { get; set; }

        public virtual IEnumerable<TrackListingModel> Tracks { get; set; }
    }
}
