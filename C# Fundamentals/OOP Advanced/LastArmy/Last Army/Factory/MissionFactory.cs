using System;
using System.Linq;
using System.Reflection;

public class MissionFactory : IMissionFactory
{
    public IMission CreateMission(string difficultyLevel, double neededPoints)
    {
        var missionType = Assembly
            .GetCallingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name == difficultyLevel);

        if (missionType == null)
        {
            throw new ArgumentException(string.Format(OutputMessages.TYPE_NOT_FOUND, nameof(Mission)));
        }

        if (!typeof(IMission).IsAssignableFrom(missionType))
        {
            throw new ArgumentException(string.Format(OutputMessages.NOT_APPROPRIATE_TYPE, difficultyLevel, nameof(IMission)));
        }

        var mission = (IMission) Activator.CreateInstance(missionType, new object[] {neededPoints});

        return mission;
    }
}