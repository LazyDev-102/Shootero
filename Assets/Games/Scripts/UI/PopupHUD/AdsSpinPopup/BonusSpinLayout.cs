using Gemmob;
using GameSystem.Common.UI;
using Helper;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BonusSpinLayout : MonoBehaviour {
    [SerializeField] private BonusSpinItem[] items;
    [SerializeField] private ButtonExplorer spinButton;
    [SerializeField] private float numberChoose;
    [SerializeField] private float deltaChoose;
    [SerializeField] private float acceleration;
    [SerializeField] private float totalTime;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private TMPro.TextMeshProUGUI timeLeft;
    private BonusSpinInfo[] data;
    private int[] percent;
    private BonusSpinInfo reward;
    private ItemClaim[] display = new ItemClaim[1];
    private int cSpin;
    public void Assign() {
        spinButton.AddEvent(OnSpin);
    }


    private void MixItem() {
        for (int i = 0; i < items.Length; i++) {
            bool ran = Random.Range(0, 2) == 0;
            if (ran)
                items[i].transform.SetAsFirstSibling();
            else
                items[i].transform.SetAsLastSibling();
        }
    }
    public void UpdateUI(BonusSpinInfo[] data, int[] percent) {
        this.data = data;
        this.percent = percent;
        cSpin = GameResources.Instance.AdsSpin.CurrentSpin;
        timeLeft.text = $"Time left: {cSpin}";
        spinButton.SetState(cSpin > 0);
        for (int i = 0; i < items.Length; i++) {
            items[i].UpdateUI(data[i])
                    .SetColor(data[i].IsSpecial ? highlightColor : normalColor);
        }
        //MixItem();
    }
    private void OnSpin() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.ads_spin, () => {
            var pause = PopupHUD.Instance.GetFrameOnTop<PausePopup>();
            if (pause != null)
                pause.OnBack();
            DOVirtual.DelayedCall(1, () => {
                GameResources.Instance.AdsSpin.DeSpin();
                timeLeft.text = $"Time left: {cSpin - 1}";
                if (gameObject.activeInHierarchy)
                    StartCoroutine(ISpin());
            }).SetUpdate(true);
        });
    }
    private IEnumerator ISpin() {
        HUDManager.IgnoreUserInput(true);
        int startIndex = Random.Range(0, items.Length);
        int endIndex = RandomHelper.RandomWithPercent(percent);
        reward = data[endIndex];
        float timeUse = 0f;
        float deltaTime = 0f;
        int length = items.Length;
        int indexChoose = 0;
        for (int i = startIndex; i < numberChoose; ++i) {
            indexChoose = i % length;
            StartCoroutine(items[indexChoose].PlayEffect(0.3f));
            deltaTime = deltaChoose + i * acceleration;
            timeUse += deltaTime;
            if (indexChoose == endIndex && timeUse >= totalTime) {
                items[endIndex].PlayChooseEffect(deltaChoose * 2, SpinDone);
                yield break;
            }
            yield return Yielder.Wait(deltaTime);
        }
    }
    private void SpinDone() {
        spinButton.SetState(false);
        HUDManager.IgnoreUserInput(false);
        display[0] = reward.Reward;
        reward.Reward.Claim();
        IngameHUD.Instance.Show<RewardPopup>().UpdateClaimUI(display);
    }
}