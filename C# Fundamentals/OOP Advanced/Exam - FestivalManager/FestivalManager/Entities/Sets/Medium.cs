namespace FestivalManager.Entities.Sets
{
	using System;

	public class Medium : Set
	{
	   private const int Minutes = 40;

        public Medium(string name)
			: base(name, new TimeSpan(0, Minutes, 0))
		{
		}
	}
}