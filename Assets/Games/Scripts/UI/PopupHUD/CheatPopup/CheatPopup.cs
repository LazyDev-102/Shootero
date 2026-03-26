using GameSystem.Common.UI;
using Gemmob;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatPopup : DOTweenFrame {
    [SerializeField] private ButtonExplorer unlockZoneButton;
    [SerializeField] private ButtonExplorer addEXPButton;
    [SerializeField] private ButtonExplorer addEnergyButton;
    [SerializeField] private ButtonExplorer addGemButton;
    [SerializeField] private ButtonExplorer addCoinButton;
    [SerializeField] private ButtonExplorer addMaterialButton;
    [SerializeField] private ButtonExplorer addGearButton;
    [SerializeField] private ButtonExplorer removeGearButton;
    [SerializeField] private ButtonExplorer openTestScene;
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private ItemCollector matCollector;

    private int indexGear = 0;

    private void Awake() {
        unlockZoneButton.AddEvent(UnlockZone);
        addEXPButton.AddEvent(AddExp);
        addEnergyButton.AddEvent(AddEnergy);
        addGemButton.AddEvent(AddGem);
        addCoinButton.AddEvent(AddCoin);
        addMaterialButton.AddEvent(AddMaterial);
        addGearButton.AddEvent(AddGear);
        removeGearButton.AddEvent(removeGear);
        openTestScene.AddEvent(OpenTestScene);
        backButton.AddEvent(() => Hide());
    }


    private void UnlockZone() {
        GameResources.Instance.ConquerorData.SetNextUnlockZone();
        NotificationText.Instance.Show("Unlock Zone Success!", NotificationText.NoticeType.Info);
    }
    private void AddExp() {
        if (string.IsNullOrEmpty(inputField.text))
            return;
        bool convert = int.TryParse(inputField.text, out int value);
        if (convert) {
            GameResources.Instance.LevelProgress.AddExp(value);
            EventDispatcher.Instance.Dispatch(new EventKey.OnExpChange());
        }
        else {
            NotificationText.Instance.Show("Vui lòng chèn đúng giá trị!", NotificationText.NoticeType.Error);
        }
    }
    private void AddEnergy() {
#if CHEAT
        GameResources.Instance.Inventory.AddXCandy(100);
        GameResources.Instance.Inventory.AddXTicket(1);
#endif
        if (string.IsNullOrEmpty(inputField.text))
            return;
        bool convert = int.TryParse(inputField.text, out int value);
        if (convert) {
            GameResources.Instance.Inventory.Add(ConstantItemID.EnergyId, value);
        }
        else {
            NotificationText.Instance.Show("Vui lòng chèn đúng giá trị!", NotificationText.NoticeType.Error);
        }
    }
    private void AddCoin() {
        if (string.IsNullOrEmpty(inputField.text))
            return;
        bool convert = int.TryParse(inputField.text, out int value);
        if (convert) {
            GameResources.Instance.Inventory.Add(ConstantItemID.ChipId, value);
            EventDispatcher.Instance.Dispatch(new EventKey.OnCoinChange());
        }
        else {
            NotificationText.Instance.Show("Vui lòng chèn đúng giá trị!", NotificationText.NoticeType.Error);
        }
    }
    private void AddGem() {
        if (string.IsNullOrEmpty(inputField.text))
            return;
        bool convert = int.TryParse(inputField.text, out int value);
        if (convert) {
            GameResources.Instance.Inventory.Add(ConstantItemID.GemId, value);
            EventDispatcher.Instance.Dispatch(new EventKey.OnGemChange());
        }
        else {
            NotificationText.Instance.Show("Vui lòng chèn đúng giá trị!", NotificationText.NoticeType.Error);
        }
    }
    private void AddMaterial() {
        if (string.IsNullOrEmpty(inputField.text))
            return;
        bool convert = int.TryParse(inputField.text, out int value);
        if (convert) {
            GameResources.Instance.Inventory.Add(ConstantItemID.WeaponryMatId, value);
            GameResources.Instance.Inventory.Add(ConstantItemID.ShieldMatId, value);
            GameResources.Instance.Inventory.Add(ConstantItemID.ReatorMatId, value);
            GameResources.Instance.Inventory.Add(ConstantItemID.PropulsionMatId, value);
            GameResources.Instance.Inventory.Add(ConstantItemID.DroneMatId, value);
        }
        else {
            NotificationText.Instance.Show("Vui lòng chèn đúng giá trị!", NotificationText.NoticeType.Error);
        }
    }
    private void AddGear() {
        GameResources.Instance.GearInventory.RemoveAll();
        if (indexGear > 4)
            indexGear = 0;
        GameResources.Instance.GearInventory.Add(2001, indexGear);
        GameResources.Instance.GearInventory.Add(2002, indexGear);
        GameResources.Instance.GearInventory.Add(2003, indexGear);
        GameResources.Instance.GearInventory.Add(2004, indexGear);
        GameResources.Instance.GearInventory.Add(2005, indexGear);
        GameResources.Instance.GearInventory.Add(2101, indexGear);
        GameResources.Instance.GearInventory.Add(2102, indexGear);
        GameResources.Instance.GearInventory.Add(2103, indexGear);
        GameResources.Instance.GearInventory.Add(2104, indexGear);
        GameResources.Instance.GearInventory.Add(2105, indexGear);
        GameResources.Instance.GearInventory.Add(2201, indexGear);
        GameResources.Instance.GearInventory.Add(2202, indexGear);
        GameResources.Instance.GearInventory.Add(2203, indexGear);
        GameResources.Instance.GearInventory.Add(2204, indexGear);
        GameResources.Instance.GearInventory.Add(2205, indexGear);
        GameResources.Instance.GearInventory.Add(2301, indexGear);
        GameResources.Instance.GearInventory.Add(2302, indexGear);
        GameResources.Instance.GearInventory.Add(2303, indexGear);
        GameResources.Instance.GearInventory.Add(2304, indexGear);
        GameResources.Instance.GearInventory.Add(2305, indexGear);
        GameResources.Instance.GearInventory.Add(2401, indexGear);
        GameResources.Instance.GearInventory.Add(2401, indexGear);
        GameResources.Instance.GearInventory.Add(2401, indexGear);
        GameResources.Instance.GearInventory.Add(2401, indexGear);
        GameResources.Instance.GearInventory.Add(2402, indexGear);
        GameResources.Instance.GearInventory.Add(2402, indexGear);
        GameResources.Instance.GearInventory.Add(2402, indexGear);
        GameResources.Instance.GearInventory.Add(2402, indexGear);
        GameResources.Instance.GearInventory.Add(2403, indexGear);
        GameResources.Instance.GearInventory.Add(2403, indexGear);
        GameResources.Instance.GearInventory.Add(2403, indexGear);
        GameResources.Instance.GearInventory.Add(2403, indexGear);
        GameResources.Instance.GearInventory.Add(2404, indexGear);
        GameResources.Instance.GearInventory.Add(2404, indexGear);
        GameResources.Instance.GearInventory.Add(2404, indexGear);
        GameResources.Instance.GearInventory.Add(2404, indexGear);
        indexGear++;
    }
    private void removeGear() {
        GameResources.Instance.GearInventory.RemoveAll();
    }
    private void OpenTestScene() {
        SceneManager.LoadScene((int)SceneDefined.Index.Tutorial + 1);
    }
}
