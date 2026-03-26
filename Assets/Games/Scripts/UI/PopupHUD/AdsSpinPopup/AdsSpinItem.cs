using Gemmob;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdsSpinItem : MonoBehaviour {
    [SerializeField] private GameObject lockGroup;
    [SerializeField] private Image icon;
    [SerializeField] private Image frameSelect;
    [SerializeField] private Image whiteFrame;

    private AdsSpinInfo data;
    public void UpdateUI(AdsSpinInfo data) {
        this.data = data;
        data.GenMod();
        if (data.IsMod)
            icon.sprite = data.Icon;
    }
    public IEnumerator PlayEffect(float deltaTime) {
        transform.DOKill(true);
        frameSelect.SetAlpha(1);
        frameSelect.transform.localScale = Vector3.one;
        frameSelect.gameObject.SetActive(true);
        transform.DOScale(1.05f, deltaTime).SetLoops(2, LoopType.Yoyo);
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
