using Gemmob;
using GameSystem.Common.UI;
using Helper;
using System.Collections;
using UnityEngine;

public class AdsSpinLayout : MonoBehaviour {
    [SerializeField] private AdsSpinItem[] items;
    [SerializeField] private ButtonExplorer spinButton;
    [SerializeField] private float numberChoose;
    [SerializeField] private float deltaChoose;
    [SerializeField] private float acceleration;
    [SerializeField] private float totalTime;
    private AdsSpinInfo[] data;
    private int[] percent;
    private AdsSpinInfo reward;
    private System.Action onClose;

    public void Assign(System.Action onClose) {
        this.onClose = onClose;
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
    public void UpdateUI(AdsSpinInfo[] data, int[] percent) {
        this.data = data;
        this.percent = percent;
        for (int i = 0; i < items.Length; i++) {
            items[i].UpdateUI(data[i]);
        }
        //MixItem();
        if (GameResources.Instance.AutoPlay)
            OnSpin();
    }
    private void OnSpin() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(ISpin());
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
                items[endIndex].PlayChooseEffect(deltaChoose * 4, SpinDone);
                yield break;
            }
            yield return Yielder.Wait(deltaTime);
        }
    }
    private void SpinDone() {
        HUDManager.IgnoreUserInput(false);
        reward.Reward[0].Claim();
        IngameHUD.Instance.Show<RewardPopup>().UpdateClaimUI(reward.Reward, onClose: OnClose);
    }
    private void OnClose() {
        onClose?.Invoke();
    }
}
