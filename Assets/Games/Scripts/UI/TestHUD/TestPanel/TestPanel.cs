

using GameSystem.Common.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPanel : DOTweenFrame {
    [SerializeField] private ButtonBase xmasButton;
    [SerializeField] private ButtonBase btnHalloween;
    [SerializeField] private ButtonBase btnEnemy;
    [SerializeField] private ButtonBase btnBoss;
    [SerializeField] private ButtonBase btnMod;
    [SerializeField] private ButtonBase btnPattern;
    [SerializeField] private ButtonBase btnReset;
    [SerializeField] private ButtonBase btnKillEnemy;
    [SerializeField] private ButtonBase btnDrone;
    [SerializeField] private ButtonBase btnChest;
    [SerializeField] private ButtonBase btnMiniBoss;
    [SerializeField] private ButtonBase btnExit;
    [SerializeField] private ButtonBase btnHide;
    [SerializeField] private ButtonBase btnChangeShotState;

    [Header("Prefabs")]
    [SerializeField] private ChestBase chestPrefab;

    private void Start() {
        xmasButton.AddEvent(ShowXmasPopup);
        btnHalloween.AddEvent(OnHalloweenButtonClicked);
        btnEnemy.AddEvent(OnEnemyButtonClicked);
        btnBoss.AddEvent(OnBossButtonClicked);
        btnMod.AddEvent(OnModButtonClicked);
        btnPattern.AddEvent(OnPatternButtonClicked);
        btnKillEnemy.AddEvent(KillEnemy);
        btnReset.AddEvent(ResetAll);
        btnDrone.AddEvent(CallDrone);
        btnChest.AddEvent(CallChest);
        btnMiniBoss.AddEvent(OnButtonMiniBossClicked);
        btnExit.AddEvent(OnExit);
        btnHide.AddEvent(OnHide);
        btnChangeShotState.AddEvent(OnChangeShotState);
        btnHalloween.gameObject.SetActive(false);
        btnEnemy.gameObject.SetActive(false);
        btnBoss.gameObject.SetActive(false);
        btnMod.gameObject.SetActive(false);
        btnKillEnemy.gameObject.SetActive(false);
        btnReset.gameObject.SetActive(false);
        btnDrone.gameObject.SetActive(false);
        btnChest.gameObject.SetActive(false);
        btnMiniBoss.gameObject.SetActive(false);
        btnExit.gameObject.SetActive(false);
        btnHide.gameObject.SetActive(false);
        btnChangeShotState.gameObject.SetActive(false);
    }


    private void ShowXmasPopup() {
        TestHUD.Instance.Show<TestMonsterXmasPopup>();
    }

    private void OnEnemyButtonClicked() {
        TestHUD.Instance.Show<TestEnemyPopup>();
    }
    private void OnHalloweenButtonClicked() {
        TestHUD.Instance.Show<TestMonsterHalloweenPopup>();
    }

    private void OnBossButtonClicked() {
        TestHUD.Instance.Show<TestBossPopup>();

    }
    private void OnButtonMiniBossClicked() {
        TestHUD.Instance.Show<TestMiniBossPopup>();

    }

    private void OnModButtonClicked() {
        TestHUD.Instance.Show<TestModPopup>();
    }

    private void OnExit() {
        SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadNormal);
    }
    private int indexHide = 0;
    private void OnHide() {
        indexHide++;
        var status = indexHide % 2 == 0;
        xmasButton.gameObject.SetActive(status);
        btnHalloween.gameObject.SetActive(status);
        btnEnemy.gameObject.SetActive(status);
        btnBoss.gameObject.SetActive(status);
        btnMod.gameObject.SetActive(status);
        btnKillEnemy.gameObject.SetActive(status);
        btnReset.gameObject.SetActive(status);
        btnDrone.gameObject.SetActive(status);
        btnChest.gameObject.SetActive(status);
        btnMiniBoss.gameObject.SetActive(status);
        btnPattern.gameObject.SetActive(status);
        btnExit.gameObject.SetActive(status);
    }
    private void OnPatternButtonClicked() {
        if (GameManager.Instance && GameManager.Instance.GameLoader.Ship) {
            TestHUD.Instance.Show<TestPatternPopup>().SetData(() => {
                xmasButton.gameObject.SetActive(true);
                btnHalloween.gameObject.SetActive(true);
                btnEnemy.gameObject.SetActive(true);
                btnBoss.gameObject.SetActive(true);
                btnMod.gameObject.SetActive(true);
                btnKillEnemy.gameObject.SetActive(true);
                btnReset.gameObject.SetActive(true);
                btnDrone.gameObject.SetActive(true);
                btnChest.gameObject.SetActive(true);
                btnMiniBoss.gameObject.SetActive(true);
                btnHide.gameObject.SetActive(true);
                btnChangeShotState.gameObject.SetActive(true);
                btnExit.gameObject.SetActive(true);

                if (GameManager.Initialized)
                    GameManager.Instance.GameLoader.Ship.ShipLevel.CurrentUpgradeLevel++;
            });
        }
    }
    private void ResetAll() {
        KillEnemy();
        if (newDrone != null)
            Destroy(newDrone);
        GameManager.Instance.GameLoader.Ship.ShipSkill.Initalize();
        GameResources.Instance.GearInventory.RemoveAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void KillEnemy() {
        GameManager.Instance.GameLoader.DespawnAllEnemy(false);
    }
    private bool stateShot = true;
    private void OnChangeShotState() {
        if (GameManager.Initialized && GameManager.Instance.GameLoader.Ship != null) {
            stateShot = !stateShot;
            GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(stateShot);
        }
    }
    DroneBase newDrone;
    private void CallDrone() {
        EquipDrone();
        if (newDrone != null && newDrone.gameObject.activeInHierarchy)
            return;
        var gameLoader = GameManager.Instance.GameLoader;
        (GearSoftData droneGearSoftData1, DroneBase drone1Prefab) = GameResources.Instance.GearInventory.GetDrone1();

        if (drone1Prefab != null) {
            if (newDrone != null)
                Destroy(newDrone);
            newDrone = gameLoader.SpawnDrone1(drone1Prefab, gameLoader.Ship.DroneLeftPos.position, droneGearSoftData1);
            if (newDrone) {
                newDrone.DroneMove.SetFocus(true);
                newDrone.DroneStat.MaxHP.SetBaseValue(1000);
                newDrone.DroneStat.AddModifier(50, 1000, 1, 10);
                newDrone.Initialize();
            }
        }
    }
    private void EquipDrone() {
        (GearSoftData droneGearSoftData1, DroneBase drone1Prefab) = GameResources.Instance.GearInventory.GetDrone1();
        if (drone1Prefab != null)
            return;
        var ran = Random.Range(0, 4);
        GearSoftData data = new GearSoftData(ran == 0 ? 2401 : ran == 1 ? 2402 : ran == 2 ? 2403 : 2404);
        GameResources.Instance.GearInventory.Add(data);
        if (data.IsEquiped) {
            return;
            //GameResourcesIG.Instance.GearInventory.UnEquip(data.GearTypeSoft);
        }
        else {
            var slot1 = GameResources.Instance.GearInventory.GetDroneLEquipable();
            if (slot1 == null) {
                data.SetGearTypeSoft(GearType.Drone1);
                GameResources.Instance.GearInventory.EquipUI(data);
            }
            else {
                GameResources.Instance.GearInventory.UnEquip(GearType.Drone1);
                GameResources.Instance.GearInventory.EquipUI(data);
            }
        }
    }
    private void CallChest() {
        GameManager.Instance.GameLoader.SpawnChest(chestPrefab, Vector2.zero).Initialize();
    }
}
