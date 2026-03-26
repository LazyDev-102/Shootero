
using UnityEngine;
using Gear_Data;

[CreateAssetMenu(fileName = "DroneGearHardData", menuName = "Resource/Gears/DroneGearHardData")]
public class DroneGearHardData : GearHardData {
    [SerializeField] private DroneBase dronePrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float rebornCooldown;
    [SerializeField] private RankStatData secondStats;

    public DroneBase DronePrefab { get => dronePrefab; }
    public float RebornCooldown { get => rebornCooldown; }
    public RankStatData SecondStats { get => secondStats; }

    public int GetDamage(int levelIndex) {
        foreach (var stat in PrimaryStatDatas) {
            if (stat.StatData.StatEvent == EventKey.StatEvent.DroneAttack) {
                levelIndex = levelIndex >= stat.Values.Length ? stat.Values.Length - 1 : levelIndex;
                return (int)stat.Values[levelIndex].Value;
            }
        }
        return 0;
    }
    public int GetHP(int levelIndex) {
        foreach (var stat in PrimaryStatDatas) {
            if (stat.StatData.StatEvent == EventKey.StatEvent.DroneHp) {
                levelIndex = levelIndex >= stat.Values.Length ? stat.Values.Length - 1 : levelIndex;
                return (int)stat.Values[levelIndex].Value;
            }
        }
        return 100;
    }
    public float GetFireRate(int levelIndex) {
        return fireRate;
    }
    public float GetCooldown() {
        return rebornCooldown;
    }
    public override void Claim(int amount) {
        GearSoftData newGear = new GearSoftData(Id, 0);
        GameResources.Instance.GearInventory.Add(newGear);
        GameResources.Instance.RateUs.SetClaimEpicItemStatus(true);
    }
}
