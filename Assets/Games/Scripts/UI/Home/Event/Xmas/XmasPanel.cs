using DG.Tweening;
using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class XmasPanel : DOTweenFrame {
    [SerializeField] private XmasMissionFrame XmasMission;
    [SerializeField] private ButtonBase playButton;
    [SerializeField] private ButtonBase exchangeButton;
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private Transform[] effects;

    private XmasModeData data;

    private void Awake() {
        SetData();
        AddEvent();
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnClaimXmasMission>(PlayEffectOnClaimMission);
        EventDispatcher.Instance.RemoveListener(EventKey.OnHTicketChanged, UpdatePlayText);
    }
    private void SetData() {
        data = GameResources.Instance.Xmas;
        XmasMission.Initialize(GameResources.Instance.XmasMission);
    }
    private void AddEvent() {
        playButton.AddEvent(PlayGame);
        closeButton.AddEvent(OnClose);
        exchangeButton.AddEvent(OpenXmasShop);
        EventDispatcher.Instance.AddListener<EventKey.OnClaimXmasMission>(PlayEffectOnClaimMission);
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

    public void UpdateUI() {
        ToolbarScaler.Instance.SetActive(false);
        HeadHUD.Instance.Hide<HeadPanel>();
        XmasMission.GenerateItem();
        UpdatePlayText();
    }

    private void UpdatePlayText() {
        playText.text = $"PLAY {GameResources.Instance.Inventory.GetXTicket().Amount}/1";
    }

    private void PlayGame() {
        if (GameResources.Instance.Inventory.GetXTicket().Amount <= 0) {
            PopupHUD.Instance.Show<XmasMoreTicketPopup>();
        }
        else {
            Gemmob.EventDispatcher.Instance.Dispatch(EventKey.XmasPlayGame);
            data.ChangeTurnRemain(1);
            IngameData.PlayGame(GameMode.EventXmas);
        }
    }

    private void OpenXmasShop() {
        PanelHUD.Instance.Show<XmasShopPanel>();
    }
    private IEnumerator IEPlayEffect(Vector2 pos) {
        playingEffect = true;
        for (int i = 0; i < effects.Length; i++) {
            var index = i;
            effects[index].localPosition = pos + new Vector2(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, -50f));
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
    private void PlayEffectOnClaimMission(EventKey.OnClaimXmasMission param) {
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
