using Gemmob;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusSpinItem : MonoBehaviour {
    [SerializeField] private Image background;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private TextMeshProUGUI value;
    [SerializeField] private Image frameSelect;
    [SerializeField] private Image whiteFrame;

    private BonusSpinInfo data;
    public BonusSpinItem UpdateUI(BonusSpinInfo data) {
        this.data = data;
        rewardIcon.sprite = data.Icon;
        value.text = $"{data.Amount}";
        return this;
    }
    public void SetColor(Color color) {
        background.SetColor(color);
    }
    public IEnumerator PlayEffect(float deltaTime) {
        transform.DOKill(true);
        frameSelect.SetAlpha(1);
        frameSelect.transform.localScale = Vector3.one;
        frameSelect.gameObject.SetActive(true);
        transform.DOScale(1.1f, deltaTime).SetLoops(2, LoopType.Yoyo);
        yield return Yielder.Wait(deltaTime);
        frameSelect.gameObject.SetActive(false);
    }
    public void PlayChooseEffect(float deltaTime, Action onComplete) {
        transform.DOKill(true);
        whiteFrame.gameObject.SetActive(true);
        whiteFrame.SetAlpha(1);
        whiteFrame.transform.DOScale(Vector3.one * 2, deltaTime).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
            frameSelect.gameObject.SetActive(true);
            frameSelect.SetAlpha(1);
            frameSelect.DOFade(0, deltaTime * 2).SetUpdate(true);
            frameSelect.transform.DOScale(Vector3.one * 1.2f, deltaTime).SetUpdate(true).OnComplete(() => {
                whiteFrame.DOFade(0, deltaTime * 2).SetUpdate(true).OnComplete(() => {
                    onComplete?.Invoke();
                });
                frameSelect.DOFade(0, deltaTime).SetUpdate(true);

            });
        });
    }

}
