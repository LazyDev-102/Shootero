using UnityEngine;
using GameSystem.Common.UI;

public class RateUsPopup : DOTweenFrame {
    [SerializeField] private ButtonExplorer[] starButtons;
    [SerializeField] private GameObject[] activeStars;
    [SerializeField] private ButtonExplorer rateButton;
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private GameObject rateFrame;
    [SerializeField] private GameObject thankYou;
    private void Awake() {
        for (int i = 0; i < starButtons.Length; i++) {
            var index = i;
            starButtons[index].AddEvent(() => OnStarClick(index));
        }
        rateButton.AddEvent(ButtonRateClick);
        backButton.AddEvent(OnClose);
        closeButton.AddEvent(OnClose);
    }
    protected override void OnShow(System.Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        thankYou.SetActive(false);
        rateButton.gameObject.SetActive(false);
        rateFrame.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        for (int i = 0; i < activeStars.Length; i++) {
            activeStars[i].SetActive(false);
        }
    }
    private void GoToStore() {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.galaxyattack.invadershootero");
#elif UNITY_IPHONE
            Application.OpenURL("https://apps.apple.com/vn/app/shootero-space-galaxy-attack/id1547570442");
#endif
    }

    public void ButtonRateClick() {
        GameResources.Instance.RateUs.SetFinishRated(true);
        if (activeStars[activeStars.Length - 1].activeInHierarchy) {
            GoToStore();
            Hide();
        }
        else {
            GameResources.Instance.RateUs.SetFinishRateFailed(true);
            thankYou.SetActive(true);
            rateFrame.SetActive(false);
            backButton.gameObject.SetActive(false);
        }
    }

    public void OnStarClick(int index) {
        for (int i = 0; i < activeStars.Length; i++) {
            activeStars[i].SetActive(i <= index);
        }
        rateButton.gameObject.SetActive(true);
    }

    public void OnClose() {
        GameResources.Instance.RateUs.SetTrigger(GameResources.Instance.ConquerorData.CurrentZoneIndex, false)
                                       .SetClaimEpicItemStatus(false);
        Hide();
    }

    public override Frame OnBack() {
        return this;
    }
    public override void SpecialTrigger(System.Action onCompleted) {
        if (!GameResources.Instance.RateUs.CanSpecialTrigger()) {
            onCompleted?.Invoke();
            return;
        }
        var p = PopupHUD.Instance.Show<RateUsPopup>();
        p.OnOneShotHide = onCompleted;
    }
}
