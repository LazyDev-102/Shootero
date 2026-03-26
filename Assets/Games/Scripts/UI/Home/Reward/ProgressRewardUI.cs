using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ProgressRewardUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI zoneIndexText;
    [SerializeField] TextMeshProUGUI waveReachText;
    [SerializeField] TextMeshProUGUI titletext;
    [SerializeField] Image background;
    [SerializeField] Image frameSelect;

    private float timeMove = 0.5f;

    public ProgressRewardUI UpdateUI(int zoneIndex, int waveReach) {
        if (zoneIndex != -1 && waveReach != -1) {
            gameObject.SetActive(true);
            zoneIndexText.text = $"Zone {zoneIndex}";
            waveReachText.text = waveReach.ToString();
        }
        else {
            gameObject.SetActive(false);
        }
        return this;
    }
    public void MoveToTarget(Transform target, float alpha, System.Action onComplete) {
        var origin = background.transform.position;
        var originScale = transform.localScale;
        gameObject.SetActive(true);
        Fade(alpha);
        transform.DOMove(target.position, timeMove).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
            background.gameObject.SetActive(false);
            background.transform.position = origin;
            transform.localScale = originScale;
            onComplete?.Invoke();
        });
    }
    public void Fade(float alpha) {
        zoneIndexText.SetAlpha(0);
        waveReachText.SetAlpha(0);
        titletext.SetAlpha(0);
        background.SetAlpha(0);
        zoneIndexText.DOFade(alpha * 5, timeMove);
        waveReachText.DOFade(alpha * 5, timeMove);
        titletext.DOFade(alpha * 5, timeMove);
        background.DOFade(alpha, timeMove);
    }
    public void SetFrameSelect(bool status) {
        if (frameSelect)
            frameSelect.gameObject.SetActive(status);
    }
}