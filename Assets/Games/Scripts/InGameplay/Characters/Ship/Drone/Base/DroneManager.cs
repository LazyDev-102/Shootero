using Gear_Data;
using Gemmob;
using UnityEngine;

public class DroneManager : SingletonBind<DroneManager> {
    private GearSlotData droneLSlot;
    private GearSlotData droneRSlot;
    private DroneBase droneLeft;
    private DroneBase droneRight;
    private Countdowner droneLeftCD;
    private Countdowner droneRightCD;
    private bool droneLeftReborning;
    private bool droneRightReborning;

    public void SetDroneLeft(DroneBase droneLeft) {
        this.droneLeft = droneLeft;
        LoadDroneLData(droneLeft);
    }
    public void SetDroneRight(DroneBase droneRight) {
        this.droneRight = droneRight;
        LoadDroneRData(droneRight);
    }
    public void SetParent(Transform parent) {
        if (droneLeft) {
            droneLeft.transform.SetParent(parent);
        }
        if (droneRight) {
            droneRight.transform.SetParent(parent);
        }
    }

    public void ChangeShotStatus(bool status) {
        if (droneLeft) {
            droneLeft.DroneAttack.ChangeStateShot(status);
        }
        if (droneRight) {
            droneRight.DroneAttack.ChangeStateShot(status);
        }
    }

    private void Start() {
        droneLeftCD = new Countdowner();
        droneRightCD = new Countdowner();
        droneLeftCD.StartCountdown(10);
        droneRightCD.StartCountdown(10);
    }
    private void Update() {
        if (droneLeftReborning) {
            CheckRebornDroneLeft();
        }
        if (droneRightReborning) {
            CheckRebornDroneRight();
        }
    }

    private void CheckRebornDroneLeft() {
        if (droneLeftCD.IsTimeOut()) {
            Reborn(droneLeft);
            droneLeftReborning = false;
        }
        else {
            droneLeftCD.Countdowning(Time.deltaTime);
        }
    }
    private void CheckRebornDroneRight() {
        if (droneRightCD.IsTimeOut()) {
            Reborn(droneRight);
            droneRightReborning = false;
        }
        else {
            droneRightCD.Countdowning(Time.deltaTime);
        }
    }

    private void Reborn(DroneBase drone) {
        if (drone == null)
            return;
        drone.Reborn();
    }
    private void SetStartRebornLeft() {
        droneLeftCD.StartCountdown(droneLeft.DroneStat.RebornCooldown.Value);
        droneLeftReborning = true;
    }
    private void SetStartRebornRight() {
        droneRightCD.StartCountdown(droneRight.DroneStat.RebornCooldown.Value);
        droneRightReborning = true;
    }
    public void SetStartCountdownReborn(DroneBase drone) {
        if (drone == null)
            return;
        if (drone == droneLeft)
            SetStartRebornLeft();
        else if (drone == droneRight)
            SetStartRebornRight();
    }
    private void LoadDroneLData(DroneBase drone) {
        droneLSlot = GameResources.Instance.GearInventory.DroneLSlot;
        if (droneLSlot != null && droneLSlot.IsExist) {
            var itemEquip = droneLSlot.ItemEquip;
            var convertData = itemEquip.GearHardData as DroneGearHardData;
            if (convertData != null) {
                foreach (var item in convertData.SecondStats.RankStat) {
                    drone.DroneStat.SetModifier(item.StatData.StatEvent, item.Values[itemEquip.CurrentRank]);
                }
            }
        }
    }
    private void LoadDroneRData(DroneBase drone) {
        droneRSlot = GameResources.Instance.GearInventory.DroneRSlot;
        if (droneRSlot != null && droneRSlot.IsExist) {
            var itemEquip = droneRSlot.ItemEquip;
            var convertData = itemEquip.GearHardData as DroneGearHardData;
            if (convertData != null) {
                foreach (var item in convertData.SecondStats.RankStat) {
                    drone.DroneStat.SetModifier(item.StatData.StatEvent, item.Values[itemEquip.CurrentRank]);
                }
            }
        }
    }
}
