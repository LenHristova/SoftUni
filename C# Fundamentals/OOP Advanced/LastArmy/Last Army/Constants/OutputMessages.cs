public class OutputMessages
{
    //Weapon messages
    public const string NO_WEAPON = "There is no weapon for {0} {1}!";

    //Mission messages
    public const string MISSION_DECLINED = "Mission declined - {0}";

    public const string MISSION_SUCCESSFUL = "Mission completed - {0}";

    public const string MISSION_ON_HOLD = "Mission on hold - {0}";

    //Result message
    public const string END_RESULT = "Results:\r\n" +
                                     "Successful missions - {0}\r\n" +
                                     "Failed missions - {1}\r\n" +
                                     "Soldiers:";

    //Exception messages
    public const string TYPE_NOT_FOUND = "Type: {0} not found.";

    public const string NOT_APPROPRIATE_TYPE = "{0} is not {1}.";

    public const string SOLDIER_TO_STRING = "{0} - {1}";
}