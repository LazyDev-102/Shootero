

using GameSystem.Common.UI;

public class IngameHUD : HUD<IngameHUD> {
    [UnityEngine.SerializeField] private CombatPanelAction[] combatActions;
    #region Ingame Popup Varialbles
    private ConfirmPopup confirm;
    private CombatPanel combat;
    private AdsSpinPopup adsSpin;
    private MysteryStationPopup mysteryStation;
    private FullHealPopup fullHeal;
    private SpaceMerchantOffer spaceMerchant;
    private AngleOfferPopup angel;
    public ConfirmPopup Confirm {
        get {
            if (confirm == null) {
                confirm = GetActiveFrame<ConfirmPopup>();
                if (confirm == null) {
                    confirm = GetFrame<ConfirmPopup>();
                }
            }
            return confirm;
        }
    }
    public CombatPanel Combat {
        get {
            if (combat == null)
                combat = combatActions[(int)IngameData.currentGameMode].GetCombat();
            return combat;
        }
    }
    public AdsSpinPopup AdsSpin {
        get {
            if (adsSpin == null) {
                adsSpin = GetActiveFrame<AdsSpinPopup>();
                if (adsSpin == null) {
                    adsSpin = GetFrame<AdsSpinPopup>();
                }
            }
            return adsSpin;
        }
    }
    public MysteryStationPopup MysteryStation {
        get {
            if (mysteryStation == null) {
                mysteryStation = GetActiveFrame<MysteryStationPopup>();
                if (mysteryStation == null) {
                    mysteryStation = GetFrame<MysteryStationPopup>();
                }
            }
            return mysteryStation;
        }
    }
    public FullHealPopup FullHeal {
        get {
            if (fullHeal == null) {
                fullHeal = GetActiveFrame<FullHealPopup>();
                if (fullHeal == null) {
                    fullHeal = GetFrame<FullHealPopup>();
                }
            }
            return fullHeal;
        }
    }
    public SpaceMerchantOffer SpaceMerchant {
        get {
            if (spaceMerchant == null) {
                spaceMerchant = GetActiveFrame<SpaceMerchantOffer>();
                if (spaceMerchant == null) {
                    spaceMerchant = GetFrame<SpaceMerchantOffer>();
                }
            }
            return spaceMerchant;
        }
    }
    #endregion
    public override void Back() {
        if (GetFrameOnTop<CombatPanel>() != null) {
            Combat.OnBack();
            return;
        }
        if (!GameResources.Instance.TutorialSytemData.FinishTutorialEquipment)
            return;
        base.Back();
    }
    public void ShowConfirm(System.Action successAction, System.Action failAction, string title = "", string content = "", string btnConfirmTitle = "", string btnCancelTitle = "", bool hideOnYes = true, bool hideOnNo = true, bool btnClose = false) {
        Confirm.transform.SetAsLastSibling();
        Confirm.Init(successAction, failAction, title, content, btnConfirmTitle, btnCancelTitle, hideOnYes, hideOnNo, btnClose)
                .Show();
    }
    public T GetCombat<T>() where T : CombatPanel {
        if (combat is T cb) {
            return cb;
        }
        if (combat != null)
            Gemmob.Logs.LogWarning("Get Wrong CombatPanel!!! This is " + combat.GetType().Name + " not " + typeof(T).Name);
        return null;
    }
}
