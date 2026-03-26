using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NewModInforItemDisplayer : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI modName;
    [SerializeField] private TextMeshProUGUI modDescription;

    public NewModInforItemDisplayer SetName(string name) {
        modName.text = name;
        return this;
    }
    public NewModInforItemDisplayer SetDescription(string description) {
        modDescription.text = description;
        return this;
    }
    public NewModInforItemDisplayer SetInfor(string name, string description) {
        modName.text = name;
        modDescription.text = description;
        return this;
    }
    public NewModInforItemDisplayer PlayAnimation(float time) {
        modName.SetAlpha(0);
        modDescription.SetAlpha(0);
        DOVirtual.DelayedCall(time - 1f, () => {
            modName.DOFade(1, 1);
            modDescription.DOFade(1, 1);
        });
        return this;
    }
    public NewModInforItemDisplayer PlayAnimation1(float time, int index) {
        DOVirtual.DelayedCall(time - 1f, () => {
            transform.rectTransform().anchoredPosition = Vector2.zero;
            transform.rectTransform().DOAnchorPosY(-45 - 90 * index, 1).SetEase(Ease.OutBack);
            modName.SetAlpha(0);
            modDescription.SetAlpha(0);
            modName.DOFade(1, 1);
            modDescription.DOFade(1, 1);
        });
        return this;
    }
}
