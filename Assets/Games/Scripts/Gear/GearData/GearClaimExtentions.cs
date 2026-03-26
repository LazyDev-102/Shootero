using UnityEngine;

public static class GearClaimExtentions {

    public static void Claim(int id, int rank) {
        if (id <= 2404 && id >= 2401) {
            GameResources.Instance.GearInventory.Add(new DroneGearSoftData(id, rank));
        }
        else
            GameResources.Instance.GearInventory.Add(id, rank);
    }
    public static void Claim(int id) {
        if (id <= 2404 && id >= 2401) {
            GameResources.Instance.GearInventory.Add(new DroneGearSoftData(id));
        }
        else
            GameResources.Instance.GearInventory.Add(id);
    }
    
    public static bool IsGear(int id) {
        return id > 2000 && id < 2999;
    }
}
