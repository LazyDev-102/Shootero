using DG.Tweening;
using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipEnhancePopup : DOTweenFrame, ILayout<ShipEnhanceItem, ShipSpecialInfo> {
    [SerializeField] float timeScaleText = 0.2f;
    [SerializeField] private TextMeshProUGUI shipNameText;
    [SerializeField] private TextMeshProUGUI shipLevelText;
    [SerializeField] private TextMeshProUGUI shipAttackValueText;
    [SerializeField] private TextMeshProUGUI shipAttackIncreaseValueText;
    [SerializeField] private TextMeshProUGUI shipHPValueText;
    [SerializeField] private TextMeshProUGUI shipHPIncreaseValueText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI enhanceText;
    [SerializeField] private GameObject maxText;
    [SerializeField] private Image shipIcon;
    [SerializeField] private Image priceIcon;
    [SerializeField] private ButtonExplorer enhanceButton;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ShipEnhanceItem itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private ParticleSystem enhanceEffect;
    [SerializeField] private LockbarNotify lockbar;
    [SerializeField] private GameObject warning;
    [SerializeField] private TextMeshProUGUI levelReachText;

    private Action onClose;
    private ShipInfor data;
    public List<ShipEnhanceItem> Items { get; set; } = new List<ShipEnhanceItem>();
    private void Awake() {
        enhanceButton.AddEvent(OnEnhance);
        closeButton.AddEvent(Close);
    }

    public void GenerateItem() {
        List<ShipSpecialInfo> shipSpecialInfos = data.ShipSpecial;
        if (Items != null && Items.Count > shipSpecialInfos.Count) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < shipSpecialInfos.Count) {
                    Items[i].UpdateUI(shipSpecialInfos[i], data.CurrentLevel, false);
                }
                Items[i].gameObject.SetActive(i < shipSpecialInfos.Count);
            }
        }
        else {
            for (int i = 0; i < shipSpecialInfos.Count; i++) {
                if (Items == null || i >= Items.Count) {
                    var gearItem = itemPrefab.Spawn(container);
                    gearItem.transform.localScale = Vector3.one;
                    Items.Add(gearItem);
                }
                Items[i].UpdateUI(shipSpecialInfos[i], data.CurrentLevel, false);
                Items[i].gameObject.SetActive(true);
            }
        }
    }

    public void UpdateUI(ShipInfor data) {
        this.data = data;
        UpdateUIInforShip();
        GenerateItem();
        CheckEnvoluration();
    }
    private void UpdateUIInforShip() {
        shipIcon.sprite = data.GetIcon();
        shipLevelText.text = $"Level {data.CurrentLevel + 1}";
        shipAttackValueText.text = $"{(int)data.GetCurrentAttack()}";
        shipAttackIncreaseValueText.text = $"(+{data.GetNextAttackInc(data.CurrentLevel)})";
        shipHPValueText.text = $"{(int)data.GetCurrentHP()}";
        shipHPIncreaseValueText.text = $"(+{data.GetNextHPInc(data.CurrentLevel)})";
        EnhanceStatusUI();
        if (data.IsMax)
            return;
        priceText.text = $"{data.Levels[data.CurrentLevel + 1].Price.Amount}";
        priceIcon.sprite = data.Levels[data.CurrentLevel + 1].Price.Icon;
    }
    private void EnhanceStatusUI() {
        int cLevel = GameResources.Instance.LevelProgress.GetCurrentLevel();
        bool overLevel = data.CurrentLevel >= cLevel;
        lockbar.gameObject.SetActive(false);
        enhanceButton.SetState(!data.IsMax);
        maxText.SetActive(data.IsMax);
        warning.SetActive(overLevel);
        enhanceButton.gameObject.SetActive(!overLevel);
        levelReachText.text = $"{cLevel + 2}";
    }
    private void OnEnhance() {
        ItemStack price = data.Levels[data.CurrentLevel + 1].Price;
        ItemStack curCurrency = GameResources.Instance.Inventory.GetItem(price.Id);
        if (curCurrency.Amount >= price.Amount) {
            if (GameResources.Instance.Ship.Enhance(data.ID)) {
                var evo = data.GetShipEvolution();
                ChangeTextOnEnhance(!(evo != null && evo.Level == data.CurrentLevel + 1));
                GameResources.Instance.Inventory.Add(price.Id, -price.Amount);
                UpdateUIInforShip();
                GenerateItem();
                //Tracking.Instance.TrackingOnEnhanceShip();
                GameResources.Instance.DailyMission.AddPointProgress(MissionType.UpgradeShip, 1);
                EventDispatcher.Instance.Dispatch(EventKey.OnUpgradeShip);
                CheckEnvoluration();
                Tracking.Instance.LogShip($"{data.ID}", data.CurrentLevel);
                return;
            }
        }
        else {
            ShowLockBarNotify();
        }
    }
    private void CheckEnvoluration() {
        enhanceText.text = data.CheckEnvoluration() ? "Evolve" : "Enhance";
        var evo = data.GetShipEvolution();
        if (evo != null && evo.Level == data.CurrentLevel + 1) {
            evo.EvolutionState = true;
            PopupHUD.Instance.Show<ShipEvolutionPopup>().Init(data);
        }
    }
    public void ShowLockBarNotify() {
        lockbar.transform.position = enhanceButton.transform.position;
        lockbar.SetOriginPos(enhanceButton.transform.position - Vector3.up * 1).SetContent(GameDefine.InsufficientResources, 0.5f).Show();
    }
    private void ChangeTextOnEnhance(bool playEffect = true) {
        shipAttackValueText.transform.DOKill(true);
        shipHPValueText.transform.DOKill(true);
        shipLevelText.transform.DOKill(true);
        shipAttackValueText.transform.DOScale(1.2f, timeScaleText).SetEase(Ease.Linear).OnComplete(() => {
            shipAttackValueText.transform.DOScale(1, timeScaleText).SetEase(Ease.Linear);
        });
        shipHPValueText.transform.DOScale(1.2f, timeScaleText).SetEase(Ease.Linear).OnComplete(() => {
            shipHPValueText.transform.DOScale(1, timeScaleText).SetEase(Ease.Linear);
        });
        shipLevelText.transform.DOScale(1.2f, timeScaleText).SetEase(Ease.Linear).OnComplete(() => {
            shipLevelText.transform.DOScale(1, timeScaleText).SetEase(Ease.Linear);
        });
        if (enhanceEffect != null && playEffect) {
            enhanceEffect.Stop();
            enhanceEffect.Play();
        }
    }
    public ShipEnhancePopup AddOnClose(Action onClose) {
        this.onClose = onClose;
        return this;
    }
    private void Close() {
        Hide();
        onClose?.Invoke();
    }
}
