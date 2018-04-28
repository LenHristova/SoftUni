public interface IWareHouse
{
    void EquipArmy(IArmy army);

    void AddAmmunition(IAmmunition ammunition);

    bool TryEquipSoldier(ISoldier soldier);
}