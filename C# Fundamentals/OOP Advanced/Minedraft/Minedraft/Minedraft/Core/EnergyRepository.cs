public class EnergyRepository : IEnergyRepository
{
    public double EnergyStored { get; private set; }

    public bool TakeEnergy(double energyNeeded)
    {
        if (EnergyStored >= energyNeeded)
        {
            EnergyStored -= energyNeeded;
            return true;
        }

        return false;
    }

    public void StoreEnergy(double energy)
    {
        EnergyStored += energy;
    }
}