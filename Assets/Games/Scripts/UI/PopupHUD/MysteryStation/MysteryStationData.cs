using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "MysteryStationData", menuName = "Resource/HardData/Offer/MysteryStationData")]
public class MysteryStationData : ScriptableObject {
    [SerializeField] private ModData[] mods;
    //[SerializeField] private ModData oneTimeMod;
    [SerializeField] private int maxTrade;
    [SerializeField] private StatModifier percentMaxHP;
    private int cTrade;
    //private bool oneTimeModLoadable;
    public void Reset() {
        cTrade = maxTrade;
        //oneTimeModLoadable = true;
    }

    public ModData GetMod() {
        return RandomHelper.RandomInCollection(mods);
        //ModData result = null;
        //int loopTimes = 0;
        //do {
        //    result = RandomHelper.RandomInCollection(mods);
        //    loopTimes++;
        //    if (loopTimes > 5)
        //        break;
        //} while (!oneTimeModLoadable && result == oneTimeMod);
        //if (result == oneTimeMod)
        //    oneTimeModLoadable = false;
        //return result;
    }
    public int GetHpTrade(ShipBase ship) {
        int result = (int)(ship.ShipStat.MaxHP.Value * percentMaxHP.Value);
        if (result < 0)
            result *= -1;
        return result;
    }
    public bool Tradeable() {
        return cTrade > 0;
    }
    public void Trade(ShipBase ship) {
        if (ship == null)
            return;
        if (percentMaxHP == null)
            return;
        var maxhp = ship.ShipStat.MaxHP.Value;
        ship.ShipStat.MaxHP.Reset();
        ship.ShipStat.MaxHP.SetBaseValue(maxhp + (int)(percentMaxHP.Value * maxhp));
        ship.ShipHealth.ResetHpAttachMaxHp();
    }
}
