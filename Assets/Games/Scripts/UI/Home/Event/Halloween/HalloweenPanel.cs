using DG.Tweening;
using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HalloweenPanel : DOTweenFrame
{
    [SerializeField] private HalloweenMissionFrame halloweenMission;
    [SerializeField] private ButtonBase playButton;
    [SerializeField] private ButtonBase exchangeButton;
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private Transform[] effects;

    private HalloweenModeData data;

    private void Awake() {
        SetData();
        AddEvent();
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnClaimHalloweenMission>(PlayEffectOnClaimMission);
        EventDispatcher.Instance.RemoveListener(EventKey.OnHTicketChanged, UpdatePlayText);
    }
    private void SetData() {
        data = GameResources.Instance.Halloween;
        halloweenMission.Initialize(GameResources.Instance.HalloweenMission);
    }
    private void AddEvent() {
        playButton.AddEvent(PlayGame);
        closeButton.AddEvent(OnClose);
        exchangeButton.AddEvent(OpenHalloweenShop);
        EventDispatcher.Instance.AddListener<EventKey.OnClaimHalloweenMission>(PlayEffectOnClaimMission);
        EventDispatcher.Instance.AddListener(EventKey.OnHTicketChanged, UpdatePlayText);
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        ToolbarScaler.Instance.SetActive(true);
        HeadHUD.Instance.Show<HeadPanel>();
    }

    private void UpdateUI() {
        ToolbarScaler.Instance.SetActive(false);
        HeadHUD.Instance.Hide<HeadPanel>();
        halloweenMission.GenerateItem();
        UpdatePlayText();
    }

    private void UpdatePlayText() {
        playText.text = $"PLAY {GameResources.Instance.Inventory.GetHTicket().Amount}/{data.MaxTurn}";
    }

    private void PlayGame() {
        if(GameResources.Instance.Inventory.GetHTicket().Amount <= 0) {
            PopupHUD.Instance.Show<HalloweenMoreTicketPopup>();
        } else {
            Gemmob.EventDispatcher.Instance.Dispatch(EventKey.HalloweenPlayGame);
            data.ChangeTurnRemain(1);
            IngameData.PlayGame(GameMode.EventHalloween);
        }
    }

    private void OpenHalloweenShop() {
        PanelHUD.Instance.Show<HalloweenShopPanel>();
    }
    private IEnumerator IEPlayEffect(Vector2 pos) {
        playingEffect = true;
        for (int i = 0; i < effects.Length; i++) {
            var index = i;
            effects[index].localPosition = pos +  new Vector2(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, -50f));
            yield return Yielder.Wait(UnityEngine.Random.Range(0f, 0.3f));
            effects[index].gameObject.SetActive(true);
            effects[index].DOScale(UnityEngine.Random.Range(0.5f, 1f), 0.5f);
            effects[index].DOMove(exchangeButton.transform.position, 1.5f)
                     .SetEase(Ease.InExpo)
                     .OnComplete(() => {
                         effects[index].gameObject.SetActive(false);
                         effects[index].localPosition = Vector3.zero;
                     });
        }
        yield return StartCoroutine(DisableEffect());
    }
    private bool playingEffect;
    private void PlayEffectOnClaimMission(EventKey.OnClaimHalloweenMission param) {
        if (gameObject.activeInHierarchy && !playingEffect) {
            StartCoroutine(IEPlayEffect(param.Position));
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

    private void OnClose() {
        base.OnBack();
    }
}
