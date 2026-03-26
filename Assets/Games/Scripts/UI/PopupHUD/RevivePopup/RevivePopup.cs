using DG.Tweening;
using Gemmob;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RevivePopup : BasePopup {
    [SerializeField] private Image imgTimeCountdown;
    [SerializeField] private TextMeshProUGUI txtTimeCountdown;
    [SerializeField] private Transform countdown;
    [SerializeField] private ButtonBase btnGem;
    [SerializeField] private ButtonBase btnAds;
    [SerializeField] private ItemView gemCostDisplayer;
    [SerializeField] private float timeCountdown;
    [SerializeField] private ButtonBase btnSkip;
    [SerializeField] private ItemStack gemNeed;
    [SerializeField] private LockbarNotify lockbarNotify;


    private Countdowner timeCountdonwer = new Countdowner();
    private int reviveTime;
    private Coroutine countdownCoroutine;

    protected override void Start() {
        base.Start();
        btnGem?.AddEvent(OnGemButtonClicked);
        btnAds?.AddEvent(OnAdsButtonClicked);
        btnSkip?.AddEvent(OnSkipButtonClicked);
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        StartCoroutine(IStartCountdown());
    }

    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        StopAllCoroutines();
        if (countdownCoroutine != null) {
            StopCoroutine(countdownCoroutine);
        }
        StopCoroutine(IStartCountdown());
    }

    private IEnumerator IStartCountdown() {
        countdownCoroutine = StartCoroutine(ICountdown());
        btnSkip.gameObject.SetActive(false);
        lockbarNotify.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(2f);
        btnSkip.gameObject.SetActive(true);

    }

    private IEnumerator ICountdown() {
        WaitForSecondsRealtime deltaTime = new WaitForSecondsRealtime(Time.fixedDeltaTime);
        timeCountdonwer.StartCountdown(timeCountdown);
        while (timeCountdonwer.IsCountdowning()) {
            SetRatioImageTimeCountdown(timeCountdonwer.Countdown / timeCountdown);
            timeCountdonwer.Countdowning(Time.fixedDeltaTime);
            SetContentTimeCountdown($"{(int)(timeCountdonwer.Countdown) + 1}", true);
            yield return deltaTime;
        }
        Hide();
        GameManager.Instance.Lose();
    }


    private void SetRatioImageTimeCountdown(float ratio) {
        if (imgTimeCountdown) {
            imgTimeCountdown.fillAmount = ratio;
        }
    }

    private void SetContentTimeCountdown(string content, bool show) {
        if (txtTimeCountdown) {
            txtTimeCountdown.gameObject.SetActive(show);
            if (show) {
                txtTimeCountdown.text = content;
            }
        }
    }

    public void SetStateGemButton(bool interaction, bool show) {
        if (btnGem) {
            btnGem.gameObject.SetActive(show);
            if (show) {
                btnGem.SetState(interaction);
            }
        }
    }

    public void SetStateAdsButton(bool interaction, bool show) {
        if (btnAds) {
            btnAds.gameObject.SetActive(show);
            if (show) {
                btnAds.SetState(interaction);
            }
        }
    }

    public void SetGemCost(ItemStack itemNeed) {
        if (gemCostDisplayer) {
            gemCostDisplayer.SetModel(itemNeed).Show();
        }
    }

    private void OnGemButtonClicked() {
        GameResources.Instance.Inventory.EnoughPrice(gemNeed, () => {
            Hide();
            GameManager.Instance.Revive();
            EventDispatcher.Instance.Dispatch(EventKey.OnRevive);
        }, () => {
            ShowLockBarNotify(btnGem.transform);
        });
    }

    private void OnAdsButtonClicked() {
        if (countdownCoroutine != null) {
            StopCoroutine(countdownCoroutine);
        }
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.revive_ads, () => {
            DOVirtual.DelayedCall(0.5f, () => {
                Time.timeScale = 1;
                Hide();
                GameManager.Instance.Revive();
                EventDispatcher.Instance.Dispatch(EventKey.OnRevive);
            });
        }, () => {
            Hide();
            GameManager.Instance.Lose();
        });
    }

    private void OnSkipButtonClicked() {
        Hide();
        GameManager.Instance.Lose();

    }

    protected override void OnCloseButtonClicked() {
        base.OnCloseButtonClicked();
        GameManager.Instance.Lose();
    }


    public void SetReviveTime(int reviveTime) {
        this.reviveTime = reviveTime;
        bool hasVideo = EMAdManager.Instance.HasRewardAds();
        if (reviveTime == 0) {
            SetStateGemButton(true, true);
            SetStateAdsButton(hasVideo, true);
            SetGemCost(gemNeed);
        }
        else {
            StopCoroutine(countdownCoroutine);
            Hide();
            GameManager.Instance.Lose();
        }
    }
    public void ShowLockBarNotify(Transform trans) {
        lockbarNotify.transform.position = trans.position;
        lockbarNotify.SetOriginPos(trans.position - Vector3.up * 1).SetContent(GameDefine.InsufficientResources, 0.5f).Show();
    }
}
