class Pump
{
    public int PetrolAmount { get; private set; }
    public int KmToNextPump { get; private set; }

    public Pump(int petrolAmount, int kmToNextPump)
    {
        PetrolAmount = petrolAmount;
        KmToNextPump = kmToNextPump;
    }
}