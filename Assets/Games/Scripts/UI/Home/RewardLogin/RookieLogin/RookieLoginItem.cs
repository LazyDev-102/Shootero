using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class RookieLoginItem : MonoBehaviour, IItem<RookieLoginInfor> {
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject notify;
    [SerializeField] private GameObject tick;
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject frameSelected;
    public RookieLoginInfor dataStack { get; set; }
    private int dayClaimable;
    private bool claimable;
    private bool onPlayEffectClaim;

    public IItem<RookieLoginInfor> Generate() {
        var tickStatus = dataStack.Day < dayClaimable;
        var notifyStatus = claimable && dataStack.Day == dayClaimable;
        dayText.text = $"Day {dataStack.Day}";
        descriptionText.text = $"x{dataStack.Rewards[0].Amount}";
        icon.sprite = dataStack.Rewards[0].Icon;
        notify.SetActive(notifyStatus);
        tick.SetActive(tickStatus);
        locked.SetActive(tickStatus);
        frameSelected.SetActive(notifyStatus);
        return this;
    }
    public void Initialized(RookieLoginInfor data, int dayClaimable, bool claimable, bool onPlayEffectClaim) {
        this.dataStack = data;
        this.dayClaimable = dayClaimable + 1;
        this.claimable = claimable;
        this.onPlayEffectClaim = onPlayEffectClaim;
        Generate();
    }
}
