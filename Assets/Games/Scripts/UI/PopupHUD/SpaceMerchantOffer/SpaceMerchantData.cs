using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceMerchantData", menuName = "Resource/HardData/Offer/SpaceMerchantData")]
public class SpaceMerchantData : ScriptableObject {
    [System.NonSerialized] public int MaxItem = 3;
    [SerializeField] private int[] rateTypes;
    [SerializeField] private SpaceMerchantPack[] spaceMerchantPacks;

    public ItemStack GetPrice(GearType gearType, Rarety rank) {
        for (int i = 0; i < spaceMerchantPacks.Length; i++) {
            if (spaceMerchantPacks[i].GearType == gearType) {
                return spaceMerchantPacks[i].GetPrice(rank);
            }
        }
        return null;
    }
    public int GetRank() {
        if (rateTypes == null || rateTypes.Length == 0)
            return 0;
        return RandomHelper.RandomWithPercent(rateTypes);
    }
}
[System.Serializable]
public class SpaceMerchantPack {
    [SerializeField] private GearType gearType;
    [SerializeField] private SpaceMerchantPackInfo[] packInfoes;

    public GearType GearType { get => gearType; }

    public ItemStack GetPrice(Rarety rank) {
        for (int i = 0; i < packInfoes.Length; i++) {
            if (packInfoes[i].Rank == rank) {
                return packInfoes[i].Price;
            }
        }
        return null;
    }
}

[System.Serializable]
public class SpaceMerchantPackInfo {
    [SerializeField] private Rarety rank;
    [SerializeField] private ItemStack price;

    public Rarety Rank { get => rank; }
    public ItemStack Price { get => price; }
}