using UnityEngine;
using GameSystem.Common.UI;
using TMPro;
using System.Collections;
using Gemmob;
using DG.Tweening;
using UnityEngine.UI;

public class NewAbilityPanel : DOTweenFrame {
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showL2R;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showR2L;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideL2R;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideR2L;

    [SerializeField] private ButtonExplorer buyButton;
    [SerializeField] private ButtonExplorer resetButton;
    [SerializeField] private ButtonExplorer resetAdsButton;
    [SerializeField] private LockbarNotify lockbar;

    [SerializeField] private TextMeshProUGUI abilityPointText;
    [SerializeField] private TextMeshProUGUI priceValueText;
    [SerializeField] private TextMeshProUGUI resetValueText;
    [SerializeField] private NewAbilityItemView[] items;

    [SerializeField] private GameObject resetGroup;
    [SerializeField] private ButtonBase confirmReset;
    [SerializeField] private ButtonBase cancelReset;
    [SerializeField] private ButtonBase tabToCloseReset;

    [SerializeField] private Transform[] effects;
    [SerializeField] private Image frameSelect;
    [SerializeField] private Image whiteFrame;
    [SerializeField] private float effectTime;

    [Header("Cheat")]
    [SerializeField] private ButtonBase cheatButton;

    private NewAbilityData data;

    private void Start() {
        AddEvent();
        SetData();
    }

    private void SetData() {
        if (data == null) {
            data = GameResources.Instance.AbilityData;
        }
    }

    private void AddEvent() {
        buyButton.AddEvent(OnBuy);
        resetButton.AddEvent(OnReset);
        resetAdsButton.AddEvent(OnResetAds);
        cheatButton.AddEvent(OnCheat);
        confirmReset.AddEvent(OnConfirmReset);
        cancelReset.AddEvent(OnCancelReset);
        tabToCloseReset.AddEvent(OnCancelReset);
    }

    private void UpdateUI() {
        bool hasPoint = data.Point != data.TotalPoint;
        lockbar.gameObject.SetActive(false);
        resetGroup.SetActive(false);
        resetAdsButton.gameObject.SetActive(hasPoint && data.ResettableAds());
        resetButton.gameObject.SetActive(hasPoint && !resetAdsButton.gameObject.activeInHierarchy);
        abilityPointText.text = $"{data.Point}";
        priceValueText.text = $"{data.Price.Amount}";
        priceValueText.color = GameResources.Instance.Inventory.EnoughPrice(data.Price)? Color.white : Color.red;
        resetValueText.text = $"{data.ResetPrice.Amount}";
        resetValueText.color = GameResources.Instance.Inventory.EnoughPrice(data.ResetPrice) ? Color.white : Color.red;
        foreach (var ability in items) {
            ability.Initialize(OnCloseInfoPopup);
        }
#if !CHEAT
        cheatButton.gameObject.SetActive(false);
#endif
    }

    private void OnCloseInfoPopup() {
        bool hasPoint = data.Point != data.TotalPoint;
        abilityPointText.text = $"{data.Point}";
        resetAdsButton.gameObject.SetActive(hasPoint && data.ResettableAds());
        resetButton.gameObject.SetActive(hasPoint && !resetAdsButton.gameObject.activeInHierarchy);
    }

    private void OnConfirmReset() {
        GameResources.Instance.Inventory.EnoughPrice(data.ResetPrice, () => {
            data.ResetAll(false);
            UpdateUI();
            PlayEffectOnReset();
        }, () => {
            lockbar.SetContent(GameDefine.InsufficientResources, 0.5f).Show();
        });
    }
    private void OnCancelReset() {
        resetGroup.SetActive(false);
    }

    private void OnReset() {
        resetGroup.SetActive(true);
    }

    private void OnResetAds() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.ability_ads, () => {
            data.ResetAll(true);
            UpdateUI();
            PlayEffectOnReset();
        });
    }

    private void OnBuy() {
        if (data.Buyable) {
            GameResources.Instance.Inventory.EnoughPrice(data.Price, () => {
                data.BuyPoint();
                //UpdateUI();
                PlayChooseEffect(effectTime);
            }, () => {
                lockbar.SetContent(GameDefine.InsufficientResources, 0.5f).Show();
            });
        }
        else {
            lockbar.SetContent(GameDefine.UpgradeYourGrade, 0.5f).Show();
        }
    }
    public void PlayChooseEffect(float deltaTime) {
        HUDManager.IgnoreUserInput(true);
        transform.DOKill(true);
        whiteFrame.gameObject.SetActive(true);
        whiteFrame.SetAlpha(1);
        whiteFrame.transform.DOScale(Vector3.one * 2, deltaTime).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
            frameSelect.gameObject.SetActive(true);
            frameSelect.transform.DOScale(Vector3.one * 1.2f, deltaTime).SetUpdate(true).OnComplete(() => {
                whiteFrame.DOFade(0, deltaTime * 2).SetUpdate(true).OnComplete(() => {
                    UpdateUI();
                    HUDManager.IgnoreUserInput(false);
                });
                frameSelect.DOFade(0, deltaTime).SetUpdate(true);

            });
        });
    }

    private bool playingEffect;
    private IEnumerator IEPlayEffect(Vector2 pos) {
        playingEffect = true;
        for (int i = 0; i < effects.Length; i++) {
            var index = i;
            effects[index].localPosition = pos + new Vector2(UnityEngine.Random.Range(-50f, 50f), Random.Range(-50f, -50f));
            yield return Yielder.Wait(Random.Range(0f, 0.3f));
            effects[index].gameObject.SetActive(true);
            effects[index].DOScale(Random.Range(0.5f, 1f), 0.5f);
            effects[index].DOMove(abilityPointText.transform.position, 1.5f)
                     .SetEase(Ease.InExpo)
                     .OnComplete(() => {
                         effects[index].gameObject.SetActive(false);
                         effects[index].localPosition = Vector3.zero;
                     });
        }
        yield return StartCoroutine(DisableEffect());
    }
    private void PlayEffectOnReset() {
        if (gameObject.activeInHierarchy && !playingEffect) {
            StartCoroutine(IEPlayEffect(Vector2.zero));
        }
    }

    private IEnumerator DisableEffect() {
        yield return Yielder.Wait(2);
        for (int i = 0; i < effects.Length; i++) {
            DOTween.Kill(effects[i]);
            effects[i].gameObject.SetActive(false);
        }
        playingEffect = false;
    }
    private void OnCheat() {
#if CHEAT
        data.AddPoint(100);
        UpdateUI();
#endif
    }

    protected override void OnShow(System.Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        SetData();
        UpdateUI();
    }

    public override Frame SetAnimShow(bool l2r) {
        showAnimation = l2r ? showL2R : showR2L;
        return this;
    }

    public override Frame SetAnimHide(bool l2r) {
        hideAnimation = l2r ? hideL2R : hideR2L;
        return this;
    }
}
