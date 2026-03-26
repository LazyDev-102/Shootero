
using DG.Tweening;
using GameSystem.Common.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEvolutionPopup : DOTweenFrame {
    [SerializeField] private float timeAppear = 1;
    [SerializeField] private float timeMove = 0.5f;
    [SerializeField] private Image oldIcon;
    [SerializeField] private Image icon;
    [SerializeField] private Transform appearPoint;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform topTrans;
    [SerializeField] private Transform statsTrans;
    [SerializeField] private ParticleSystem[] glowEffect;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject tabtoHideGO;
    [SerializeField] private GameObject tranferEffectGo;
    [SerializeField] private ParticleSystem tranferEffect;
    [SerializeField] private ShipEnhanceItem[] shipEnhanceItems;
    [SerializeField] private WhiteFrameEffect whiteFrame;

    private ShipInfor shipInfo;
    private void Awake() {
        closeButton.interactable = false;
        closeButton.onClick.AddListener(OnClose);
    }

    public void Init(ShipInfor shipInfo) {
        this.shipInfo = shipInfo;
        UpdateUI();
    }
    private void UpdateUI() {
        HeadHUD.Instance.HideAll();
        icon.sprite = shipInfo.GetIcon();
        oldIcon.sprite = shipInfo.GetOldIcon();
        UpdateBonusSpecial(shipInfo);
        PlayEffect();
    }
    private void UpdateBonusSpecial(ShipInfor data) {
        List<ShipSpecialInfo> shipSpecialInfos = data.ShipSpecial;
        for (int i = 0; i < shipEnhanceItems.Length; i++) {
            shipEnhanceItems[i].UpdateUI(shipSpecialInfos[i], data.CurrentLevel, i == (data.CurrentLevel + 1) / 20);
        }
    }
    private void OnClose() {
        Hide();
        HeadHUD.Instance.Show<HeadPanel>();
        ResetState();
    }
    private void ResetState() {
        topTrans.position = appearPoint.position;
        topTrans.localScale = Vector3.zero;
        statsTrans.localScale = Vector3.zero;
        tabtoHideGO.SetActive(false);
        closeButton.interactable = false;
    }
    public void Show() {
        topTrans.gameObject.SetActive(true);
        topTrans.DOScale(Vector3.one, timeAppear).SetEase(Ease.Linear)
            .OnComplete(() => {
                topTrans.DOMove(topPoint.position, timeMove).OnComplete(() => {
                    statsTrans.gameObject.SetActive(true);
                    statsTrans.DOScale(Vector3.one, timeAppear).OnComplete(() => {
                        tabtoHideGO.SetActive(true);
                        closeButton.interactable = true;
                    });
                });
            });
    }
    private void PlayEffect() {
        if (tranferEffectGo != null && tranferEffect != null) {
            tranferEffectGo.SetActive(true);
            tranferEffect.Play();
        }
        DOVirtual.DelayedCall(1f, PlayWhiteFrame);
    }
    private void PlayWhiteFrame() {
        tranferEffectGo.SetActive(false);
        whiteFrame.Show(Show);

    }
    public override Frame OnBack() {
        OnClose();
        return this;
    }
}
