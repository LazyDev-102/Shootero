using Gear_Data;
using Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleOpenChestGearDisplayer : View<GearSoftData> {
    [SerializeField] private Image imgIcon;
    [SerializeField] private Image imgBorder;
    [SerializeField] private ParticleSystem[] effects;
    [SerializeField] private DotweenAnimation showAnim;
    [SerializeField] private ButtonExplorer btnGearInfo;
    [SerializeField] private WhiteFrameEffect whiteFrame;
    private bool canShowEffect;
    private bool canShowWhiteFrame;
    private void Awake() {
        btnGearInfo.AddEvent(ShowGearInfo);
    }
    private void OnDisable() {
        SetStatusEffect(false);
        canShowWhiteFrame = false;
    }
    public override void Show() {
        if (Model == null) {
            return;
        }

        GearHardData gearHardData = Model.GearHardData;
        RaretyData curRaretyData = Model.CurrentRaretyData;
        SetIcon(gearHardData.Icon, true);
        SetBorder(curRaretyData.Frame, true);
        ShowAnim();
    }

    private void ShowAnim() {
        if (!canShowWhiteFrame) {
            whiteFrame.Show();
            canShowWhiteFrame = true;
        }
        if (showAnim) {
            showAnim.Play(() => ShowEffect(), true);
        }
        else {
            ShowEffect();
        }
    }
    private void ShowEffect() {
        foreach (var item in effects) {
            if (!canShowEffect)
                continue;
            item.gameObject.SetActive(true);
            //item.ChangeColorParticle(Model.GearHardData.GetRarety(Model.CurrentRank).Color);
        }
    }
    private void SetStatusEffect(bool status) {
        foreach (var e in effects) {
            if (e != null) {
                e.gameObject.SetActive(status);
            }
        }
    }
    public SimpleOpenChestGearDisplayer SetShowEffect(bool show) {
        return this;
    }
    public SimpleOpenChestGearDisplayer SetShowFrameEffect(bool show) {
        canShowEffect = show;
        return this;
    }

    private void SetIcon(Sprite icon, bool show) {
        if (imgIcon) {
            imgIcon.gameObject.SetActive(show);
            if (show) {
                imgIcon.sprite = icon;
            }
        }
    }
    private void SetBorder(Sprite icon, bool show) {
        if (imgBorder) {
            imgBorder.gameObject.SetActive(show);
            if (show) {
                imgBorder.sprite = icon;
            }
        }
    }
    private void ShowGearInfo() {
        PopupHUD.Instance.Show<GearDetailItemPopup>().InitData(Model, null, false, false, null, false);
    }
}
