// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to your project (entities/controllers/etc)


using System;

using FestivalManager.Core.Controllers;
using FestivalManager.Entities;
using FestivalManager.Entities.Instruments;
using FestivalManager.Entities.Sets;


namespace FestivalManager.Tests
{
	using NUnit.Framework;

	[TestFixture]
	public class SetControllerTests
    {
		[Test]
	    public void Test()
		{
		    var stage = new Stage();
            var set = new Medium("Set");
            stage.AddSet(set);

		    var setController = new SetController(stage);

		   var result =  setController.PerformSets();
           Assert.IsTrue(result.Contains("Did not perform"));
		}

        [Test]
        public void Test2()
        {
            var stage = new Stage();
            var set = new Medium("Set");
            var performer = new Performer("Pesho", 15);
            performer.AddInstrument(new Guitar());

            set.AddPerformer(performer);
            set.AddSong(new Song("sonf", new TimeSpan(0,5,0)));

            stage.AddSet(set);

            var setController = new SetController(stage);

            var result = setController.PerformSets();
             Assert.IsTrue(result.Contains("-- Set Successful"));
        }

        [Test]
        public void Test3()
        {
            var stage = new Stage();

            var set = new Medium("Set");
            var performer = new Performer("Pesho", 15);
            performer.AddInstrument(new Guitar());

            set.AddPerformer(performer);
           set.AddSong(new Song("sonf", new TimeSpan(0, 5, 0)));

            var performer2 = new Performer("Gosho", 12);
            performer2.AddInstrument(new Drums());

            set.AddPerformer(performer2);
            stage.AddSet(set);

            var set2 = new Long("long");
            stage.AddSet(set2);

            var setController = new SetController(stage);
 
            var result = setController.PerformSets();

           Assert.That(result, Is.EqualTo("1. Set:\r\n-- 1. sonf (05:00)\r\n-- Set Successful\r\n2. long:\r\n-- Did not perform"));
        }

        [Test]
        public void Test4()
        {
            var stage = new Stage();

            var set = new Medium("Set");
            var performer = new Performer("Pesho", 15);
            performer.AddInstrument(new Guitar());

            set.AddPerformer(performer);
            set.AddSong(new Song("sonf", new TimeSpan(0, 5, 0)));

            var set2 = new Medium("Set2");
            var performer2 = new Performer("Gosho", 12);
            performer2.AddInstrument(new Drums());

            set2.AddPerformer(performer2);
            set2.AddSong(new Song("sonf", new TimeSpan(0, 5, 0)));

            stage.AddSet(set);
            stage.AddSet(set2);

            var setController = new SetController(stage);

            var result = setController.PerformSets();

            Assert.That(result, Is.EqualTo("1. Set:\r\n-- 1. sonf (05:00)\r\n-- Set Successful\r\n2. Set2:\r\n-- 1. sonf (05:00)\r\n-- Set Successful"));
        }

        [Test]
        public void Test9()
        {
            var stage = new Stage();

            var set = new Medium("Set");
            var performer = new Performer("Pesho", 15);
            var instrument = new Guitar();
            performer.AddInstrument(instrument);

            set.AddPerformer(performer);
           set.AddSong(new Song("sonf", new TimeSpan(0, 5, 0)));


            stage.AddSet(set);


            var setController = new SetController(stage);
            setController.PerformSets();
            var result = instrument.Wear;

            Assert.That(result, Is.EqualTo(40));
        }

        [Test]
        public void Test5()
        {
            var stage = new Stage();

            var set = new Medium("Set");
            var performer = new Performer("Pesho", 15);
            performer.AddInstrument(new Guitar());

            set.AddPerformer(performer);

            var performer2 = new Performer("Gosho", 12);
            performer2.AddInstrument(new Drums());

            set.AddPerformer(performer2);
            stage.AddSet(set);

            var set2 = new Long("long");
            stage.AddSet(set2);

            var setController = new SetController(stage);

            var result = setController.PerformSets();

            Assert.That(result, Is.EqualTo("1. Set:\r\n-- Did not perform\r\n2. long:\r\n-- Did not perform"));
        }
    }
}