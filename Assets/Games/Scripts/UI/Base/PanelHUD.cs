using DG.Tweening;
using GameSystem.Common.UI;
using Gemmob;
using UnityEngine;

public class PanelHUD : HUD<PanelHUD> {
    #region  Panel Varialbes
    private ShipPanel ship;
    private ConquerorPanel conqueror;
    private GearPanel gear;
    private GearUpgradeSuccesPanel gearUpgradeSucces;
    private GearUpgradePanel1 newGearUpgrade;
    private GearUpgradePanel gearUpgrade;
    private InfinityPanel infinity;
    private NewAbilityPanel ability;
    private ShopPanel shop;
    private ModesPanel modes;


    public ConquerorPanel Conqueror {
        get {
            conqueror = GetActiveFrame<ConquerorPanel>();
            if (conqueror == null) {
                conqueror = GetFrame<ConquerorPanel>();
            }
            return conqueror;
        }
    }
    public GearPanel Gear {
        get {
            if (gear == null) {
                gear = GetActiveFrame<GearPanel>();
                if (gear == null) {
                    gear = GetFrame<GearPanel>();
                }
            }
            return gear;
        }
    }
    public InfinityPanel Infinity {
        get {
            infinity = GetActiveFrame<InfinityPanel>();
            if (infinity == null) {
                infinity = GetFrame<InfinityPanel>();
            }
            return infinity;
        }
    }
    public NewAbilityPanel Ability {
        get {
            ability = GetActiveFrame<NewAbilityPanel>();
            if (ability == null) {
                ability = GetFrame<NewAbilityPanel>();
            }
            return ability;
        }
    }
    public ShopPanel Shop {
        get {
            shop = GetActiveFrame<ShopPanel>();
            if (shop == null) {
                shop = GetFrame<ShopPanel>();
            }
            return shop;
        }
    }
    public ModesPanel Modes {
        get {
            modes = GetActiveFrame<ModesPanel>();
            if (modes == null) {
                modes = GetFrame<ModesPanel>();
            }
            return modes;
        }
    }
    #endregion
    private bool exiting;
    protected override void Start() {
        SoundManager.Instance.PlayBackgroundHome();
        EventDispatcher.Instance.AddListener(EventKey.OnLoadHomeScene, ActionOnloadHomeScene);
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        EventDispatcher.Instance.RemoveListener(EventKey.OnLoadHomeScene, ActionOnloadHomeScene);
    }
    private void ActionOnloadHomeScene() {
        var tut = GameResources.Instance.TutorialSytemData;
        if (!tut.FinishTutorialIntroduce) {
            tut.SetFinishTutorialIntroduce(true)
               .GetRewardKey()
               .GetRewardEnergy();
        }
        GameResources.Instance.Ship.SetTrial(false, 0);
    }
    public override void Back() {
        if (!GameResources.Instance.TutorialSytemData.FinishTutorialEquipment)
            return;
        if (GetActiveFrameCount() == 1) {
            //Show Popup Quit
            var p = GetActiveFrame<DOTweenFrame>() as ConquerorPanel;
            if (p == null) {
                ToolbarScaler.Instance.ShowConquerorPanel();
                return;
            }
            if (exiting) {
#if UNITY_ANDROID
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
#endif

                //Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else {
                exiting = true;
                ToolbarScaler.Instance.LockBar.SetContent($"tap again to exit!", 0.5f).Show();
                //NotificationText.Instance.Show("tap again to exit!").SetColor(Color.white);
                DOVirtual.DelayedCall(2f, () => {
                    exiting = false;
                }).SetUpdate(true);
            }
            return;
        }
        else
            base.Back();
    }
}
