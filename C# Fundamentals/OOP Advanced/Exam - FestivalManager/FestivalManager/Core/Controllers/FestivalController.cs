using FestivalManager.Entities.Factories.Contracts;

namespace FestivalManager.Core.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Entities.Contracts;

    public class FestivalController : IFestivalController
    {
        //private const string TimeFormat = "hh\\:mm\\:ss";
        private const string TimeFormat = "mm\\:ss";
        //private const string TimeFormatLong = "{0:2D}:{1:2D}";
        //private const string TimeFormatThreeDimensional = "{0:3D}:{1:3D}";

        private readonly IStage stage;
        private readonly ISetFactory setFactory;
        private readonly IPerformerFactory performerFactory;
        private readonly IInstrumentFactory instrumentFactory;
        private readonly ISongFactory songFactory;

        public FestivalController(IStage stage, ISetFactory setFactory, IPerformerFactory performerFactory, IInstrumentFactory instrumentFactory, ISongFactory songFactory)
        {
            this.stage = stage;
            this.setFactory = setFactory;
            this.performerFactory = performerFactory;
            this.instrumentFactory = instrumentFactory;
            this.songFactory = songFactory;
        }

        public string ProduceReport()
        {
            var result = string.Empty;

            var totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));

            var min = totalFestivalLength.TotalMinutes;
            var sec = totalFestivalLength.Seconds;
            result += ($"Festival length: {min:00}:{sec:00}") + "\n";
            //result += ($"Festival length: {totalFestivalLength:mm\\:ss}") + "\n";

            foreach (var set in this.stage.Sets)
            {
                // result += ($"--{set.Name} ({string.Format(TimeFormat, set.ActualDuration)}):") + "\n";
                var min2 = set.ActualDuration.TotalMinutes;
                var sec2 = set.ActualDuration.Seconds;
                result += ($"--{set.Name} ({min2:00}:{sec2:00}):") + "\n";

                var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
                foreach (var performer in performersOrderedDescendingByAge)
                {
                    var instruments = string.Join(", ", performer.Instruments
                        .OrderByDescending(i => i.Wear));

                    result += ($"---{performer.Name} ({instruments})") + "\n";
                }

                if (!set.Songs.Any())
                    result += ("--No songs played") + "\n";
                else
                {
                    result += ("--Songs played:") + "\n";
                    foreach (var song in set.Songs)
                    {
                        var min3 = song.Duration.TotalMinutes;
                        var sec3 = song.Duration.Seconds;
                        result += ($"----{song.Name} ({min3:00}:{sec3:00})") + "\n";
                    }
                }
            }

            return result.ToString();
        }

        public string RegisterSet(string[] args)
        {
            var name = args[0];
            var type = args[1];
            var set = setFactory.CreateSet(name, type);
            stage.AddSet(set);

            return $"Registered {type} set";
        }

        public string SignUpPerformer(string[] args)
        {
            var name = args[0];
            var age = int.Parse(args[1]);
            var performer = performerFactory.CreatePerformer(name, age);

            var instruments = args.Skip(2).ToList();
            foreach (var instrumentName in instruments)
            {
                var instrument = instrumentFactory.CreateInstrument(instrumentName);
                performer.AddInstrument(instrument);
            }

            stage.AddPerformer(performer);

            return $"Registered performer {name}";
        }

        public string RegisterSong(string[] args)
        {
            var name = args[0];
            var duration = args[1].Split(':');
            var min = int.Parse(duration[0]);
            var sec = int.Parse(duration[1]);

            TimeSpan time = new TimeSpan(0, min, sec);
            var song = songFactory.CreateSong(name, time);

            stage.AddSong(song);

            return $"Registered song {name} ({time:mm\\:ss})";
        }

        public string AddSongToSet(string[] args)
        {
            var songName = args[0];
            var setName = args[1];

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            if (!this.stage.HasSong(songName))
            {
                throw new InvalidOperationException("Invalid song provided");
            }

            var set = this.stage.GetSet(setName);
            var song = this.stage.GetSong(songName);

            set.AddSong(song);

            return $"Added {songName} ({song.Duration:mm\\:ss}) to {setName}";
        }

        public string AddPerformerToSet(string[] args)
        {
            var performerName = args[0];
            var setName = args[1];

            if (!this.stage.HasPerformer(performerName))
            {
                throw new InvalidOperationException("Invalid performer provided");
            }

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            var performer = this.stage.GetPerformer(performerName);
            var set = this.stage.GetSet(setName);

            set.AddPerformer(performer);

            return $"Added {performerName} to {setName}";
        }

        public string RepairInstruments(string[] args)
        {
            var instrumentsToRepair = this.stage.Performers
                .SelectMany(p => p.Instruments)
                .Where(i => i.Wear < 100)
                .ToArray();

            foreach (var instrument in instrumentsToRepair)
            {
                instrument.Repair();
            }

            return $"Repaired {instrumentsToRepair.Length} instruments";
        }


        //public string Report()
        //{
        //	var result = string.Empty;

        //	var totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));

        //	result += ($"Festival length: {FormatTime(totalFestivalLength)}") + "\n";

        //	foreach (var set in this.stage.Sets)
        //	{
        //		result += ($"--{set.Name} ({FormatTime(set.ActualDuration)}):") + "\n";

        //		var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
        //		foreach (var performer in performersOrderedDescendingByAge)
        //		{
        //			var instruments = string.Join(", ", performer.Instruments
        //				.OrderByDescending(i => i.Wear));

        //			result += ($"---{performer.Name} ({instruments})") + "\n";
        //		}

        //		if (!set.Songs.Any())
        //			result += ("--No songs played") + "\n";
        //		else
        //		{
        //			result += ("--Songs played:") + "\n";
        //			foreach (var song in set.Songs)
        //			{
        //				result += ($"----{song.Name} ({song.Duration.ToString(TimeFormat)})") + "\n";
        //			}
        //		}
        //	}

        //	return result.ToString();
        //}



        //// Временно!!! Чтобы работало делаем срез на конец месяца
        //public string AddPerformerToSet(string[] args)
        //{
        //	return PerformerRegistration(args);
        //}

        //public string SignUpPerformer(string[] args)
        //{
        //    var performerName = args[0];
        //    var setName = args[1];

        //    if (!this.stage.HasPerformer(performerName))
        //    {
        //        throw new InvalidOperationException("Invalid performer provided");
        //    }

        //    if (!this.stage.HasSet(setName))
        //    {
        //        throw new InvalidOperationException("Invalid set provided");
        //    }

        //    AddPerformerToSet(args);

        //    var performer = this.stage.GetPerformer(performerName);
        //    var set = this.stage.GetSet(setName);

        //    set.AddPerformer(performer);

        //    return $"Added {performer.Name} to {set.Name}";
        //}
    }
}