namespace P03_OldestFamilyMember
{
    using System;

    class StartUp
    {
        static void Main()
        {
            var family = new Family();

            var membersCount = int.Parse(Console.ReadLine());
            for (int member = 0; member < membersCount; member++)
            {
                var memberInfo = Console.ReadLine()?.Split();
                if (memberInfo != null)
                {
                    var memberName = memberInfo[0];
                    var memberAge = int.Parse(memberInfo[1]);
                    family.AddMember(new Person(memberName, memberAge));
                }
            }

            try
            {
                Console.WriteLine(family.GetOldestMember());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}