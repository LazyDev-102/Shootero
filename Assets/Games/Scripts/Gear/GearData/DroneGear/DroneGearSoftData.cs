

using Gear_Data;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DroneGearSoftData : GearSoftData {
    public DroneGearSoftData(int id) : base(id) { }
    public DroneGearSoftData(int id, int rank) : base(id, rank) { }

    public int Damage { get => GetDamage(); }
    public int HP { get => GetHP(); }
    public float FireRate { get => GetFireRate(); }

    private int GetDamage() {
        foreach (var stat in GearHardData.PrimaryStatDatas) {
            if (stat.StatData.StatEvent == EventKey.StatEvent.DroneAttack) {
                return (int)stat.Values[CurrentLevel].Value;
            }
        }
        return 0;
    }
    private int GetHP() {
        foreach (var stat in GearHardData.PrimaryStatDatas) {
            if (stat.StatData.StatEvent == EventKey.StatEvent.DroneHp) {
                return (int)stat.Values[CurrentLevel].Value;
            }
        }
        return 100;
    }
    private float GetFireRate() {
        foreach (var stat in GearHardData.PrimaryStatDatas) {
            if (stat.StatData.StatEvent == EventKey.StatEvent.DroneFirerate) {
                return (int)stat.Values[CurrentLevel].Value;
            }
        }
        return 1;
    }

}
