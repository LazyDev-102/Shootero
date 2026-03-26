using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RookieLoginItemSpecial : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject notify;
    [SerializeField] private GameObject tick;
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject frameSelected;

    private RookieLoginData rookieData;
    public RookieLoginInfor data { get; set; }
    private int dayClaimable;
    private bool claimable;
    private ShipInfor convertData;
    private bool playClaimEffect;

    public void Generate() {
        var tickStatus = rookieData.IsComplete;
        var notifyStatus = claimable && data.Day == dayClaimable;

        dayText.text = $"Day {data.Day}";
        descriptionText.text = data.Rewards[0].Description;
        icon.sprite = data.Rewards[0].Icon;
        notify.SetActive(notifyStatus && !tickStatus);
        tick.SetActive(tickStatus);
        locked.SetActive(tickStatus);
        frameSelected.SetActive(notifyStatus && !tickStatus);
    }
    public void Initialized(RookieLoginInfor data, int dayClaimable, bool claimable, bool playClaimEffect) {
        rookieData = GameResources.Instance.RookieLoginData;
        this.data = data;
        this.dayClaimable = dayClaimable + 1;
        this.claimable = claimable;
        this.playClaimEffect = playClaimEffect;
        Generate();
    }
}
