using System.Linq;

namespace FestivalManager.Entities
{
	using System.Collections.Generic;
	using Contracts;

	public class Stage : IStage
	{
		// да си вкарват през полетата бе
		private readonly IList<ISet> sets;
		private readonly IList<ISong> songs;
		private readonly IList<IPerformer> performers;

	    public Stage()
	    {
            sets = new List<ISet>();
	        songs = new List<ISong>();
	        performers = new List<IPerformer>();
	    }

	    public Stage(IList<ISet> sets, IList<ISong> songs, IList<IPerformer> performers)
	    {
	        this.sets = sets;
	        this.songs = songs;
	        this.performers = performers;
	    }

	    public IReadOnlyCollection<ISet> Sets => (IReadOnlyCollection<ISet>)sets;

	    public IReadOnlyCollection<ISong> Songs => (IReadOnlyCollection<ISong>) songs;

	    public IReadOnlyCollection<IPerformer> Performers => (IReadOnlyCollection<IPerformer>) performers;

	    public IPerformer GetPerformer(string name)
	    {
	        var performmer = performers.FirstOrDefault(p => p.Name == name);


	        if (performmer == null)
	        {
	            //TODO
	        }

	        return performmer;
	    }

	    public ISong GetSong(string name)
        {
            var song = songs.FirstOrDefault(p => p.Name == name);


            if (song == null)
            {
                //TODO
            }

            return song;
        }

        public ISet GetSet(string name)
	    {
	        var set = sets.FirstOrDefault(p => p.Name == name);

            //if (set == null)
            //{
            //    //TODO
            //}

            return set;
        }

	    public void AddPerformer(IPerformer performer)
	    {
	       performers.Add(performer);
	    }

	    public void AddSong(ISong song)
	    {
	        songs.Add(song);
	    }

	    public void AddSet(ISet set)
	    {
	        sets.Add(set);
	    }

	    public bool HasPerformer(string name)
	    {
	        var performmer = performers.FirstOrDefault(p => p.Name == name);

	        if (performmer == null)
	        {
	            return false;
	        }

	        return true;
	    }

	    public bool HasSong(string name)
	    {
	        var song = songs.FirstOrDefault(p => p.Name == name);


	        if (song == null)
	        {
	            return false;
	        }

	        return true;
        }

	    public bool HasSet(string name)
	    {
	        var set = sets.FirstOrDefault(p => p.Name == name);

	        return set != null;
	    }
	}
}
